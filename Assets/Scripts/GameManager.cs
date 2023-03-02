using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//using Facebook.Unity;
//using GameAnalyticsSDK;
using TMPro;
using UnityEngine.SceneManagement;
using Taptic;
public class GameManager : MonoBehaviour
{
 


    [SerializeField] AudioSource buttonAudioSource;


    AudioSource audioSource;

    [SerializeField] LevelSettings levelSettings;
    [SerializeField] EconomySettings[] economySettings;

    public bool V_2;

    [SerializeField] Vector3[] PosCam;
    [SerializeField] Vector3[] PosCam2;
    [SerializeField] Vector3[] RotCam;
    [SerializeField] Camera _mainCam;
    [SerializeField] float[] _camSize;



    public GameObject[] FactoryObj;
    //public FactoryManager[] Factory;
    //public FactoryManagerV2[] FactoryV2;
    public FactoryManager _currentFactory;

    public int FactoryValue;
    public float tapSpeed;


    public Button AddStandButton;
    public Button AddOpButton;
    public Button UpgradeButton;
    public Image UpBack;
    public GameObject UpTextObj;


    public float money;
    public float moneyPer;
    public Transform moneyImage;


    [SerializeField] TMP_Text _moneyText;
    [SerializeField] TMP_Text _currentMoneyPerText;


    public static GameManager Instance;
    //public EconomySettings economySettings;
    public float timeCount;


    public Button RewardStandButton;
    public Button RewardOpButton;
    public Button RewardUpButton;


    public int rewardValue;
    public int x4Value;
    public int x2Value;

    public GameObject x4Tab;
    public GameObject x2Tab;
    public GameObject noInternetTab;
    public GameObject x4Button;
    public GameObject x2Button;

    public float rewardSpeedMultp = 1;
    public float rewardPriceMultp = 1;

    public static float RewardSpeedMultp = 1;
    public static float RewardPriceMultp = 1;

    [SerializeField] GameObject _lock;
    public Button NextMapButton;
    public Button PreviousMapButton;

    [SerializeField] float[] newMapPrice;


    [SerializeField] TMP_Text _mapMoneyText;


    public float[] MoneyPer;



    [SerializeField] GameObject _ADSCanvas;

    [SerializeField] GameObject[] Fon;


    [SerializeField] TMP_Text[] MoneyPerText;
    //[SerializeField] TMP_Text MoneyPerText1;
    //[SerializeField] TMP_Text MoneyPerText2;
    //[SerializeField] TMP_Text MoneyPerText3;


    [SerializeField] Color colorGreen;
    [SerializeField] Color colorWhite;


    [SerializeField] GameObject _tutorial;
    [SerializeField] GameObject _gameCanvas;
 

    [SerializeField] TMP_Text BustTimeText;
    [SerializeField] bool bustOpen;
    [SerializeField] float _bustTimeValue;
    [SerializeField] float _bustTime;
    [SerializeField] int _bustValue;
    [SerializeField] int _bustValueLimit;

    public Image _fillImage;
    public float UpSpeed;
    [SerializeField] float _fillFloat;



    [SerializeField] TMP_Text BustTimeText2;
    [SerializeField] bool bustOpen2;
    [SerializeField] float _bustTimeValue2;
    [SerializeField] float _bustTime2;
    [SerializeField] int _bustValue2;
    [SerializeField] int _bustValueLimit2;

    public Image _fillImage2;
    public float UpSpeed2;
    [SerializeField] float _fillFloat2;

    [SerializeField] float _bustTextMoveX;




    [SerializeField] TMP_Text _opText;
    [SerializeField] TMP_Text _mergeText;
    [SerializeField] TMP_Text _standText;
    [SerializeField] Transform moneyImageMain;
    [SerializeField] TutorialManager tutorial;







    [Header("InterstitialAd Settings")]

    public  TMP_Text ADSTimeText;
    public bool InterstitialAdOpen;
    public float _interstitialAdTimeValue;
    public float _interstitialAdTime;
    [SerializeField] int _interstitialAdValue;
    [SerializeField] int _interstitialAdValueLimit;




    [SerializeField] bool x5Time;

