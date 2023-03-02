using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FactoryManager : MonoBehaviour
{

    [SerializeField] Vector3[] PosCam;
    [SerializeField] Vector3[] PosCam2;
    [SerializeField] Vector3[] RotCam;
    [SerializeField] Camera _mainCam;
    [SerializeField] float[] _camSize;

    [SerializeField] StandManager[] Stand;
    //[SerializeField] OperatorController[] Operator;
    //[SerializeField] OperatorController[] OpMachine;
    //[SerializeField] OperatorController[] OpLaser;
    //[SerializeField] OperatorController[] OpHexa;
    //[SerializeField] OperatorController[] OpPress;
    //[SerializeField] OperatorController[] OpTermal;


    [SerializeField] SlotManager[] Slot;
    [SerializeField] SlotManager[] CurrentSlot = new SlotManager[3];

    public List<SlotManager> EmptySlot;
    public List<SlotManager> OpActiveSlot;
    [SerializeField] List<SlotManager> OpMacActiveSlot;
    [SerializeField] List<SlotManager> OpLaserActiveSlot;
    [SerializeField] List<SlotManager> OpHexaActiveSlot;
    [SerializeField] List<SlotManager> OpPressActiveSlot;
    [SerializeField] List<SlotManager> OpTermalActiveSlot;


    /*
    [SerializeField] List<Transform> ActiveOp;
    [SerializeField] List<Transform> ActiveMac;
    [SerializeField] List<Transform> ActiveLaser;
    [SerializeField] List<Transform> ActiveHexa;
    [SerializeField] List<Transform> ActivePress;
 */
    public int StandValue;
    public int[] SlotValue;
    /*
    public int OpValue;
    public int OpValue2;
    public int OpMachineValue;
    public int OpMachineValue2;
    public int OpLaserValue;
    public int OpLaserValue2;
    public int OpHexaValue;
    public int OpHexaValue2;
    public int OpPressValue;
    public int OpPressValue2;
    public int OpTermalValue;
    public int OpTermalValue2;
    */

    public int OpCount;
    public int OpMacCount;
    public int OpLaserCount;
    public int OpHexaCount;
    public int OpPressCount;
    public int OpTermalCount;











    public float tapSpeed;

    [SerializeField] Transform _enter;
    [SerializeField] GameObject _product;

    public float productSpeed;
    public float productSpeedDown;

    public float money;
    public int moneyPer;
    public int n;
    public int n1;
    public int nVar;
    public int z;
    public int nVar1;
    public int z1;
    public float opPriceVar;
    public float standPriceVar;
    public float upgradePriceVar;

    public int opVar1;
    public int opVar2;

    public int upVar1;
    public int upVar2;


    public int standVar1;
    public int standVar2;



    [SerializeField] float _currentOpPrice;

    [SerializeField] float _currentUpPrice;

    [SerializeField] float _currentStandPrice;

    public  bool progress;
    public bool progress2;


    public int opClickCapacity;

    public int upClickCapacity;

    public TMP_Text _opText;
    public TMP_Text _mergeText;
    public TMP_Text _standText;
    public Transform moneyImage;
    public TutorialManager tutorial;



    public ParticleSystem spawnParticle;


    public bool inflateMoney;



    [SerializeField] Animator _enterAnim;
    public int boxCapacity;
    public int x;
    public int boxMultp;
    public float productBoxCount;



    public bool canBuyOp;
    public bool canBuyStand;
    public bool canBuyUp;



    [SerializeField] string _keyMoney;
    [SerializeField] string _keyN;
    [SerializeField] string _keyN1;
    public string _keyCurrentOpPrice;
    public string _keyCurrentUpPrice;
    public string _keyCurrentStandPrice;
    [SerializeField] string _keyStandValue;
    [SerializeField] string _keyBoxCapacity;
    [SerializeField] string _keyX;
    public string _keyMoneyPer;

    public EconomyManager economy;


    [SerializeField] float[] MoneyPer;

    public int factoryNum;

    [SerializeField] GameManager gameManager;


    [Header("Level isci ekleme fiyat artis yuzdesi")]
    public float yuzdeOp;

    [Header("Level merge fiyat artis yuzdesi")]
    public float yuzdeMerge;


    [Header("Level stand fiyat artis yuzdesi")]
    public float yuzdeStand;


    public int fontValue;
    // Start is called before the first frame update


    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Start()
    {
        
        // money = PlayerPrefs.GetFloat("MainMoney");
        _mainCam = Camera.main;
        
        Load();


      


        for (int i = 0; i < 4; i++)
        {
            MoneyPer[i] = gameManager.MoneyPer[i];
        }
        

        StartCoroutine(Spawning());

        if (PlayerPrefs.GetInt("LastFactory") == 1)
        {

            tutorial.gameObject.SetActive(false);
        }
        StartCoroutine(MoneyEnter());

        for (int i = 0; i < 28; i++)
        {
            Slot[i].hierarchy = i;
        }


        if (StandValue > 0)
        {
            tutorial.gameObject.SetActive(false);
        }

        moneyPer = OpActiveSlot.Count * economy.payment[0] + OpMacActiveSlot.Count * economy.payment[1] + OpLaserActiveSlot.Count * economy.payment[2] + OpHexaActiveSlot.Count * economy.payment[3] + OpPressActiveSlot.Count * economy.payment[4] + OpTermalActiveSlot.Count * economy.payment[5];
        //PlayerPrefs.SetFloat(_keyMoneyPer, moneyPer);

        yuzdeOp = economy.yuzdeOp;
        yuzdeMerge = economy.yuzdeMerge;
        yuzdeStand = economy.yuzdeStand;


    }

    // Update is called once per frame
    void Update()
    {

        _mainCam.orthographicSize = Mathf.Lerp(_mainCam.orthographicSize, _camSize[StandValue], Time.deltaTime * 2);
        productSpeedDown = FindObjectOfType<EconomyManager>().productSpeedDown;

        tapSpeed = Mathf.Lerp(tapSpeed, 0, Time.deltaTime * 0.6f);
        productSpeed = Mathf.Lerp(productSpeed, productSpeedDown, Time.deltaTime);


        moneyPer = OpActiveSlot.Count * economy.payment[0] + OpMacActiveSlot.Count * economy.payment[1] + OpLaserActiveSlot.Count * economy.payment[2] + OpHexaActiveSlot.Count * economy.payment[3] + OpPressActiveSlot.Count * economy.payment[4] + OpTermalActiveSlot.Count * economy.payment[5];



        // Debug.Log("Money:"+PlayerPrefs.GetInt("StandValue").ToString());

        if (tapSpeed > 2)
        {
            OperatorController.tapFast = true;

        }
        else
        {
            OperatorController.tapFast = false;
        }




        if (!progress)
        {

            if (OpCount < 3 && OpMacCount < 3 && OpLaserCount < 3 && OpHexaCount < 3 && OpPressCount < 3 )
            {
                gameManager.UpTextObj.SetActive(false);
                gameManager.UpBack.enabled = false;
                gameManager.UpgradeButton.enabled = false;
                gameManager.UpgradeButton.image.enabled = false;
                canBuyUp = true;
            }
            else
            if (money < _currentUpPrice)
            {

                gameManager.UpTextObj.SetActive(true);
                gameManager.UpBack.enabled = true;
                gameManager.UpgradeButton.enabled = false;
                gameManager.UpgradeButton.image.enabled = false;

                canBuyUp = false;

            }
            else
            {
                gameManager.UpTextObj.SetActive(true);
                gameManager.UpBack.enabled = true;
                gameManager.UpgradeButton.enabled = true;
                gameManager.UpgradeButton.image.enabled = true;
                if (tutorial.gameObject.activeSelf == true)
                {
                    if (OpMacActiveSlot.Count < 1)
                    {
                        tutorial.MergeImageOpen();
                    }
                }
                canBuyUp = true;
            }



            if (EmptySlot.Count < 1)
            {
                gameManager.AddOpButton.enabled = false;
                gameManager.AddOpButton.image.enabled = false;
                canBuyOp = true;
            }
            else
            if (money < _currentOpPrice)
            {
                gameManager.AddOpButton.enabled = false;
                gameManager.AddOpButton.image.enabled = false;
                canBuyOp = false;
            }
            else
            {
                gameManager.AddOpButton.enabled = true;
                gameManager.AddOpButton.image.enabled = true;

                if (tutorial.gameObject.activeSelf == true)
                {
                    if (OpActiveSlot.Count < 3)
                    {
                        tutorial.OpImageOpen();
                    }
                }
                canBuyOp = true;
            }

            if (StandValue >= Stand.Length - 1)
            {
                gameManager.AddStandButton.enabled = false;
                gameManager.AddStandButton.image.enabled = false;
                canBuyStand = true;
            }
            else
            if (money < _currentStandPrice)
            {
                gameManager.AddStandButton.enabled = false;
                gameManager.AddStandButton.image.enabled = false;
                canBuyStand = false;
            }
            else
            {
                gameManager.AddStandButton.enabled = true;
                gameManager.AddStandButton.image.enabled = true;
                canBuyStand = true;
            }





        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            money += 1000000;
        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            money += 50;
        }


        if (EmptySlot.Count == 0)
        {
            _opText.text = "MAX";

        }
        else
        {
            ButtonText(_currentOpPrice, _opText);
        }



        ButtonText(_currentUpPrice, _mergeText);


        if (StandValue < Stand.Length - 1)
        {
            ButtonText(_currentStandPrice, _standText);
        }
        else
        {
            _standText.text = "MAX";

        }




    }

    public void AddStand()
    {
        if (StandValue < Stand.Length - 1 && money >= _currentStandPrice)
        {

            tutorial.gameObject.SetActive(false);

            money -= _currentStandPrice;



            if (gameManager.FactoryValue == 3)
            {
                _currentStandPrice += (_currentStandPrice * yuzdeStand);
            }
            else
            if (gameManager.FactoryValue == 2)
            {
                _currentStandPrice += (_currentStandPrice * yuzdeStand);
            }
            else
            if (gameManager.FactoryValue == 1)
            {
                _currentStandPrice += (_currentStandPrice * yuzdeStand);
            }
            else
            {
                _currentStandPrice = (_currentStandPrice * 2 + 3500);

            }





            Stand[StandValue].Box.SetActive(false);
            //Stand[StandValue].TurnPiece.SetActive(true);
            StandValue++;

            for (int i = 0; i < Stand[StandValue].Stands.childCount; i++)
            {
                if (Stand[StandValue].Stands.GetChild(i).GetComponent<Collider>() != null)
                {
                    Stand[StandValue].Stands.GetChild(i).GetComponent<Collider>().enabled = false;
                }
            }

            for (int i = SlotValue[StandValue]; i < SlotValue[StandValue] + Stand[StandValue].Slot.Length; i++)
            {
                EmptySlot.Add(Slot[i]);
            }
            _mainCam.transform.DOMove(PosCam2[StandValue], 0.4f);
            //_mainCam.transform.DORotate(RotCam[StandValue], 0.4f);

            StartCoroutine(AddingStand());

        }
    }

    public void AddFreeStand()
    {
        if (StandValue < Stand.Length - 1)
        {

            tutorial.gameObject.SetActive(false);




            Stand[StandValue].Box.SetActive(false);
            //Stand[StandValue].TurnPiece.SetActive(true);
            StandValue++;

            for (int i = 0; i < Stand[StandValue].Stands.childCount; i++)
            {
                if (Stand[StandValue].Stands.GetChild(i).GetComponent<Collider>() != null)
                {
                    Stand[StandValue].Stands.GetChild(i).GetComponent<Collider>().enabled = false;
                }
            }

            for (int i = SlotValue[StandValue]; i < SlotValue[StandValue] + Stand[StandValue].Slot.Length; i++)
            {
                EmptySlot.Add(Slot[i]);
            }
            _mainCam.transform.DOMove(PosCam2[StandValue], 0.4f);
            //_mainCam.transform.DORotate(RotCam[StandValue], 0.4f);

            StartCoroutine(AddingStand());

        }
    }



    public void AddOperator()
    {

        if (EmptySlot.Count > 0)
        {
            if (OpActiveSlot.Count > 3 || OpMacActiveSlot.Count > 1)
            {

                tutorial.gameObject.SetActive(false);

            }
            else

                if (tutorial.gameObject.activeSelf == true)
            {
                tutorial.OpImageClose();

                if (OpActiveSlot.Count > 2)
                {
                    tutorial.TapImageClose();
                }
            }
            //money -= _currentOpPrice;
            //_currentOpPrice = (opVar1 + ((opVar2+z) * n));
            //n++;

            money -= _currentOpPrice;


            n++;

            if (gameManager.FactoryValue == 3)
            {
                _currentOpPrice += (_currentOpPrice * yuzdeOp);
                //_currentOpPrice =15000+ (opVar1 + (opVar2 * n)) * 20;
                Debug.Log("LEVEL3");
            }
            else
            if (gameManager.FactoryValue == 2)
            {
                _currentOpPrice += (_currentOpPrice * yuzdeOp);
                //_currentOpPrice =10000+ (opVar1 + (opVar2 * n)) * 10;
                Debug.Log("LEVEL2");
            }
            else
             if (gameManager.FactoryValue == 1)
            {
                Debug.Log("_currentOpPrice0:" + _currentOpPrice);
                _currentOpPrice += (_currentOpPrice * yuzdeOp);
                Debug.Log("LEVEL1");
                Debug.Log("_currentOpPrice1:" + _currentOpPrice);
                //_currentOpPrice =(opVar1 + (opVar2 * n))*3;
            }
            else
            {
                _currentOpPrice = (opVar1 + (opVar2 * n));

            }









            StartCoroutine(AddingOperator());
        }

    }



    public void AddFreeOperator()
    {

        if (EmptySlot.Count > 0)
        {
            if (OpActiveSlot.Count > 3 || OpMacActiveSlot.Count > 1)
            {

                tutorial.gameObject.SetActive(false);

            }
            else

                if (tutorial.gameObject.activeSelf == true)
            {
                tutorial.OpImageClose();

                if (OpActiveSlot.Count > 2)
                {
                    tutorial.TapImageClose();
                }
            }



            StartCoroutine(AddingOperator());
        }

    }



    public void Upgrade()
    {



        if (OpCount > 2 && money > _currentUpPrice)
        {
            if (tutorial.gameObject.activeSelf == true)
            {
                tutorial.MergeImageClose();
                tutorial.TapImageClose();
            }

            money -= _currentUpPrice;



            if (gameManager.FactoryValue == 3)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 60;
            }
            else
           if (gameManager.FactoryValue == 2)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 30;
            }
            else
           if (gameManager.FactoryValue == 1)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 9;

            }
            else
            {

                _currentUpPrice += (upVar1 + (upVar2 + z1) * n1);

            }

            n1++;

            //if (nVar1 < 3)
            //{
            //    nVar1++;
            //}
            //else
            //{
            //    nVar1 = 0;
            //    z1++;
            //}


            HierarchyOp();
            StartCoroutine(AddingMachine(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));


        }
        else
        if (OpMacCount > 2 && money > _currentUpPrice)
        {

            money -= _currentUpPrice;
            if (gameManager.FactoryValue == 3)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 60;
            }
            else
        if (gameManager.FactoryValue == 2)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 30;
            }
            else
        if (gameManager.FactoryValue == 1)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 9;

            }
            else
            {

                _currentUpPrice += (upVar1 + (upVar2 + z1) * n1);

            }
            n1++;
            //if (nVar1 < 3)
            //{
            //    nVar1++;
            //}
            //else
            //{
            //    nVar1 = 0;
            //    z1++;
            //}

            HierarchyOpMac();
            StartCoroutine(AddingLaser(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));

        }
        else


        if (OpLaserCount > 2 && money > _currentUpPrice)
        {

            money -= _currentUpPrice;
            if (gameManager.FactoryValue == 3)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 60;
            }
            else
             if (gameManager.FactoryValue == 2)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 30;
            }
            else
             if (gameManager.FactoryValue == 1)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 9;

            }
            else
            {

                _currentUpPrice += (upVar1 + (upVar2 + z1) * n1);

            }
            n1++;
            //if (nVar1 < 3)
            //{
            //    nVar1++;
            //}
            //else
            //{
            //    nVar1 = 0;
            //    z1++;
            //}

            HierarchyOpLaser();
            StartCoroutine(AddingHexa(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));

        }
        else


        if (OpHexaCount > 2 && money > _currentUpPrice)
        {


            money -= _currentUpPrice;
            if (gameManager.FactoryValue == 3)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 60;
            }
            else
           if (gameManager.FactoryValue == 2)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 30;
            }
            else
           if (gameManager.FactoryValue == 1)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 9;

            }
            else
            {

                _currentUpPrice += (upVar1 + (upVar2 + z1) * n1);

            }
            n1++;
            //if (nVar1 < 3)
            //{
            //    nVar1++;
            //}
            //else
            //{
            //    nVar1 = 0;
            //    z1++;
            //}

            HierarchyOpHexa();
            StartCoroutine(AddingPress(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));


        }
        else

        if (OpPressCount > 2 && money > _currentUpPrice)
        {

            money -= _currentUpPrice;

            if (gameManager.FactoryValue == 3)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 60;
            }
            else
                if (gameManager.FactoryValue == 2)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 30;
            }
            else
                if (gameManager.FactoryValue == 1)
            {
                _currentUpPrice += (_currentUpPrice * yuzdeMerge);
                //_currentUpPrice += (upVar1 + (upVar2 + z1) * n1) * 9;

            }
            else
            {

                _currentUpPrice += (upVar1 + (upVar2 + z1) * n1);

            }
            n1++;
            //if (nVar1 < 3)
            //{
            //    nVar1++;
            //}
            //else
            //{
            //    nVar1 = 0;
            //    z1++;
            //}


            HierarchyOpPress();
            StartCoroutine(AddingTermal(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));

        }










    }





    public void UpgradeFree()
    {



        if (OpCount > 2)
        {
            if (tutorial.gameObject.activeSelf == true)
            {
                tutorial.MergeImageClose();
                tutorial.TapImageClose();
            }




            HierarchyOp();
            StartCoroutine(AddingMachine(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));


        }
        else
        if (OpMacCount > 2)
        {



            HierarchyOpMac();
            StartCoroutine(AddingLaser(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));

        }
        else


        if (OpLaserCount > 2)
        {



            HierarchyOpLaser();
            StartCoroutine(AddingHexa(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));

        }
        else


        if (OpHexaCount > 2)
        {




            HierarchyOpHexa();
            StartCoroutine(AddingPress(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));


        }
        else

        if (OpPressCount > 2)
        {



            HierarchyOpPress();
            StartCoroutine(AddingTermal(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));

        }










    }







    IEnumerator AddingStand()
    {
        progress = true;
        Time.timeScale = 0.1f;
        ButtonClose();
        Stand[StandValue].gameObject.SetActive(true);
        Stand[StandValue].transform.DOScale(Vector3.one * 1.4f, 0.02f);
        yield return new WaitForSeconds(0.02f);
        Stand[StandValue].transform.DOScale(Vector3.one, 0.01f);
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < Stand[StandValue].Stands.childCount; i++)
        {
            if (Stand[StandValue].Stands.GetChild(i).GetComponent<Collider>() != null)
            {
                Stand[StandValue].Stands.GetChild(i).GetComponent<Collider>().enabled = true;
            }
        }

        progress = false;
        Time.timeScale = 1f;
        Save();
        ButtonOpen();
    }


    IEnumerator AddingOperator()
    {

        progress = true;
        ButtonClose0();

        EmptySlot[0].Operator.gameObject.SetActive(true);
        EmptySlot[0].Operator.transform.DOScale(Vector3.one * 1.4f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        EmptySlot[0].Operator.transform.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.1f);
        EmptySlot[0].type = 1;
        EmptySlot[0].SaveType();
        OpActiveSlot.Add(EmptySlot[0]);
        EmptySlot.RemoveAt(0);
        OpCount++;

        progress = false;
        Save();
        ButtonOpen();
    }

    IEnumerator AddingMachine(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress = true;
        progress2 = true;
        ButtonClose();

        slot0.Operator.transform.DOMove(slot2.Operator.transform.position, 0.6f);
        slot1.Operator.transform.DOMove(slot2.Operator.transform.position, 0.6f);
        yield return new WaitForSeconds(0.6f);
        slot0.Operator.transform.localScale = Vector3.zero;
        slot1.Operator.transform.localScale = Vector3.zero;
        slot2.Operator.transform.localScale = Vector3.zero;

        slot0.Operator.gameObject.SetActive(false);
        slot1.Operator.gameObject.SetActive(false);
        slot2.Operator.gameObject.SetActive(false);


        slot0.type = 0;
        slot0.SaveType();
        slot1.type = 0;
        slot1.SaveType();
        slot2.type = 0;
        slot2.SaveType();

        slot2.OpMachine.gameObject.SetActive(true);
        slot2.mergeParticle.Play();
        slot2.type = 2;
        slot2.SaveType();
        slot2.OpMachine.transform.DOScale(Vector3.one * 1.4f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot2.OpMachine.transform.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.1f);
        slot0.Operator.transform.localPosition = slot0.Operator.pFirst;
        slot1.Operator.transform.localPosition = slot1.Operator.pFirst;
        EmptySlot.Add(slot0);
        EmptySlot.Add(slot1);
        OpMacActiveSlot.Add(slot2);
        OpActiveSlot.Remove(slot0);
        OpActiveSlot.Remove(slot1);
        OpActiveSlot.Remove(slot2);
        OpMacCount++;
        OpCount -= 3;

        progress2 = false;
        progress = false;
        Save();
        ButtonOpen();
    }

    IEnumerator AddingLaser(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress = true;
        progress2 = true;
        ButtonClose();

        slot0.OpMachine.transform.DOMove(slot2.OpMachine.transform.position, 0.6f);
        slot1.OpMachine.transform.DOMove(slot2.OpMachine.transform.position, 0.6f);
        yield return new WaitForSeconds(0.6f);
        slot0.OpMachine.transform.localScale = Vector3.zero;
        slot1.OpMachine.transform.localScale = Vector3.zero;
        slot2.OpMachine.transform.localScale = Vector3.zero;

        slot0.OpMachine.gameObject.SetActive(false);
        slot1.OpMachine.gameObject.SetActive(false);
        slot2.OpMachine.gameObject.SetActive(false);


        slot0.type = 0;
        slot0.SaveType();
        slot1.type = 0;
        slot1.SaveType();
        slot2.type = 0;
        slot2.SaveType();

        slot2.OpLaser.gameObject.SetActive(true);
        slot2.mergeParticle.Play();
        slot2.type = 3;
        slot2.SaveType();
        slot2.OpLaser.transform.DOScale(Vector3.one * 1.4f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot2.OpLaser.transform.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.1f);
        slot0.OpMachine.transform.localPosition = slot0.OpMachine.pFirst;
        slot1.OpMachine.transform.localPosition = slot1.OpMachine.pFirst;
        EmptySlot.Add(slot0);
        EmptySlot.Add(slot1);
        OpLaserActiveSlot.Add(slot2);
        OpMacActiveSlot.Remove(slot0);
        OpMacActiveSlot.Remove(slot1);
        OpMacActiveSlot.Remove(slot2);
        OpLaserCount++;
        OpMacCount -= 3;

        progress2 = false;
        progress = false;
        Save();
        ButtonOpen();

    }


    IEnumerator AddingHexa(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress = true;
        progress2 = true;
        ButtonClose();

        slot0.OpLaser.transform.DOMove(slot2.OpLaser.transform.position, 0.6f);
        slot1.OpLaser.transform.DOMove(slot2.OpLaser.transform.position, 0.6f);
        yield return new WaitForSeconds(0.6f);
        slot0.OpLaser.transform.localScale = Vector3.zero;
        slot1.OpLaser.transform.localScale = Vector3.zero;
        slot2.OpLaser.transform.localScale = Vector3.zero;

        slot0.OpLaser.gameObject.SetActive(false);
        slot1.OpLaser.gameObject.SetActive(false);
        slot2.OpLaser.gameObject.SetActive(false);

        slot0.type = 0;
        slot0.SaveType();
        slot1.type = 0;
        slot1.SaveType();
        slot2.type = 0;
        slot2.SaveType();

        slot2.OpHexa.gameObject.SetActive(true);
        slot2.mergeParticle.Play();
        slot2.type = 4;
        slot2.SaveType();
        slot2.OpHexa.transform.DOScale(Vector3.one * 1.4f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot2.OpHexa.transform.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.1f);
        slot0.OpLaser.transform.localPosition = slot0.OpLaser.pFirst;
        slot1.OpLaser.transform.localPosition = slot1.OpLaser.pFirst;
        EmptySlot.Add(slot0);
        EmptySlot.Add(slot1);
        OpHexaActiveSlot.Add(slot2);
        OpLaserActiveSlot.Remove(slot0);
        OpLaserActiveSlot.Remove(slot1);
        OpLaserActiveSlot.Remove(slot2);
        OpHexaCount++;
        OpLaserCount -= 3;

        progress2 = false;
        progress = false;
        Save();
        ButtonOpen();


    }


    IEnumerator AddingPress(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress = true;
        progress2 = true;
        ButtonClose();

        slot0.OpHexa.transform.DOMove(slot2.OpHexa.transform.position, 0.6f);
        slot1.OpHexa.transform.DOMove(slot2.OpHexa.transform.position, 0.6f);
        yield return new WaitForSeconds(0.6f);
        slot0.OpHexa.transform.localScale = Vector3.zero;
        slot1.OpHexa.transform.localScale = Vector3.zero;
        slot2.OpHexa.transform.localScale = Vector3.zero;

        slot0.OpHexa.gameObject.SetActive(false);
        slot1.OpHexa.gameObject.SetActive(false);
        slot2.OpHexa.gameObject.SetActive(false);

        slot0.type = 0;
        slot0.SaveType();
        slot1.type = 0;
        slot1.SaveType();
        slot2.type = 0;
        slot2.SaveType();

        slot2.OpPress.gameObject.SetActive(true);
        slot2.mergeParticle.Play();
        slot2.type = 5;
        slot2.SaveType();
        slot2.OpPress.transform.DOScale(Vector3.one * 1.4f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot2.OpPress.transform.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.1f);
        slot0.OpHexa.transform.localPosition = slot0.OpHexa.pFirst;
        slot1.OpHexa.transform.localPosition = slot1.OpHexa.pFirst;
        EmptySlot.Add(slot0);
        EmptySlot.Add(slot1);
        OpPressActiveSlot.Add(slot2);
        OpHexaActiveSlot.Remove(slot0);
        OpHexaActiveSlot.Remove(slot1);
        OpHexaActiveSlot.Remove(slot2);
        OpPressCount++;
        OpHexaCount -= 3;

        progress2 = false;
        progress = false;
        Save();
        ButtonOpen();

    }

    IEnumerator AddingTermal(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress = true;
        progress2 = true;
        ButtonClose();

        slot0.OpPress.transform.DOMove(slot2.OpPress.transform.position, 0.6f);
        slot1.OpPress.transform.DOMove(slot2.OpPress.transform.position, 0.6f);
        yield return new WaitForSeconds(0.6f);
        slot0.OpPress.transform.localScale = Vector3.zero;
        slot1.OpPress.transform.localScale = Vector3.zero;
        slot2.OpPress.transform.localScale = Vector3.zero;

        slot0.OpPress.gameObject.SetActive(false);
        slot1.OpPress.gameObject.SetActive(false);
        slot2.OpPress.gameObject.SetActive(false);

        slot0.type = 0;
        slot0.SaveType();
        slot1.type = 0;
        slot1.SaveType();
        slot2.type = 0;
        slot2.SaveType();


        slot2.OpTermal.gameObject.SetActive(true);
        slot2.mergeParticle.Play();
        slot2.type = 6;
        slot2.SaveType();
        slot2.OpTermal.transform.DOScale(Vector3.one * 1.4f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot2.OpTermal.transform.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.1f);
        slot0.OpPress.transform.localPosition = slot0.OpPress.pFirst;
        slot1.OpPress.transform.localPosition = slot1.OpPress.pFirst;
        EmptySlot.Add(slot0);
        EmptySlot.Add(slot1);
        OpTermalActiveSlot.Add(slot2);
        OpPressActiveSlot.Remove(slot0);
        OpPressActiveSlot.Remove(slot1);
        OpPressActiveSlot.Remove(slot2);
        OpTermalCount++;
        OpPressCount -= 3;

        progress2 = false;
        progress = false;
        Save();
        ButtonOpen();

    }



    IEnumerator Spawning()
    {
        while (true)
        {



            yield return new WaitForSeconds(productSpeed / GameManager.RewardSpeedMultp - 0.1f);
            spawnParticle.Play();

            Instantiate(_product, _enter.position + Vector3.up * 10, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            if (OperatorController.fast && OperatorController.tapFast)
            {
                _enterAnim.SetTrigger("Working2");
                _enterAnim.SetTrigger("Idle");
            }
            else

            if (OperatorController.tapFast || OperatorController.fast)
            {
                _enterAnim.SetTrigger("Working1");
                _enterAnim.SetTrigger("Idle");
            }
            else
            {
                _enterAnim.SetTrigger("Working");
                _enterAnim.SetTrigger("Idle");

            }


            Save();
        }

    }


    private void HierarchyOp()
    {
        if (OpActiveSlot[0].hierarchy > OpActiveSlot[1].hierarchy &&
    OpActiveSlot[1].hierarchy > OpActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpActiveSlot[0];
            CurrentSlot[1] = OpActiveSlot[1];
            CurrentSlot[2] = OpActiveSlot[2];
        }
        else if (OpActiveSlot[0].hierarchy > OpActiveSlot[2].hierarchy &&
        OpActiveSlot[2].hierarchy > OpActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpActiveSlot[0];
            CurrentSlot[1] = OpActiveSlot[2];
            CurrentSlot[2] = OpActiveSlot[1];
        }
        else if (OpActiveSlot[1].hierarchy > OpActiveSlot[2].hierarchy &&
        OpActiveSlot[2].hierarchy > OpActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpActiveSlot[1];
            CurrentSlot[1] = OpActiveSlot[2];
            CurrentSlot[2] = OpActiveSlot[0];
        }
        else if (OpActiveSlot[1].hierarchy > OpActiveSlot[0].hierarchy &&
        OpActiveSlot[0].hierarchy > OpActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpActiveSlot[0];
            CurrentSlot[1] = OpActiveSlot[2];
            CurrentSlot[2] = OpActiveSlot[1];
        }
        else if (OpActiveSlot[2].hierarchy > OpActiveSlot[1].hierarchy &&
        OpActiveSlot[1].hierarchy > OpActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpActiveSlot[2];
            CurrentSlot[1] = OpActiveSlot[1];
            CurrentSlot[2] = OpActiveSlot[0];
        }
        else if (OpActiveSlot[2].hierarchy > OpActiveSlot[0].hierarchy &&
        OpActiveSlot[0].hierarchy > OpActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpActiveSlot[2];
            CurrentSlot[1] = OpActiveSlot[0];
            CurrentSlot[2] = OpActiveSlot[1];
        }
    }


    private void HierarchyOpMac()
    {
        if (OpMacActiveSlot[0].hierarchy > OpMacActiveSlot[1].hierarchy &&
    OpMacActiveSlot[1].hierarchy > OpMacActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpMacActiveSlot[0];
            CurrentSlot[1] = OpMacActiveSlot[1];
            CurrentSlot[2] = OpMacActiveSlot[2];
        }
        else if (OpMacActiveSlot[0].hierarchy > OpMacActiveSlot[2].hierarchy &&
        OpMacActiveSlot[2].hierarchy > OpMacActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpMacActiveSlot[0];
            CurrentSlot[1] = OpMacActiveSlot[2];
            CurrentSlot[2] = OpMacActiveSlot[1];
        }
        else if (OpMacActiveSlot[1].hierarchy > OpMacActiveSlot[2].hierarchy &&
        OpMacActiveSlot[2].hierarchy > OpMacActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpMacActiveSlot[1];
            CurrentSlot[1] = OpMacActiveSlot[2];
            CurrentSlot[2] = OpMacActiveSlot[0];
        }
        else if (OpMacActiveSlot[1].hierarchy > OpMacActiveSlot[0].hierarchy &&
        OpMacActiveSlot[0].hierarchy > OpMacActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpMacActiveSlot[0];
            CurrentSlot[1] = OpMacActiveSlot[2];
            CurrentSlot[2] = OpMacActiveSlot[1];
        }
        else if (OpMacActiveSlot[2].hierarchy > OpMacActiveSlot[1].hierarchy &&
        OpMacActiveSlot[1].hierarchy > OpMacActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpMacActiveSlot[2];
            CurrentSlot[1] = OpMacActiveSlot[1];
            CurrentSlot[2] = OpMacActiveSlot[0];
        }
        else if (OpMacActiveSlot[2].hierarchy > OpMacActiveSlot[0].hierarchy &&
        OpMacActiveSlot[0].hierarchy > OpMacActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpMacActiveSlot[2];
            CurrentSlot[1] = OpMacActiveSlot[0];
            CurrentSlot[2] = OpMacActiveSlot[1];
        }
    }


    private void HierarchyOpLaser()
    {
        if (OpLaserActiveSlot[0].hierarchy > OpLaserActiveSlot[1].hierarchy &&
    OpLaserActiveSlot[1].hierarchy > OpLaserActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpLaserActiveSlot[0];
            CurrentSlot[1] = OpLaserActiveSlot[1];
            CurrentSlot[2] = OpLaserActiveSlot[2];
        }
        else if (OpLaserActiveSlot[0].hierarchy > OpLaserActiveSlot[2].hierarchy &&
        OpLaserActiveSlot[2].hierarchy > OpLaserActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpLaserActiveSlot[0];
            CurrentSlot[1] = OpLaserActiveSlot[2];
            CurrentSlot[2] = OpLaserActiveSlot[1];
        }
        else if (OpLaserActiveSlot[1].hierarchy > OpLaserActiveSlot[2].hierarchy &&
        OpLaserActiveSlot[2].hierarchy > OpLaserActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpLaserActiveSlot[1];
            CurrentSlot[1] = OpLaserActiveSlot[2];
            CurrentSlot[2] = OpLaserActiveSlot[0];
        }
        else if (OpLaserActiveSlot[1].hierarchy > OpLaserActiveSlot[0].hierarchy &&
        OpLaserActiveSlot[0].hierarchy > OpLaserActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpLaserActiveSlot[0];
            CurrentSlot[1] = OpLaserActiveSlot[2];
            CurrentSlot[2] = OpLaserActiveSlot[1];
        }
        else if (OpLaserActiveSlot[2].hierarchy > OpLaserActiveSlot[1].hierarchy &&
        OpLaserActiveSlot[1].hierarchy > OpLaserActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpLaserActiveSlot[2];
            CurrentSlot[1] = OpLaserActiveSlot[1];
            CurrentSlot[2] = OpLaserActiveSlot[0];
        }
        else if (OpLaserActiveSlot[2].hierarchy > OpLaserActiveSlot[0].hierarchy &&
        OpLaserActiveSlot[0].hierarchy > OpLaserActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpLaserActiveSlot[2];
            CurrentSlot[1] = OpLaserActiveSlot[0];
            CurrentSlot[2] = OpLaserActiveSlot[1];
        }
    }

    private void HierarchyOpHexa()
    {
        if (OpHexaActiveSlot[0].hierarchy > OpHexaActiveSlot[1].hierarchy &&
    OpHexaActiveSlot[1].hierarchy > OpHexaActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpHexaActiveSlot[0];
            CurrentSlot[1] = OpHexaActiveSlot[1];
            CurrentSlot[2] = OpHexaActiveSlot[2];
        }
        else if (OpHexaActiveSlot[0].hierarchy > OpHexaActiveSlot[2].hierarchy &&
        OpHexaActiveSlot[2].hierarchy > OpHexaActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpHexaActiveSlot[0];
            CurrentSlot[1] = OpHexaActiveSlot[2];
            CurrentSlot[2] = OpHexaActiveSlot[1];
        }
        else if (OpHexaActiveSlot[1].hierarchy > OpHexaActiveSlot[2].hierarchy &&
        OpHexaActiveSlot[2].hierarchy > OpHexaActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpHexaActiveSlot[1];
            CurrentSlot[1] = OpHexaActiveSlot[2];
            CurrentSlot[2] = OpHexaActiveSlot[0];
        }
        else if (OpHexaActiveSlot[1].hierarchy > OpHexaActiveSlot[0].hierarchy &&
        OpHexaActiveSlot[0].hierarchy > OpHexaActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpHexaActiveSlot[0];
            CurrentSlot[1] = OpHexaActiveSlot[2];
            CurrentSlot[2] = OpHexaActiveSlot[1];
        }
        else if (OpHexaActiveSlot[2].hierarchy > OpHexaActiveSlot[1].hierarchy &&
        OpHexaActiveSlot[1].hierarchy > OpHexaActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpHexaActiveSlot[2];
            CurrentSlot[1] = OpHexaActiveSlot[1];
            CurrentSlot[2] = OpHexaActiveSlot[0];
        }
        else if (OpHexaActiveSlot[2].hierarchy > OpHexaActiveSlot[0].hierarchy &&
        OpHexaActiveSlot[0].hierarchy > OpHexaActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpHexaActiveSlot[2];
            CurrentSlot[1] = OpHexaActiveSlot[0];
            CurrentSlot[2] = OpHexaActiveSlot[1];
        }
    }

    private void HierarchyOpPress()
    {
        if (OpPressActiveSlot[0].hierarchy > OpPressActiveSlot[1].hierarchy &&
    OpPressActiveSlot[1].hierarchy > OpPressActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpPressActiveSlot[0];
            CurrentSlot[1] = OpPressActiveSlot[1];
            CurrentSlot[2] = OpPressActiveSlot[2];
        }
        else if (OpPressActiveSlot[0].hierarchy > OpPressActiveSlot[2].hierarchy &&
        OpPressActiveSlot[2].hierarchy > OpPressActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpPressActiveSlot[0];
            CurrentSlot[1] = OpPressActiveSlot[2];
            CurrentSlot[2] = OpPressActiveSlot[1];
        }
        else if (OpPressActiveSlot[1].hierarchy > OpPressActiveSlot[2].hierarchy &&
        OpPressActiveSlot[2].hierarchy > OpPressActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpPressActiveSlot[1];
            CurrentSlot[1] = OpPressActiveSlot[2];
            CurrentSlot[2] = OpPressActiveSlot[0];
        }
        else if (OpPressActiveSlot[1].hierarchy > OpPressActiveSlot[0].hierarchy &&
        OpPressActiveSlot[0].hierarchy > OpPressActiveSlot[2].hierarchy)
        {
            CurrentSlot[0] = OpPressActiveSlot[0];
            CurrentSlot[1] = OpPressActiveSlot[2];
            CurrentSlot[2] = OpPressActiveSlot[1];
        }
        else if (OpPressActiveSlot[2].hierarchy > OpPressActiveSlot[1].hierarchy &&
        OpPressActiveSlot[1].hierarchy > OpPressActiveSlot[0].hierarchy)
        {
            CurrentSlot[0] = OpPressActiveSlot[2];
            CurrentSlot[1] = OpPressActiveSlot[1];
            CurrentSlot[2] = OpPressActiveSlot[0];
        }
        else if (OpPressActiveSlot[2].hierarchy > OpPressActiveSlot[0].hierarchy &&
        OpPressActiveSlot[0].hierarchy > OpPressActiveSlot[1].hierarchy)
        {
            CurrentSlot[0] = OpPressActiveSlot[2];
            CurrentSlot[1] = OpPressActiveSlot[0];
            CurrentSlot[2] = OpPressActiveSlot[1];
        }
    }


    public void ButtonText(float price, TMP_Text text)
    {


        if (price >= 1000000000)
        {
            text.text = "$" + (price / 1000000000).ToString("0.#") + "B";


        }
        else
        if (price >= 1000000)
        {
            text.text = "$" + (price / 1000000).ToString("0.#") + "M";


        }
        else
        if (price >= 1000)
        {
            text.text = "$" + (price / 1000).ToString("0.#") + "K";


        }
        else
        {
            text.text = "$" + price.ToString("0.#");
        }
    }

    private void ButtonClose0()
    {
        gameManager.UpTextObj.SetActive(false);
        gameManager.UpBack.enabled = false;
        gameManager.UpgradeButton.enabled = false;
        gameManager.UpgradeButton.image.enabled = false;

        gameManager.AddOpButton.enabled = false;
        // FindObjectOfType<GameManager>().AddOpButton.image.enabled = false;

        gameManager.AddStandButton.enabled = false;
        gameManager.AddStandButton.image.enabled = false;

    }


    private void ButtonClose()
    {
        gameManager.UpTextObj.SetActive(false);
        gameManager.UpBack.enabled = false;
        gameManager.UpgradeButton.enabled = false;
        gameManager.UpgradeButton.image.enabled = false;

        gameManager.AddOpButton.enabled = false;
        gameManager.AddOpButton.image.enabled = false;

        gameManager.AddStandButton.enabled = false;
        gameManager.AddStandButton.image.enabled = false;

    }

    private void ButtonOpen()
    {/*
        if (OpCount < 3 && OpMacCount < 3 && OpLaserCount < 3 && OpHexaCount < 3 && OpPressCount < 3 && OpTermalCount < 3)
        {
            FindObjectOfType<GameManager>().UpTextObj.SetActive(false);
            FindObjectOfType<GameManager>().UpBack.enabled = false;
            FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
            FindObjectOfType<GameManager>().UpgradeButton.image.enabled = false;

        }
        else
         if (money < _currentUpPrice)
        {

            FindObjectOfType<GameManager>().UpTextObj.SetActive(true);
            FindObjectOfType<GameManager>().UpBack.enabled = true;
            FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
            FindObjectOfType<GameManager>().UpgradeButton.image.enabled = false;



        }
        else
        {
            FindObjectOfType<GameManager>().UpTextObj.SetActive(true);
            FindObjectOfType<GameManager>().UpBack.enabled = true;
            FindObjectOfType<GameManager>().UpgradeButton.enabled = true;
            FindObjectOfType<GameManager>().UpgradeButton.image.enabled = true;
        }

        FindObjectOfType<GameManager>().AddOpButton.enabled = true;
        FindObjectOfType<GameManager>().AddOpButton.image.enabled = true;

        FindObjectOfType<GameManager>().AddStandButton.enabled = true;
        FindObjectOfType<GameManager>().AddStandButton.image.enabled = true;*/


        if (!progress)
        {

            if (OpCount < 3 && OpMacCount < 3 && OpLaserCount < 3 && OpHexaCount < 3 && OpPressCount < 3 )
            {
                gameManager.UpTextObj.SetActive(false);
                gameManager.UpBack.enabled = false;
                gameManager.UpgradeButton.enabled = false;
                gameManager.UpgradeButton.image.enabled = false;

            }
            else
            if (money < _currentUpPrice)
            {

                gameManager.UpTextObj.SetActive(true);
                gameManager.UpBack.enabled = true;
                gameManager.UpgradeButton.enabled = false;
                gameManager.UpgradeButton.image.enabled = false;



            }
            else
            {
                gameManager.UpTextObj.SetActive(true);
                gameManager.UpBack.enabled = true;
                gameManager.UpgradeButton.enabled = true;
                gameManager.UpgradeButton.image.enabled = true;
                if (tutorial.gameObject.activeSelf == true)
                {
                    if (OpMacActiveSlot.Count < 1)
                    {
                        tutorial.MergeImageOpen();
                    }
                }
            }



            if (money < _currentOpPrice || EmptySlot.Count < 1)
            {
                gameManager.AddOpButton.enabled = false;
                gameManager.AddOpButton.image.enabled = false;

            }
            else
            {
                gameManager.AddOpButton.enabled = true;
                gameManager.AddOpButton.image.enabled = true;

                if (tutorial.gameObject.activeSelf == true)
                {
                    if (OpActiveSlot.Count < 3)
                    {
                        tutorial.OpImageOpen();
                    }
                }
            }


            if (money < _currentStandPrice || StandValue >= Stand.Length - 1)
            {
                gameManager.AddStandButton.enabled = false;
                gameManager.AddStandButton.image.enabled = false;
            }
            else
            {
                gameManager.AddStandButton.enabled = true;
                gameManager.AddStandButton.image.enabled = true;
            }





        }
    }


    public void MoneyUp()
    {
        money += 1000000;
    }

    public void MoneyInflate()
    {
        if (!inflateMoney)
        {
            inflateMoney = true;
            StartCoroutine(MoneyInflating());
        }
    }

    IEnumerator MoneyInflating()
    {

        moneyImage.DOScale(Vector3.one * 1.2f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        moneyImage.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.1f);
        inflateMoney = false;



    }


    public void Save()
    {

        PlayerPrefs.SetFloat(_keyMoney, money);
        PlayerPrefs.SetInt(_keyN, n);
        PlayerPrefs.SetInt(_keyN1, n1);




        PlayerPrefs.SetFloat(_keyCurrentOpPrice, _currentOpPrice);
        PlayerPrefs.SetFloat(_keyCurrentUpPrice, _currentUpPrice);
        PlayerPrefs.SetFloat(_keyCurrentStandPrice, _currentStandPrice);
        PlayerPrefs.SetInt(_keyStandValue, StandValue);
        PlayerPrefs.SetFloat(_keyMoneyPer, moneyPer);
        PlayerPrefs.SetInt(_keyBoxCapacity, boxCapacity);

        PlayerPrefs.SetFloat("MainMoney", gameManager.money);
    }


    public void Load()
    {

        _keyMoneyPer = economy._keyMoneyPer;
        _keyMoneyPer = economy._keyMoneyPer;
        _keyCurrentOpPrice = economy._keyCurrentOpPrice;
        _keyCurrentUpPrice = economy._keyCurrentUpPrice;
        _keyCurrentStandPrice = economy._keyCurrentStandPrice;

        opPriceVar = economy.opPriceVar;
        upgradePriceVar = economy.upgradeVar;
        standPriceVar = economy.standPriceVar;



        opVar1 = economy.opVar1;
        opVar2 = economy.opVar2;
        upVar1 = economy.upVar1;
        upVar2 = economy.upVar2;
        opClickCapacity = economy.opClickCapacity;
        upClickCapacity = economy.upClickCapacity;
        productSpeedDown = economy.productSpeedDown;
        productSpeed = productSpeedDown;
        progress = false;
        progress2 = false;
        n = PlayerPrefs.GetInt(_keyN);
        n1 = PlayerPrefs.GetInt(_keyN1);

        if (gameManager.FactoryValue == 0)
        {
            if (PlayerPrefs.HasKey(_keyCurrentOpPrice))
            {
                _currentOpPrice = PlayerPrefs.GetFloat(_keyCurrentOpPrice);
            }
            else
            {
                _currentOpPrice = economy.opPriceVar;
            }


            if (PlayerPrefs.HasKey(_keyCurrentUpPrice))
            {
                _currentUpPrice = PlayerPrefs.GetFloat(_keyCurrentUpPrice);
            }
            else
            {
                _currentUpPrice = economy.upgradeVar;
            }


            if (PlayerPrefs.HasKey(_keyCurrentStandPrice))
            {
                _currentStandPrice = PlayerPrefs.GetFloat(_keyCurrentStandPrice);
            }
            else
            {
                _currentStandPrice = economy.standPriceVar;
            }

        }
        else if (gameManager.FactoryValue == 1)
        {
            if (PlayerPrefs.HasKey(_keyCurrentOpPrice))
            {
                _currentOpPrice = PlayerPrefs.GetFloat(_keyCurrentOpPrice);
              
            }
            else
            {

                
                _currentOpPrice = PlayerPrefs.GetFloat("COP_0");
       
            }


            if (PlayerPrefs.HasKey(_keyCurrentUpPrice))
            {
                _currentUpPrice = PlayerPrefs.GetFloat(_keyCurrentUpPrice);
            }
            else
            {
                _currentUpPrice = PlayerPrefs.GetFloat("CUP_0");
            }


            if (PlayerPrefs.HasKey(_keyCurrentStandPrice))
            {
                _currentStandPrice = PlayerPrefs.GetFloat(_keyCurrentStandPrice);
            }
            else
            {
                _currentStandPrice = PlayerPrefs.GetFloat("CSP_0");
            }

        }
        else if (gameManager.FactoryValue == 2)
        {
            if (PlayerPrefs.HasKey(_keyCurrentOpPrice))
            {
                _currentOpPrice = PlayerPrefs.GetFloat(_keyCurrentOpPrice);
            }
            else
            {
                _currentOpPrice = PlayerPrefs.GetFloat("COP_1");
            }


            if (PlayerPrefs.HasKey(_keyCurrentUpPrice))
            {
                _currentUpPrice = PlayerPrefs.GetFloat(_keyCurrentUpPrice);
            }
            else
            {
                _currentUpPrice = PlayerPrefs.GetFloat("CUP_1");
            }


            if (PlayerPrefs.HasKey(_keyCurrentStandPrice))
            {
                _currentStandPrice = PlayerPrefs.GetFloat(_keyCurrentStandPrice);
            }
            else
            {
                _currentStandPrice = PlayerPrefs.GetFloat("CSP_1");
            }

        }
        else if (gameManager.FactoryValue == 3)
        {
            if (PlayerPrefs.HasKey(_keyCurrentOpPrice))
            {
                _currentOpPrice = PlayerPrefs.GetFloat(_keyCurrentOpPrice);
            }
            else
            {
                _currentOpPrice = PlayerPrefs.GetFloat("COP_2");
            }


            if (PlayerPrefs.HasKey(_keyCurrentUpPrice))
            {
                _currentUpPrice = PlayerPrefs.GetFloat(_keyCurrentUpPrice);
            }
            else
            {
                _currentUpPrice = PlayerPrefs.GetFloat("CUP_2");
            }


            if (PlayerPrefs.HasKey(_keyCurrentStandPrice))
            {
                _currentStandPrice = PlayerPrefs.GetFloat(_keyCurrentStandPrice);
            }
            else
            {
                _currentStandPrice = PlayerPrefs.GetFloat("CSP_2");
            }

        }







        if (PlayerPrefs.HasKey("MainMoney") ==false)
        {
          money = 20;
            for (int i = 1; i < 3; i++)
            {

                // Slot[i] = Stand[0].Slot[i];


                EmptySlot.Add(Slot[i]);

            }
            boxCapacity = 10;
            //_currentOpPrice = opPriceVar;
            //_currentUpPrice = upgradePriceVar;
            //_currentStandPrice = standPriceVar;
            x = 0;
            n = 1;
            n1 = 1;

        }
        else
        {
          
                money = PlayerPrefs.GetFloat("MainMoney");

 

            //_currentOpPrice = PlayerPrefs.GetInt(_keyCurrentOpPrice);
            //_currentUpPrice = PlayerPrefs.GetInt(_keyCurrentUpPrice);
            //_currentStandPrice = PlayerPrefs.GetInt(_keyCurrentStandPrice);
            StandValue = PlayerPrefs.GetInt(_keyStandValue);
            boxCapacity = PlayerPrefs.GetInt(_keyBoxCapacity);
            x = PlayerPrefs.GetInt(_keyX);

            if (StandValue > 0)
            {
                for (int i = 1; i < StandValue + 1; i++)
                {
                    Stand[i].transform.localScale = Vector3.one;
                    Stand[i].gameObject.SetActive(true);
                    Stand[i].Box.SetActive(true);

                    for (int x = i - 1; x > -1; x--)
                    {
                        Stand[x].Box.SetActive(false);
                    }
                }
            }



            for (int i = 1; i < 28; i++)
            {

                if (Slot[i].type == 6)
                {
                    OpTermalActiveSlot.Add(Slot[i]);
                    Slot[i].OpTermal.transform.localScale = Vector3.one;
                    Slot[i].OpTermal.gameObject.SetActive(true);
                    OpTermalCount = OpTermalActiveSlot.Count;
                }
                else
                if (Slot[i].type == 5)
                {
                    OpPressActiveSlot.Add(Slot[i]);
                    Slot[i].OpPress.transform.localScale = Vector3.one;
                    Slot[i].OpPress.gameObject.SetActive(true);
                    OpPressCount = OpPressActiveSlot.Count;
                }
                else
                if (Slot[i].type == 4)
                {
                    OpHexaActiveSlot.Add(Slot[i]);
                    Slot[i].OpHexa.transform.localScale = Vector3.one;
                    Slot[i].OpHexa.gameObject.SetActive(true);
                    OpHexaCount = OpHexaActiveSlot.Count;
                }
                else
                if (Slot[i].type == 3)
                {
                    OpLaserActiveSlot.Add(Slot[i]);
                    Slot[i].OpLaser.transform.localScale = Vector3.one;
                    Slot[i].OpLaser.gameObject.SetActive(true);
                    OpLaserCount = OpLaserActiveSlot.Count;
                }
                else
                if (Slot[i].type == 2)
                {
                    OpMacActiveSlot.Add(Slot[i]);
                    Slot[i].OpMachine.transform.localScale = Vector3.one;
                    Slot[i].OpMachine.gameObject.SetActive(true);
                    OpMacCount = OpMacActiveSlot.Count;
                }
                else
                if (Slot[i].type == 1)
                {
                    OpActiveSlot.Add(Slot[i]);
                    Slot[i].Operator.transform.localScale = Vector3.one;
                    Slot[i].Operator.gameObject.SetActive(true);
                    OpCount = OpActiveSlot.Count;
                }
                else
                if (Slot[i].type == 0)
                {
                    if (Slot[i].transform.parent.gameObject.activeSelf == true)
                    {
                        OpActiveSlot.Remove(Slot[i]);
                        EmptySlot.Add(Slot[i]);
                        Slot[i].Operator.transform.localScale = Vector3.zero;
                        Slot[i].Operator.gameObject.SetActive(false);
                    }
                }
            }









        }


        _mainCam.transform.position = PosCam2[StandValue];
        // _mainCam.transform.rotation = Quaternion.Euler(RotCam[StandValue]);
        _mainCam.orthographicSize = _camSize[StandValue];

        ButtonText(_currentOpPrice, _opText);


        ButtonText(_currentUpPrice, _mergeText);


        if (StandValue < Stand.Length - 1)
        {
            ButtonText(_currentStandPrice, _standText);
        }
        else
        {
            _standText.text = "MAX";
        }
    }


    IEnumerator MoneyEnter()
    {

        while (true)
        {


            yield return new WaitForSeconds(1);

            for (int i = 0; i < 4; i++)
            {
                if (i != gameManager.FactoryValue)
                {
                    money += MoneyPer[i];

                   
                }
            }

            /*
            yield return new WaitForSeconds(0.05f);
        
            for (int i = 0; i < 4; i++)
            {
                if(i!= gameManager.FactoryValue)
                {
                    money += MoneyPer[i]*0.25f;

                    Debug.Log($"MoneyPer{i}: " + MoneyPer[i]);
                }
            }
            yield return new WaitForSeconds(0.05f);
            for (int i = 0; i < 4; i++)
            {
                if (i != gameManager.FactoryValue)
                {
                    money += MoneyPer[i] * 0.25f;

                    Debug.Log($"MoneyPer{i}: " + MoneyPer[i]);
                }
            }
            yield return new WaitForSeconds(0.05f);
            for (int i = 0; i < 4; i++)
            {
                if (i != gameManager.FactoryValue)
                {
                    money += MoneyPer[i] * 0.25f;

                    Debug.Log($"MoneyPer{i}: " + MoneyPer[i]);
                }
            }
            yield return new WaitForSeconds(0.05f);
            for (int i = 0; i < 4; i++)
            {
                if (i != gameManager.FactoryValue)
                {
                    money += MoneyPer[i] * 0.25f;

                    Debug.Log($"MoneyPer{i}: " + MoneyPer[i]);
                }
            }*/

        }
    }


}


