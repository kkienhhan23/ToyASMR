using UnityEngine;


[CreateAssetMenu(menuName = "SciptableObjects / Economy Settings / New Economy Settings")]
public class EconomySettings : ScriptableObject
{
    

    [Header("Baslangic Degerleri Level ")]
    public float opPriceVar;
    public float upgradeVar;
    public float standPriceVar;


    [Header("Isci ekleme formul degiskenleri Level ")]
    public int opVar1;
    public int opVar2;


    [Header("Merge formul degiskenleri Level ")]
    public int upVar1;
    public int upVar2;

    [Header("Urun kazanc degerleri Level ")]
    public int[] payment;


    [Header("Urun kazanc carpani Level ")]
    public float paymentMultiplier;


    [Header("Versiyon_2 icin tiklama kapasiteleri Level ")]
    public int opClickCapacity;
    public int upClickCapacity;

    [Header("Uretim Hizlari Level ")]
    public float speedDown;
    public float speedUp;
    public float turnTime;

    [Header("Urun cikis hizi Level ")]
    public float productSpeed;
    public float productSpeedUp;
    public float productSpeedDown;


    [Header("Level isci ekleme fiyat artis yuzdesi")]
    public float yuzdeOp;

    [Header("Level merge fiyat artis yuzdesi")]
    public float yuzdeMerge;


    [Header("Level stand fiyat artis yuzdesi")]
    public float yuzdeStand;

    [Header("Level acma fiyatlari")]
    public float[] newMapPrice;


    public string _keyMoneyPer;
    public string _keyCurrentOpPrice;
    public string _keyCurrentUpPrice;
    public string _keyCurrentStandPrice;
}