    private void Awake()
    {



        Instance = this;

        //GameAnalytics.Initialize();

        _ADSCanvas.SetActive(true);

 



        //if (!FB.IsInitialized) {
        //    // Initialize the Facebook SDK
        //    FB.Init(InitCallback);
        //} else {
        //    // Already initialized, signal an app activation App Event
        //    FB.ActivateApp();
        //}

    }

    //private void InitCallback()
    //{
    //    if (FB.IsInitialized) {
    //        // Signal an app activation App Event
    //        FB.ActivateApp();
    //        // Continue with Facebook SDK
    //        // ...
    //    } else {
    //        Debug.Log("Failed to Initialize the Facebook SDK");
    //    }
    //}


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 1;
        //RemoteConfig.Load(levelSettings,LoadGame);

        //for (int i = 0; i < 4; i++)
        //{
        //    RemoteConfig.Load(economySettings[i]);
        //}
       

        LoadGame();

    }

    // Update is called once per frame

    

    void Update()
    {
        //if (x5Time)
        //{
        //   Time.timeScale = 3;
        //}
     

        if (InterstitialAdOpen)
        {
            ADSTimeText.enabled = true;
            _interstitialAdTime -= Time.deltaTime;
            ADSTimeText.text = ((int)_interstitialAdTime + 1).ToString();
        }
        else
        {
            ADSTimeText.enabled = false;
        }

        if (bustOpen)
        {
            BustTimeText.gameObject.SetActive(true);

            _bustTime -= Time.deltaTime;

            if (_bustTime + 1 >= 10)
            {
                BustTimeText.text = "00:" + ((int)_bustTime + 1).ToString();
            }
            else
            {
                BustTimeText.text = "00:0" + ((int)_bustTime + 1).ToString();
            }


            _fillImage.fillAmount -= _fillFloat * Time.deltaTime;



        }
        else
        {

            _fillImage.fillAmount = 0;
            BustTimeText.gameObject.SetActive(false);

        }

        if (bustOpen2)
        {
            BustTimeText2.gameObject.SetActive(true);

            _bustTime2 -= Time.deltaTime;


            if (_bustTime2 + 1 >= 10)
            {
                BustTimeText2.text = "00:" + ((int)_bustTime2 + 1).ToString();
            }
            else
            {
                BustTimeText2.text = "00:0" + ((int)_bustTime2 + 1).ToString();
            }


            _fillImage2.fillAmount -= _fillFloat2 * Time.deltaTime;



        }
        else
        {

            _fillImage2.fillAmount = 0;
            BustTimeText2.gameObject.SetActive(false);

        }


        //if (FactoryValue == 1)
        //{
        //    _lock.SetActive(true);
        //    PreviousMapButton.enabled = true;
        //    PreviousMapButton.gameObject.SetActive(true);
        //    NextMapButton.enabled = false;
        //    _mapMoneyText.text = "$" + (_newMapPrice1 / 1000000) + "M";
        //}
        //else





        //if (PlayerPrefs.GetInt("LastFactory") <= 2)
        //{


        //    _lock.SetActive(true);
        //    NextMapButton.enabled = false;
        //    PreviousMapButton.enabled = false;
        //    PreviousMapButton.gameObject.SetActive(false);

        //    _mapMoneyText.text = "$" + (_newMapPrice / 1000000) + "M";
        //}else



        //if (_newMapPrice < money|| PlayerPrefs.GetInt("LastFactory") == 1)
        //{
        //    _lock.SetActive(false);
        //    PreviousMapButton.enabled = false;
        //    PreviousMapButton.gameObject.SetActive(false);
        //    NextMapButton.enabled = true;
        //    _mapMoneyText.text = "";
        //}
        //else

        //    if (PlayerPrefs.GetInt("LastFactory") <= 1) {


        //    _lock.SetActive(true);
        //    NextMapButton.enabled = false;
        //    PreviousMapButton.enabled = false;
        //    PreviousMapButton.gameObject.SetActive(false);

        //    _mapMoneyText.text = "$" + (_newMapPrice / 1000000) + "M";
        //}




        if (PlayerPrefs.GetInt("LastFactory") <= FactoryValue)
        {
            if (newMapPrice[FactoryValue] > money)
            {
                _lock.SetActive(true);
                NextMapButton.enabled = false;

                if (FactoryValue == 0)
                {
                    PreviousMapButton.enabled = false;
                    PreviousMapButton.gameObject.SetActive(false);
                }


                if (newMapPrice[FactoryValue] >= 1000000000)
                {
                    _mapMoneyText.text = "$" + (newMapPrice[FactoryValue] / 1000000000) + "B";
                }
                else
                {

                    _mapMoneyText.text = "$" + (newMapPrice[FactoryValue] / 1000000) + "M";
                }
            }
            else
            {
                _lock.SetActive(false);
                NextMapButton.enabled = true;

            }
        }
        else
        {
            _lock.SetActive(false);
            NextMapButton.enabled = true;

            if (FactoryValue == 0)
            {

                PreviousMapButton.enabled = false;
                PreviousMapButton.gameObject.SetActive(false);
            }
            else
            {
                PreviousMapButton.enabled = true;
                PreviousMapButton.gameObject.SetActive(true);

            }
        }

   



        timeCount += Time.deltaTime;

        RewardSpeedMultp = rewardSpeedMultp;
        RewardPriceMultp = rewardPriceMultp;

      
        money = Mathf.Lerp(money, _currentFactory.money, Time.deltaTime * 8);



        //if (PlayerPrefs.GetInt("LastFactory") == 1)
        //{
        //    MoneyPerText1.transform.parent.gameObject.SetActive(true);
        //}


        //if (FactoryValue == 1)
        //{
        //    moneyPer = _currentFactory.moneyPer;
        //    MoneyPerText0.transform.parent.GetComponent<Image>().color = colorWhite;
        //    MoneyPerText1.transform.parent.GetComponent<Image>().color = colorGreen;
        //    ButtonPerText(moneyPer0, MoneyPerText0 );
        //    FindObjectOfType<TapController>().perImage = MoneyPerText1.transform;
        //    _currentMoneyPerText = MoneyPerText1;
        //}
        //else
        //{
        //    moneyPer = _currentFactory.moneyPer;
        //    MoneyPerText0.transform.parent.GetComponent<Image>().color = colorGreen;
        //    MoneyPerText1.transform.parent.GetComponent<Image>().color = colorWhite;
        //    ButtonPerText(moneyPer1, MoneyPerText1);
        //    FindObjectOfType<TapController>().perImage = MoneyPerText0.transform;
        //    _currentMoneyPerText = MoneyPerText0;
        //}
        
            for (int i = 0; i < PlayerPrefs.GetInt("LastFactory") + 1; i++)
            {
                 MoneyPerText[i].transform.parent.gameObject.SetActive(true);
                moneyPer = _currentFactory.moneyPer;

                if (i == FactoryValue)
                {

                    MoneyPerText[i].transform.parent.GetComponent<Image>().color = colorGreen;
                    FindObjectOfType<TapController>().perImage = MoneyPerText[i].transform;
                    _currentMoneyPerText = MoneyPerText[i];
                }
                else 
                {
                    MoneyPerText[i].transform.parent.GetComponent<Image>().color = colorWhite;
                    ButtonPerText(MoneyPer[i], MoneyPerText[i]);
                }



            }
        



        _moneyText.text = money.ToString();




        if (_currentFactory.tapSpeed > 1)
        {
            //_moneyPerText.text = (moneyPer * FindObjectOfType<EconomyManager>().productSpeed*2).ToString() + "/sec";

            ButtonPerText((moneyPer * _currentFactory.economy.productSpeed*rewardSpeedMultp*rewardPriceMultp * 2), _currentMoneyPerText);
        }
        else
        {
            // _moneyPerText.text = (moneyPer * FindObjectOfType<EconomyManager>().productSpeed ).ToString() + "/sec";

            ButtonPerText((moneyPer * _currentFactory.economy.productSpeed * rewardPriceMultp * rewardSpeedMultp), _currentMoneyPerText);

        }


        ButtonText((int)money, _moneyText);


        if (rewardValue > 2)
        {
            OpenReward();
        }
        else
        {

            CloseRewardUpdate();
        }

    }
    

    public void AddStand()
    {

        _currentFactory.AddStand();

            if (rewardValue <3)
            {
                rewardValue++;
            }


        if (_interstitialAdValue < _interstitialAdValueLimit)
        {
            _interstitialAdValue++;
        }
        else
        {
           // StartCoroutine(ADSOpen());
            _interstitialAdValue = 0;
        }






        if (PlayerPrefs.GetInt("x4") != 1)
        {

            if (x4Value < 10)
            {
                x4Value++;
            }
            else if (x4Value == 10)
            {
                x4Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x4", 1);
                x4Value++;
            }
        }



        if (PlayerPrefs.GetInt("x2") != 1)
        {
            if (x2Value < 5)
            {
                x2Value++;
            }
            else if (x2Value == 5)
            {
                x2Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x2", 1);
                x2Value++;
            }
        }




    }

    public void AddFreeStand()
    {


        _currentFactory.AddFreeStand();

        CloseReward();

        if (PlayerPrefs.GetInt("x4") != 1)
        {

            if (x4Value < 10)
            {
                x4Value++;
            }
            else if (x4Value == 10)
            {
                x4Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x4", 1);
                x4Value++;
            }
        }



        if (PlayerPrefs.GetInt("x2") != 1)
        {
            if (x2Value < 5)
            {
                x2Value++;
            }
            else if (x2Value == 5)
            {
                x2Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x2", 1);
                x2Value++;
            }
        }




    }


    public void AddOperator()
    {

        _currentFactory.AddOperator();

            if (rewardValue < 3)
            {
                rewardValue++;
            }

        if (_interstitialAdValue < _interstitialAdValueLimit)
        {
            _interstitialAdValue++;
        }
        else
        {
            //StartCoroutine(ADSOpen());
            _interstitialAdValue = 0;
        }


        if (PlayerPrefs.GetInt("x4") != 1)
        {

            if (x4Value < 10)
            {
                x4Value++;
            }
            else if (x4Value == 10)
            {
                x4Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x4", 1);
                x4Value++;
            }
        }



        if (PlayerPrefs.GetInt("x2") != 1)
        {
            if (x2Value < 5)
            {
                x2Value++;
            }
            else if (x2Value == 5)
            {
                x2Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x2", 1);
                x2Value++;
            }
        }

    }

    


    public void AddFreeOperator()
    {
        Debug.Log("ADD FREE OP");
        _currentFactory.AddFreeOperator();

        CloseReward();



        //if (_interstitialAdValue < _interstitialAdValueLimit)
        //{
        //    _interstitialAdValue++;
        //}
        //else
        //{
        //    StartCoroutine(ADSOpen());
        //    _interstitialAdValue = 0;
        //}


        if (PlayerPrefs.GetInt("x4") != 1)
        {

            if (x4Value < 10)
            {
                x4Value++;
            }
            else if (x4Value == 10)
            {
                x4Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x4", 1);
                x4Value++;
            }
        }



        if (PlayerPrefs.GetInt("x2") != 1)
        {
            if (x2Value < 5)
            {
                x2Value++;
            }
            else if (x2Value == 5)
            {
                x2Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x2", 1);
                x2Value++;
            }
        }



    }


    public void Upgrade()
    {


        _currentFactory.Upgrade();

            if (rewardValue < 3)
            {
                rewardValue++;
            }


        if (_interstitialAdValue < _interstitialAdValueLimit)
        {
            _interstitialAdValue++;
        }
        else
        {
           // StartCoroutine(ADSOpen());
            _interstitialAdValue = 0;
        }



        if (PlayerPrefs.GetInt("x4") != 1)
        {

            if (x4Value < 10)
            {
                x4Value++;
            }
            else if (x4Value == 10)
            {
                x4Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x4", 1);
                x4Value++;
            }
        }



        if (PlayerPrefs.GetInt("x2") != 1)
        {
            if (x2Value < 5)
            {
                x2Value++;
            }
            else if (x2Value == 5)
            {
                x2Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x2", 1);
                x2Value++;
            }
        }



    }


    public void UpgradeFree()
    {


        _currentFactory.UpgradeFree();

        CloseReward();


        if (_interstitialAdValue < _interstitialAdValueLimit)
        {
            _interstitialAdValue++;
        }
        else
        {
           // StartCoroutine(ADSOpen());
            _interstitialAdValue = 0;
        }



        if (PlayerPrefs.GetInt("x4") != 1)
        {

            if (x4Value < 10)
            {
                x4Value++;
            }
            else if (x4Value == 10)
            {
                x4Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x4", 1);
                x4Value++;
            }
        }



        if (PlayerPrefs.GetInt("x2") != 1)
        {
            if (x2Value < 5)
            {
                x2Value++;
            }
            else if (x2Value == 5)
            {
                x2Tab.SetActive(true);
                GameButtonClose();
                PlayerPrefs.SetInt("x2", 1);
                x2Value++;
            }
        }



    }


    public void RestartGame()
    {

        StartCoroutine(DelayRes());
       

    }

    IEnumerator DelayRes()
    {
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ButtonText(float price, TMP_Text text)
    {


        if (price >= 1000000000)
        {
            text.text = (price / 1000000000).ToString("0.#") + "B";


        }
        else
        if (price >= 1000000)
        {
            text.text = (price / 1000000).ToString("0.#") + "M";


        }
        else
        if (price >= 1000)
        {
            text.text = (price / 1000).ToString("0.#") + "K";


        }
        else
        {
            text.text = price.ToString() ;
        }
    }

    public void MoneyUp()
    {
        _currentFactory.MoneyUp();

    }
    public void ButtonPerText(float price, TMP_Text text)
    {


        if (price >= 1000000000)
        {
            text.text ="$"+ (price / 1000000000).ToString("0.#") + "B/sec";


        }
        else
        if (price >= 1000000)
        {
            text.text = "$" + (price / 1000000).ToString("0.#") + "M/sec";


        }
        else
        if (price >= 1000)
        {
            text.text = "$" + (price / 1000).ToString("0.#") + "K/sec";


        }
        else
        {
            text.text = "$" + price.ToString() + "/sec";
        }
    }

    public void OpenReward()
    {
        if (_currentFactory.canBuyOp) 
        {
            RewardOpButton.enabled = false;
            RewardOpButton.image.enabled = false;
            RewardOpButton.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            RewardOpButton.enabled = true;
            RewardOpButton.image.enabled = true;
            RewardOpButton.transform.GetChild(0).gameObject.SetActive(true);
        }


        if (_currentFactory.canBuyUp)
        {
            RewardUpButton.enabled = false;
            RewardUpButton.image.enabled = false;
            RewardUpButton.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            RewardUpButton.enabled = true;
            RewardUpButton.image.enabled = true;
            RewardUpButton.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (_currentFactory.canBuyStand)
        {
            RewardStandButton.enabled = false;
            RewardStandButton.image.enabled = false;
            RewardStandButton.transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            RewardStandButton.enabled = true;
            RewardStandButton.image.enabled = true;
            RewardStandButton.transform.GetChild(0).gameObject.SetActive(true);
        }


    }

    public void CloseReward()
    {
        rewardValue = 0;
        RewardOpButton.enabled = false;
        RewardOpButton.image.enabled = false;
        RewardOpButton.transform.GetChild(0).gameObject.SetActive(false);


        RewardUpButton.enabled = false;
        RewardUpButton.image.enabled = false;
        RewardUpButton.transform.GetChild(0).gameObject.SetActive(false);

        RewardStandButton.enabled = false;
        RewardStandButton.image.enabled = false;
        RewardStandButton.transform.GetChild(0).gameObject.SetActive(false);



    }

    public void CloseRewardUpdate()
    {
       
        RewardOpButton.enabled = false;
        RewardOpButton.image.enabled = false;
        RewardOpButton.transform.GetChild(0).gameObject.SetActive(false);


        RewardUpButton.enabled = false;
        RewardUpButton.image.enabled = false;
        RewardUpButton.transform.GetChild(0).gameObject.SetActive(false);

        RewardStandButton.enabled = false;
        RewardStandButton.image.enabled = false;
        RewardStandButton.transform.GetChild(0).gameObject.SetActive(false);



    }

    public void OpReward()
    {
        AddFreeOperator();
        //rewardValue = 0;
        CloseReward();
    }

    public void UpReward()
    {
        UpgradeFree();
        //rewardValue = 0;
        CloseReward();
    }

    public void StandReward()
    {
        AddFreeStand();
        //rewardValue = 0;
        CloseReward();
    }

    public void x4Speed()
    {

        SpeedReward();

    }


    public void x4Speed2()
    {

        _gameCanvas.SetActive(true);
        SpeedReward();
        x4Tab.SetActive(false);
        StartCoroutine(SpeedButtonTime());
        StartCoroutine(BustOpen2());

        if (BustTimeText.gameObject.activeSelf == true)
        {
            BustTimeText.transform.DOLocalMoveX(_bustTextMoveX, 0.2f);
            BustTimeText2.transform.DOLocalMoveX(-_bustTextMoveX, 0.2f);
        }
    }



    public void x2Price()
    {

        PaymentReward();

    }

    public void x2Price2()
    {
        _gameCanvas.SetActive(true);
        PaymentReward();
        x2Tab.SetActive(false);
        StartCoroutine(PaymentButtonTime());
        StartCoroutine(BustOpen());
        if (BustTimeText2.gameObject.activeSelf == true)
        {
            BustTimeText.transform.DOLocalMoveX(_bustTextMoveX, 0.2f);
            BustTimeText2.transform.DOLocalMoveX(-_bustTextMoveX, 0.2f);
        }
    }



    public void NoX4Speed()
    {
        _gameCanvas.SetActive(true);
        x4Tab.SetActive(false);
        x4Button.SetActive(true);
    }


    public void NoX2Price()
    {
        _gameCanvas.SetActive(true);
        x2Tab.SetActive(false);
        x2Button.SetActive(true);

    }


    public void SpeedReward()
    {
        x4Tab.SetActive(false);
        // x4Button.SetActive(true);
        StartCoroutine(SpeedRewardTime());
        StartCoroutine(SpeedRewardTime());
        StartCoroutine(BustOpen2());
        if (BustTimeText.gameObject.activeSelf == true)
        {
            BustTimeText.transform.DOLocalMoveX(_bustTextMoveX, 0.2f);
            BustTimeText2.transform.DOLocalMoveX(-_bustTextMoveX, 0.2f);
        }
    }

    public void PaymentReward()
    {
        x2Tab.SetActive(false);
        //x2Button.SetActive(true);
        StartCoroutine(PaymentRewardTime());
        StartCoroutine(PaymentRewardTime());
        StartCoroutine(BustOpen());
        if (BustTimeText2.gameObject.activeSelf == true)
        {
            BustTimeText.transform.DOLocalMoveX(_bustTextMoveX, 0.2f);
            BustTimeText2.transform.DOLocalMoveX(-_bustTextMoveX, 0.2f);
        }

    }




    IEnumerator SpeedRewardTime()
    {
        x4Button.SetActive(false);
        OperatorController.fast = true;
        rewardSpeedMultp = 2f;
        yield return new WaitForSeconds(20);
        rewardSpeedMultp = 1;
        OperatorController.fast = false;
        x4Button.SetActive(true);
    }

    IEnumerator PaymentRewardTime()
    {
        x2Button.SetActive(false);
        OperatorController.doublePay =true;
        rewardPriceMultp = 2;
        yield return new WaitForSeconds(20);
        rewardPriceMultp = 1;
        OperatorController.doublePay = false;
        x2Button.SetActive(true);
    }


    public void AddRewardOnClick()
    {
        CloseReward();
    }

    public void AddSpeedOnClick()
    {
        x4Tab.SetActive(false);
        StartCoroutine(SpeedButtonTime());
    }


    public void AddPaymentOnClick()
    {
        x2Tab.SetActive(false);
        StartCoroutine(PaymentButtonTime());
    }

    IEnumerator SpeedButtonTime()
    {
        x4Button.SetActive(false);
        yield return new WaitForSeconds(20);
        x4Button.SetActive(true);
    }

    IEnumerator PaymentButtonTime()
    {
        x2Button.SetActive(false);
        yield return new WaitForSeconds(20);
   
        x2Button.SetActive(true);
    }


    public void FactorySwitch()
    {
        if (!_currentFactory.progress || !_currentFactory.progress2)
        {
            _ADSCanvas.SetActive(false);
            _currentFactory.Save();
            OperatorController.doublePay = false;


            if (PlayerPrefs.GetInt("LastFactory") <= FactoryValue)
            {

                _currentFactory.money -= newMapPrice[FactoryValue];
                money = _currentFactory.money;


            }
            PlayerPrefs.SetFloat("MainMoney", money);

            FactoryValue += 1;
            PlayerPrefs.SetInt("FactoryValue", FactoryValue);
            RestartGame();
        }
    }


    public void FactorySwitchPrevious()
    {
        if (!_currentFactory.progress || !_currentFactory.progress2)
        {
            _ADSCanvas.SetActive(false);
            _currentFactory.Save();
            PlayerPrefs.SetFloat("MainMoney", money);
            OperatorController.doublePay = false;
            FactoryValue -= 1;
            PlayerPrefs.SetInt("FactoryValue", FactoryValue);



            RestartGame();
        }
    }


    void GameButtonClose()
    {
        CloseReward();
        _gameCanvas.SetActive(false);
    }


    IEnumerator ADSOpen()
    {
        InterstitialAdOpen = true;
        yield return new WaitForSeconds(_interstitialAdTime);
        // x4Tab.SetActive(true);
        InterstitialAdOpen = false;
        CallInterstitialAd();
        _interstitialAdTime = _interstitialAdTimeValue;

    }

    IEnumerator BustOpen()
    {
        BustTimeText.transform.DOScale(Vector3.one, 0.2f);
        bustOpen = true;
        _fillImage.fillAmount = 1;
        yield return new WaitForSeconds(_bustTime);
        BustTimeText.transform.DOScale(Vector3.zero, 0.2f);
        BustTimeText.transform.DOLocalMoveX(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        bustOpen = false;


        _bustTime = _bustTimeValue;
    }

    IEnumerator BustOpen2()
    {
        BustTimeText2.transform.DOScale(Vector3.one, 0.2f);
        bustOpen2 = true;
        _fillImage2.fillAmount = 1;
        yield return new WaitForSeconds(_bustTime2);
        BustTimeText2.transform.DOScale(Vector3.zero, 0.2f);
        BustTimeText2.transform.DOLocalMoveX(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        bustOpen2 = false;


        _bustTime2 = _bustTimeValue2;
    }


    // InterstitialAd buradan cagirilacak
    public void CallInterstitialAd()
    {


    }


   void LoadGame()
    {
        FactoryValue = PlayerPrefs.GetInt("FactoryValue");

        if (PlayerPrefs.GetInt("LastFactory") < FactoryValue)
        {
            PlayerPrefs.SetInt("LastFactory", FactoryValue);
        }

        if (FactoryValue > 0)
        {
            _tutorial.SetActive(false);
        }


  

        GameObject factory0 = Resources.Load<GameObject>($"Levels/{levelSettings.LevelOrder[FactoryValue]}") as GameObject;

        Debug.Log("FactoryValue; "+ FactoryValue);
 


        _currentFactory = Instantiate(factory0, Vector3.zero, Quaternion.identity).GetComponent<FactoryManager>();

        LoadFactory();



        for (int i = 0; i < 4; i++)
        {
            MoneyPer[i] = PlayerPrefs.GetFloat($"MoneyPer_{i}");
        }


  
            money = PlayerPrefs.GetFloat("MainMoney");
        

      


        Application.targetFrameRate = 60;


        StartCoroutine(LevelPriceDelay());

        if (_currentFactory.StandValue > 0)
        {
            rewardValue = 3;
        }
        else
        {
            rewardValue = 0;
        }



        Fon[_currentFactory.fontValue].SetActive(true);



        moneyPer = MoneyPer[FactoryValue];

        if (PlayerPrefs.GetInt("x4") == 1)
        {
            x4Button.SetActive(true);

        }



        if (PlayerPrefs.GetInt("x2") == 1)
        {
            x2Button.SetActive(true);
        }


        _interstitialAdTime = _interstitialAdTimeValue;
        _bustTime = _bustTimeValue;


    }


    void LoadFactory()
    {

    _currentFactory._opText =_opText;
    _currentFactory._mergeText= _mergeText;
    _currentFactory._standText= _standText;
    _currentFactory.moneyImage= moneyImage;
    _currentFactory.tutorial= tutorial;


    }

    IEnumerator LevelPriceDelay()
    {
        yield return new  WaitForSeconds(0.01f);



        for (int i = 0; i < newMapPrice.Length; i++)
        {

            newMapPrice[i] = economySettings[0].newMapPrice[i];

        }
    }

    public void Vib()
    {
        Vibration.Vibrate(45, 60, true);
    }

    public void ProductSound()
    {
        audioSource.Play();
    }


    public void ButtonSound()
    {
        buttonAudioSource.Play();
    }


    public void MaxDebugger()
    {
        //MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
        //    // Show Mediation Debugger
        //    MaxSdk.ShowMediationDebugger();
        //};
    }

}
