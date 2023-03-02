using UnityEngine;


public class SlotManager : MonoBehaviour
{
    public Transform slotTransform;





    public bool empty;

    public OperatorController Operator;
    public OperatorController OpMachine;
    public OperatorController OpLaser;
    public OperatorController OpHexa;
    public OperatorController OpPress;
    public OperatorController OpTermal;


    public int hierarchy;

   

    public ParticleSystem mergeParticle;

    // Start is called before the first frame update
    public int type;
    public  string typeString;






    private void Awake()
    {
        typeString +="_"+ transform.parent.transform.parent.GetComponent<FactoryManager>().factoryNum.ToString();

  
        

        slotTransform = transform;
        if (transform.parent.transform.parent.GetComponent<FactoryManager>().factoryNum == 0 || transform.parent.transform.parent.GetComponent<FactoryManager>().factoryNum == 2)
        {
            Operator = transform.GetChild(0).GetComponent<OperatorController>();
            OpMachine = transform.GetChild(1).GetComponent<OperatorController>();
            OpLaser = transform.GetChild(2).GetComponent<OperatorController>();
            OpHexa = transform.GetChild(3).GetComponent<OperatorController>();
            OpPress = transform.GetChild(4).GetComponent<OperatorController>();
            OpTermal = transform.GetChild(5).GetComponent<OperatorController>();
        }
        else
        {
            Operator = transform.GetChild(0).GetComponent<OperatorController>();
            OpMachine = transform.GetChild(6).GetComponent<OperatorController>();
            OpLaser = transform.GetChild(7).GetComponent<OperatorController>();
            OpHexa = transform.GetChild(8).GetComponent<OperatorController>();
            OpPress = transform.GetChild(9).GetComponent<OperatorController>();
            OpTermal = transform.GetChild(10).GetComponent<OperatorController>();
        }






        if (type == 1)
        {
            if (PlayerPrefs.HasKey(typeString) == true&& PlayerPrefs.GetInt(typeString) ==0&& Operator.gameObject.activeSelf == true)
            {
                transform.parent.transform.parent.GetComponent<FactoryManager>().OpActiveSlot.Remove(GetComponent<SlotManager>());
                transform.parent.transform.parent.GetComponent<FactoryManager>().EmptySlot.Add(GetComponent<SlotManager>());
                transform.parent.transform.parent.GetComponent<FactoryManager>().OpCount -= 1;
                type = 0;
                Operator.gameObject.SetActive(false);
                Operator.transform.localScale = Vector3.zero;

                Debug.Log("/////////////////////////////////////////////////////////////////////");
            }
           

        }else
        if (type == 0)
        {
                type = PlayerPrefs.GetInt(typeString);
        
        }
      


        //if (type == 0)
        //{
        //    if (PlayerPrefs.GetInt(typeString) != 0)
        //    {
        //        type = PlayerPrefs.GetInt(typeString);
        //    }
        //    else
        //    {
        // 
        //    }

        //}



    }
  


    public void SaveType()
    {
        PlayerPrefs.SetInt(typeString, type);
    }

}
