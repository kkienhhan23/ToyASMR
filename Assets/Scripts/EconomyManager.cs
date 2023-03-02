using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{

    [SerializeField] TMP_Text _speedText;


    [Header(" Baslangic Degerleri")]
    public float opPriceVar;
    public float upgradeVar;
    public float standPriceVar;


    [Header("Isci ekleme formul degiskenleri")]
    public int opVar1;
    public int opVar2;


    [Header("Merge formul degiskenleri")]
    public int upVar1;
    public int upVar2;

    [Header("Urun kazanc degerleri")]
    public int[] payment;


    [Header("Urun kazanc carpani")]
    public float paymentMultiplier;


    [Header("Versiyon_2 icin tiklama kapasiteleri")]
    public int opClickCapacity;
    public int upClickCapacity;

    [Header("Uretim Hizlari")]
    public float speedDown;
    public float speedUp;
    public float turnTime;

    [Header("Urun cikis hizi")]
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
    public float[] newMapPrice ;


    [SerializeField] int economyValue;

    [SerializeField] EconomySettings[] EconomySettings;
    [SerializeField] EconomySettings economySettings;
    public string _keyMoneyPer;
    public string _keyCurrentOpPrice;
    public string _keyCurrentUpPrice;
    public string _keyCurrentStandPrice;
    private void Start()
    {


        economySettings = EconomySettings[FindObjectOfType<GameManager>().FactoryValue];

        opPriceVar = economySettings.opPriceVar;
        upgradeVar = economySettings.upgradeVar;
        standPriceVar = economySettings.standPriceVar;
        opVar1 = economySettings.opVar1;
        opVar2 = economySettings.opVar2;
        upVar1 = economySettings.upVar1;
        upVar2 = economySettings.upVar2;

        for (int i = 0; i < payment.Length; i++)
        {
            payment[i] = economySettings.payment[i];
        }

        for (int i = 0; i < newMapPrice.Length; i++)
        {
            newMapPrice[i] = economySettings.newMapPrice[i];
        }

        paymentMultiplier = economySettings.paymentMultiplier;
        speedDown = economySettings.speedDown;
        speedUp = economySettings.speedUp;
        turnTime = economySettings.turnTime;
        productSpeed = economySettings.productSpeed;
        productSpeedUp = economySettings.productSpeedUp;
        productSpeedDown = economySettings.productSpeedDown;
        yuzdeOp = economySettings.yuzdeOp;
        yuzdeMerge = economySettings.yuzdeMerge;
        yuzdeStand = economySettings.yuzdeStand;


        _keyMoneyPer = economySettings._keyMoneyPer;
        _keyCurrentOpPrice = economySettings._keyCurrentOpPrice;
        _keyCurrentUpPrice = economySettings._keyCurrentUpPrice;
        _keyCurrentStandPrice = economySettings._keyCurrentStandPrice;

    }

    


    private void Update()
    {
       // _speedText.text = "speed:" + speedDown.ToString();
    }

    public void SpeedUp()
    {
        speedDown += 1;
        speedUp = speedDown * 2;
        productSpeedDown *= 0.8f;
        productSpeedUp *= 0.8f;
        turnTime *= 0.666f;


    }


    public void SpeedReset()
    {
        speedDown = 2;
        speedUp = speedDown * 2;
        productSpeedDown = 2f;
        productSpeedUp = 1f;
        turnTime = 0.4f;


    }

}
