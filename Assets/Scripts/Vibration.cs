using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Taptic
{
    public static class Vibration
    {
        public static logLevel LogLevel = logLevel.Disabled;

        private static AndroidJavaObject vibrator = null;
        private static AndroidJavaClass vibrationEffectClass = null;
        private static int defaultAmplitude = 255;

        private static int ApiLevel = 1;
        private static bool doesSupportVibrationEffect() => ApiLevel >= 26;
        private static bool doesSupportPredefinedEffect() => ApiLevel >= 29;

        #region Initialization

        public static bool isInitialized = false;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        [SuppressMessage("Code quality", "IDE0051", Justification = "Called on scene load")]
        private static void Initialize()
        {
#if UNITY_ANDROID
            if (Application.isConsolePlatform)
            {
                Handheld.Vibrate();
            }
#endif

            if (isInitialized == false && Application.platform == RuntimePlatform.Android)
            {
                using (AndroidJavaClass androidVersionClass = new AndroidJavaClass("android.os.Build$VERSION"))
                {
                    ApiLevel = androidVersionClass.GetStatic<int>("SDK_INT");
                }

                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    if (currentActivity != null)
                    {
                        vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

                        if (doesSupportVibrationEffect())
                        {
                            vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
                            defaultAmplitude = Mathf.Clamp(vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE"), 1,
                                255);
                        }

                        if (doesSupportPredefinedEffect())
                        {
                            PredefinedEffect.EFFECT_CLICK = vibrationEffectClass.GetStatic<int>("EFFECT_CLICK");
                            PredefinedEffect.EFFECT_DOUBLE_CLICK =
                                vibrationEffectClass.GetStatic<int>("EFFECT_DOUBLE_CLICK");
                            PredefinedEffect.EFFECT_HEAVY_CLICK =
                                vibrationEffectClass.GetStatic<int>("EFFECT_HEAVY_CLICK");
                            PredefinedEffect.EFFECT_TICK = vibrationEffectClass.GetStatic<int>("EFFECT_TICK");
                        }
                    }
                }

                logAuto("Vibration component initialized", logLevel.Info);
                isInitialized = true;
            }
        }

        #endregion

        #region Vibrate Public

        public static void Vibrate(long milliseconds, int amplitude = -1, bool cancel = false)
        {
            string funcToStr() => string.Format("Vibrate ({0}, {1}, {2})", milliseconds, amplitude, cancel);
            Initialize();
            if (isInitialized == false)
            {
                logAuto(funcToStr() + ": Not initialized", logLevel.Warning);
            }
            else if (HasVibrator() == false)
            {
                logAuto(funcToStr() + ": Device doesn't have Vibrator", logLevel.Warning);
            }
            else
            {
                if (cancel) Cancel();
                if (doesSupportVibrationEffect())
                {
                    amplitude = Mathf.Clamp(amplitude, -1, 255);
                    if (amplitude == -1) amplitude = 255;
                    if (amplitude != 255 && HasAmplitudeControl() == false)
                    {
                        logAuto(funcToStr() + ": Device doesn't have Amplitude Control, but Amplitude was set",
                            logLevel.Warning);
                    }

                    if (amplitude == 0) amplitude = defaultAmplitude;


                    amplitude = HasAmplitudeControl() == false ? 255 : amplitude;

                    vibrateEffect(milliseconds, amplitude);
                    logAuto(funcToStr() + ": Effect called", logLevel.Info);
                }
                else
                {
                    vibrateLegacy(milliseconds);
                    logAuto(funcToStr() + ": Legacy called", logLevel.Info);
                }
            }
        }

        public static void Vibrate(long[] pattern, int[] amplitudes = null, int repeat = -1, bool cancel = false)
        {
            string funcToStr() => string.Format("Vibrate (({0}), ({1}), {2}, {3})", arrToStr(pattern),
                arrToStr(amplitudes), repeat, cancel);

            Initialize();
            if (isInitialized == false)
            {
                logAuto(funcToStr() + ": Not initialized", logLevel.Warning);
            }
            else if (HasVibrator() == false)
            {
                logAuto(funcToStr() + ": Device doesn't have Vibrator", logLevel.Warning);
            }
            else
            {
                if (amplitudes != null && amplitudes.Length != pattern.Length)
                {
                    logAuto(
                        funcToStr() +
                        ": Length of Amplitudes array is not equal to Pattern array. Amplitudes will be ignored.",
                        logLevel.Warning);
                    amplitudes = null;
                }

                if (amplitudes != null)
                {
                    clampAmplitudesArray(amplitudes);
                }

                if (cancel) Cancel();
                if (doesSupportVibrationEffect())
                {
                    if (amplitudes != null && HasAmplitudeControl() == false)
                    {
                        logAuto(funcToStr() + ": Device doesn't have Amplitude Control, but Amplitudes was set",
                            logLevel.Warning);
                        amplitudes = null;
                    }

                    if (amplitudes != null)
                    {
                        vibrateEffect(pattern, amplitudes, repeat);
                        logAuto(funcToStr() + ": Effect with amplitudes called", logLevel.Info);
                    }
                    else
                    {
                        vibrateEffect(pattern, repeat);
                        logAuto(funcToStr() + ": Effect called", logLevel.Info);
                    }
                }
                else
                {
                    vibrateLegacy(pattern, repeat);
                    logAuto(funcToStr() + ": Legacy called", logLevel.Info);
                }
            }
        }


        public static void VibratePredefined(int effectId, bool cancel = false)
        {
            string funcToStr() => string.Format("VibratePredefined ({0})", effectId);

            Initialize();
            if (isInitialized == false)
            {
                logAuto(funcToStr() + ": Not initialized", logLevel.Warning);
            }
            else if (HasVibrator() == false)
            {
                logAuto(funcToStr() + ": Device doesn't have Vibrator", logLevel.Warning);
            }
            else if (doesSupportPredefinedEffect() == false)
            {
                logAuto(funcToStr() + ": Device doesn't support Predefined Effects (Api Level >= 29)",
                    logLevel.Warning);
            }
            else
            {
                if (cancel) Cancel();
                vibrateEffectPredefined(effectId);
                logAuto(funcToStr() + ": Predefined effect called", logLevel.Info);
            }
        }

        #endregion

        #region Public Properties & Controls

        public static long[] ParsePattern(string pattern)
        {
            if (pattern == null) return new long[0];
            pattern = pattern.Trim();
            string[] split = pattern.Split(',');

            long[] timings = new long[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                if (int.TryParse(split[i].Trim(), out int duration))
                {
                    timings[i] = duration < 0 ? 0 : duration;
                }
                else
                {
                    timings[i] = 0;
                }
            }

            return timings;
        }


        public static int GetApiLevel() => ApiLevel;

        public static int GetDefaultAmplitude() => defaultAmplitude;

        public static bool HasVibrator()
        {
            return vibrator != null && vibrator.Call<bool>("hasVibrator");
        }

        public static bool HasAmplitudeControl()
        {
            if (HasVibrator() && doesSupportVibrationEffect())
            {
                return vibrator.Call<bool>("hasAmplitudeControl");
            }
            else
            {
                return false;
            }
        }

        public static void Cancel()
        {
            if (HasVibrator())
            {
                vibrator.Call("cancel");
                logAuto("Cancel (): Called", logLevel.Info);
            }
        }

        #endregion

        #region Vibrate Internal

        #region Vibration Callers

        private static void vibrateEffect(long milliseconds, int amplitude)
        {
            using (AndroidJavaObject effect = createEffect_OneShot(milliseconds, amplitude))
            {
                vibrator.Call("vibrate", effect);
            }
        }

        private static void vibrateLegacy(long milliseconds)
        {
            vibrator.Call("vibrate", milliseconds);
        }

        private static void vibrateEffect(long[] pattern, int repeat)
        {
            using (AndroidJavaObject effect = createEffect_Waveform(pattern, repeat))
            {
                vibrator.Call("vibrate", effect);
            }
        }

        private static void vibrateLegacy(long[] pattern, int repeat)
        {
            vibrator.Call("vibrate", pattern, repeat);
        }

        private static void vibrateEffect(long[] pattern, int[] amplitudes, int repeat)
        {
            using (AndroidJavaObject effect = createEffect_Waveform(pattern, amplitudes, repeat))
            {
                vibrator.Call("vibrate", effect);
            }
        }

        private static void vibrateEffectPredefined(int effectId)
        {
            using (AndroidJavaObject effect = createEffect_Predefined(effectId))
            {
                vibrator.Call("vibrate", effect);
            }
        }

        #endregion

        #region Vibration Effect

        private static AndroidJavaObject createEffect_OneShot(long milliseconds, int amplitude)
        {
            return vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", milliseconds, amplitude);
        }

        private static AndroidJavaObject createEffect_Predefined(int effectId)
        {
            return vibrationEffectClass.CallStatic<AndroidJavaObject>("createPredefined", effectId);
        }

        private static AndroidJavaObject createEffect_Waveform(long[] timings, int[] amplitudes, int repeat)
        {
            return vibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", timings, amplitudes, repeat);
        }

        private static AndroidJavaObject createEffect_Waveform(long[] timings, int repeat)
        {
            return vibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", timings, repeat);
        }

        #endregion

        #endregion

        #region Internal

        private static void logAuto(string text, logLevel level)
        {
            if (level == logLevel.Disabled) level = logLevel.Info;

            if (text != null)
            {
                if (level == logLevel.Warning && LogLevel == logLevel.Warning)
                {
                    Debug.LogWarning(text);
                }
                else if (level == logLevel.Info && LogLevel >= logLevel.Info)
                {
                    Debug.Log(text);
                }
            }
        }

        private static string arrToStr(long[] array) => array == null ? "null" : string.Join(", ", array);
        private static string arrToStr(int[] array) => array == null ? "null" : string.Join(", ", array);

        private static void clampAmplitudesArray(int[] amplitudes)
        {
            for (int i = 0; i < amplitudes.Length; i++)
            {
                amplitudes[i] = Mathf.Clamp(amplitudes[i], 1, 255);
            }
        }

        #endregion

        public static class PredefinedEffect
        {
            public static int EFFECT_CLICK;
            public static int EFFECT_DOUBLE_CLICK;
            public static int EFFECT_HEAVY_CLICK;
            public static int EFFECT_TICK;
        }

        public enum logLevel
        {
            Disabled,
            Info,
            Warning,
        }
    }
}