using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FactoryManagerV2 : MonoBehaviour
{
    [SerializeField] Vector3[] PosCam;
    [SerializeField] Vector3[] PosCam2;
    [SerializeField] Vector3[] RotCam;
    [SerializeField] Camera _mainCam;
    [SerializeField] float[] _camSize;

    [SerializeField] StandManager[] Stand;
    [SerializeField] OperatorController[] Operator;
    [SerializeField] OperatorController[] OpMachine;
    [SerializeField] OperatorController[] OpLaser;
    [SerializeField] OperatorController[] OpHexa;
    [SerializeField] OperatorController[] OpPress;
    [SerializeField] OperatorController[] OpTermal;


    [SerializeField] SlotManager[] Slot;
    [SerializeField] SlotManager[] CurrentSlot = new SlotManager[3];

    [SerializeField] List<SlotManager> EmptySlot;
    [SerializeField] List<SlotManager> OpActiveSlot;
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
    public int moneyPast;
    public int moneyPer;
    public int n;
    public int opPriceVar;
    public int standPriceVar;
    public int upgradePriceVar;

    public int opVar1;
    public int opVar2;

    public int upVar1;
    public int upVar2;


    public int standVar1;
    public int standVar2;



    [SerializeField] int _currentOpPrice;

    [SerializeField] int _currentUpPrice;

    [SerializeField] int _currentStandPrice = 2000;

    bool progress;
    bool progress2;


    public int opClickCapacity;

    public int upClickCapacity;


    [SerializeField] int progressCount;
    [SerializeField] int UpgradeCount;
    public BoxController Box;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam.transform.position = PosCam2[StandValue];
        _mainCam.transform.rotation = Quaternion.Euler(RotCam[StandValue]);
        _mainCam.orthographicSize = _camSize[StandValue];


        StartCoroutine(Spawning());

        
        for (int i = 0; i < 3; i++)
        {

            Slot[i] = Stand[0].Slot[i];


            EmptySlot.Add(Slot[i]);

        }


        for (int i = 0; i < 28; i++)
        {
            Slot[i].hierarchy = i;
        }

        //opPriceVar = FindObjectOfType<EconomyManager>().opPriceVar;
        //standPriceVar = FindObjectOfType<EconomyManager>().standPriceVar;
        //upgradePriceVar = FindObjectOfType<EconomyManager>().upgradeVar;
        opVar1 = FindObjectOfType<EconomyManager>().opVar1;
        opVar2 = FindObjectOfType<EconomyManager>().opVar2;
        upVar1 = FindObjectOfType<EconomyManager>().upVar1;
        upVar2 = FindObjectOfType<EconomyManager>().upVar2;
        money = 20;
        _currentOpPrice = opPriceVar;
        _currentUpPrice = upgradePriceVar;
        _currentStandPrice = standPriceVar;
        opClickCapacity = FindObjectOfType<EconomyManager>().opClickCapacity;
        upClickCapacity = FindObjectOfType<EconomyManager>().upClickCapacity;

        progress = false;
        progress2 = false;

    }

    // Update is called once per frame
    void Update()
    {
        _mainCam.orthographicSize = Mathf.Lerp(_mainCam.orthographicSize, _camSize[StandValue], Time.deltaTime * 2);


        tapSpeed = Mathf.Lerp(tapSpeed, 0, Time.deltaTime);
        productSpeed = Mathf.Lerp(productSpeed, productSpeedDown, Time.deltaTime);

        moneyPer = OpActiveSlot.Count + OpMacActiveSlot.Count * 5 + OpLaserActiveSlot.Count * 25 + OpHexaActiveSlot.Count * 125 + OpPressActiveSlot.Count * 625 + OpTermalActiveSlot.Count * 3125;

            if ((OpCount < 3 && OpMacCount < 3 && OpLaserCount < 3 && OpHexaCount < 3 && OpPressCount < 3 && OpTermalCount < 3) || money < _currentUpPrice)
            {
            FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
                FindObjectOfType<GameManager>().UpgradeButton.image.enabled = false;
            }
            else
            {

                FindObjectOfType<GameManager>().UpgradeButton.enabled = true;
                FindObjectOfType<GameManager>().UpgradeButton.image.enabled = true;
            }


       
            if (money < _currentOpPrice|| EmptySlot.Count < 1)
            {
            FindObjectOfType<GameManager>().AddOpButton.enabled = false;
            FindObjectOfType<GameManager>().AddOpButton.image.enabled = false;

            }
            else
            {
            FindObjectOfType<GameManager>().AddOpButton.enabled = true;
            FindObjectOfType<GameManager>().AddOpButton.image.enabled = true;
            }
        

            if(money< _currentStandPrice)
            {
            FindObjectOfType<GameManager>().AddStandButton.enabled = false;
            FindObjectOfType<GameManager>().AddStandButton.image.enabled = false;
            }
            else
            {
            FindObjectOfType<GameManager>().AddStandButton.enabled = true;
            FindObjectOfType<GameManager>().AddStandButton.image.enabled = true;
        }


        if (progressCount > 0 && !progress)
        {
            AddOperator2();
        }


        if (UpgradeCount > 0 && !progress && !progress2)
        {
            Upgrade2();
        }

    }



    public void AddStand()
    {
        if (StandValue < Stand.Length - 1&&money>=_currentStandPrice)
        {
            money -= _currentStandPrice;
            _currentStandPrice = _currentStandPrice * 2 + 4000;
         

            Stand[StandValue].Box.SetActive(false);
            //Stand[StandValue].TurnPiece.SetActive(true);
            StandValue++;
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
        if (progressCount < opClickCapacity)
        {
            if (money >= _currentOpPrice)
            {
                progressCount++;
                money -= _currentOpPrice;
                _currentOpPrice += (opVar1 + opVar2 * n);

            }
        }
        

    }

    public void AddOperator2()
    {

        if (EmptySlot.Count > 0)
        {
            
            StartCoroutine(AddingOperator());
        }

    }





    public void Upgrade()
    {

        if (UpgradeCount < upClickCapacity)
        {
            if (money > _currentUpPrice)
            {
                UpgradeCount++;

            }
        }


    }







    public void Upgrade2()
    {



        if (OpCount > 2)
        {

            money -= _currentUpPrice;
            _currentUpPrice += (upVar1 + upVar2 * n);



            HierarchyOp();
     

            StartCoroutine(AddingMachine(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));


        }
        else
        if (OpMacCount > 2)
        {
            
            money -= _currentUpPrice;
            _currentUpPrice += (upVar1 + upVar2 * n);
            
            
            HierarchyOpMac();

   

            StartCoroutine(AddingLaser(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));

        }
        else


        if (OpLaserCount > 2)
        {
            money -= _currentUpPrice;
            _currentUpPrice += (upVar1 + upVar2 * n);

            HierarchyOpLaser();
            StartCoroutine(AddingHexa(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));

        }
        else


        if (OpHexaCount > 2)
        {
            money -= _currentUpPrice;
            _currentUpPrice += (upVar1 + upVar2 * n);


            HierarchyOpHexa();
            StartCoroutine(AddingPress(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));


        }
        else

        if (OpPressCount > 2)
        {
            money -= _currentUpPrice;
            _currentUpPrice += (upVar1 + upVar2 * n);

            HierarchyOpPress();
            StartCoroutine(AddingTermal(CurrentSlot[2], CurrentSlot[1], CurrentSlot[0]));

        }










    }







    IEnumerator AddingStand()
    {
        progress = true;
        FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
        FindObjectOfType<GameManager>().AddOpButton.enabled = false;
        FindObjectOfType<GameManager>().AddStandButton.enabled = false;
        Stand[StandValue].gameObject.SetActive(true);
        Stand[StandValue].transform.DOScale(Vector3.one * 1.4f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        Stand[StandValue].transform.DOScale(Vector3.one, 0.2f);
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<GameManager>().AddStandButton.enabled = true;
        FindObjectOfType<GameManager>().AddOpButton.enabled = true;
        FindObjectOfType<GameManager>().AddOpButton.enabled = true;
        progress = false;
    
    }


    IEnumerator AddingOperator()
    {

        progress = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = false;
        //FindObjectOfType<GameManager>().AddStandButton.enabled = false;

        EmptySlot[0].Operator.gameObject.SetActive(true);
        EmptySlot[0].Operator.transform.DOScale(Vector3.one * 1.4f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        EmptySlot[0].Operator.transform.DOScale(Vector3.one, 0.1f);
        //FindObjectOfType<GameManager>().AddStandButton.enabled = true;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = true;
        yield return new WaitForSeconds(0.1f);
        
        OpActiveSlot.Add(EmptySlot[0]);
        EmptySlot.RemoveAt(0);
        OpCount++;
        progressCount--;
        progress = false;
        n++;

    }

    IEnumerator AddingMachine(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress2 = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = false;
        //FindObjectOfType<GameManager>().AddStandButton.enabled = false;

        slot0.Operator.transform.DOMove(slot2.Operator.transform.position, 0.2f);
        slot1.Operator.transform.DOMove(slot2.Operator.transform.position, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot0.Operator.transform.localScale = Vector3.zero;
        slot1.Operator.transform.localScale = Vector3.zero;
        slot2.Operator.transform.localScale = Vector3.zero;
        slot0.Operator.gameObject.SetActive(false);
        slot1.Operator.gameObject.SetActive(false);
        slot2.Operator.gameObject.SetActive(false);
        slot2.OpMachine.gameObject.SetActive(true);
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
        //FindObjectOfType<GameManager>().AddStandButton.enabled = true;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = true;
        UpgradeCount--;
        n++;
        progress2 = false;
    }

    IEnumerator AddingLaser(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress2 = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = false;
        //FindObjectOfType<GameManager>().AddStandButton.enabled = false;

        slot0.OpMachine.transform.DOMove(slot2.OpMachine.transform.position, 0.2f);
        slot1.OpMachine.transform.DOMove(slot2.OpMachine.transform.position, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot0.OpMachine.transform.localScale = Vector3.zero;
        slot1.OpMachine.transform.localScale = Vector3.zero;
        slot2.OpMachine.transform.localScale = Vector3.zero;
        slot0.OpMachine.gameObject.SetActive(false);
        slot1.OpMachine.gameObject.SetActive(false);
        slot2.OpMachine.gameObject.SetActive(false);
        slot2.OpLaser.gameObject.SetActive(true);
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
        //FindObjectOfType<GameManager>().AddStandButton.enabled = true;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = true;
        UpgradeCount--;
        n++;
        progress2 = false;

    }


    IEnumerator AddingHexa(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress2 = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = false;
        //FindObjectOfType<GameManager>().AddStandButton.enabled = false;

        slot0.OpLaser.transform.DOMove(slot2.OpLaser.transform.position, 0.2f);
        slot1.OpLaser.transform.DOMove(slot2.OpLaser.transform.position, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot0.OpLaser.transform.localScale = Vector3.zero;
        slot1.OpLaser.transform.localScale = Vector3.zero;
        slot2.OpLaser.transform.localScale = Vector3.zero;
        slot0.OpLaser.gameObject.SetActive(false);
        slot1.OpLaser.gameObject.SetActive(false);
        slot2.OpLaser.gameObject.SetActive(false);
        slot2.OpHexa.gameObject.SetActive(true);
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
        //FindObjectOfType<GameManager>().AddStandButton.enabled = true;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = true;
        UpgradeCount--;
        n++;
        progress2 = false;


    }


    IEnumerator AddingPress(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress2 = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = false;
        //FindObjectOfType<GameManager>().AddStandButton.enabled = false;

        slot0.OpHexa.transform.DOMove(slot2.OpHexa.transform.position, 0.2f);
        slot1.OpHexa.transform.DOMove(slot2.OpHexa.transform.position, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot0.OpHexa.transform.localScale = Vector3.zero;
        slot1.OpHexa.transform.localScale = Vector3.zero;
        slot2.OpHexa.transform.localScale = Vector3.zero;
        slot0.OpHexa.gameObject.SetActive(false);
        slot1.OpHexa.gameObject.SetActive(false);
        slot2.OpHexa.gameObject.SetActive(false);
        slot2.OpPress.gameObject.SetActive(true);
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
        //FindObjectOfType<GameManager>().AddStandButton.enabled = true;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = true;
        UpgradeCount--;
        n++;
        progress2 = false;

    }

    IEnumerator AddingTermal(SlotManager slot0, SlotManager slot1, SlotManager slot2)
    {
        progress2 = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = false;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = false;
        //FindObjectOfType<GameManager>().AddStandButton.enabled = false;

        slot0.OpPress.transform.DOMove(slot2.OpPress.transform.position, 0.2f);
        slot1.OpPress.transform.DOMove(slot2.OpPress.transform.position, 0.2f);
        yield return new WaitForSeconds(0.2f);
        slot0.OpPress.transform.localScale = Vector3.zero;
        slot1.OpPress.transform.localScale = Vector3.zero;
        slot2.OpPress.transform.localScale = Vector3.zero;
        slot0.OpPress.gameObject.SetActive(false);
        slot1.OpPress.gameObject.SetActive(false);
        slot2.OpPress.gameObject.SetActive(false);
        slot2.OpTermal.gameObject.SetActive(true);
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
        //FindObjectOfType<GameManager>().AddStandButton.enabled = true;
        //FindObjectOfType<GameManager>().AddOpButton.enabled = true;
        //FindObjectOfType<GameManager>().UpgradeButton.enabled = true;
        UpgradeCount--;
        n++;
        progress2 = false;

    }



    IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(productSpeed);
            Instantiate(_product, _enter.position + Vector3.up * 10, Quaternion.identity);

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
}
