using UnityEngine;
using TMPro;

public class OperatorController : MonoBehaviour
{

    public Vector3 pFirst;
    public Transform opTransform;
    //public int hierarchy;

    public int OpPayment;
    public int FormValue;
    public int CurPayment;


    [SerializeField] GameObject OpObj;
    [SerializeField] GameObject OpObj1;
    private Animator animator;
    private Animator animator1;



    [SerializeField] GameObject _money;
    public TMP_Text moneyText;
    [SerializeField] float y0;
    [SerializeField] float y1;
    [SerializeField] Vector3 _moneyPos;
    [SerializeField] Vector3 _moneyRot;

    public float currentPayment;


    public ParticleSystem moneyParticle;
    public ParticleSystem moneyParticle2;

    [SerializeField] Vector3 moneySize;


    public int i;

    public static bool fast;
    public static bool tapFast;
    public static bool doublePay;

    public string animString;

    public bool version2;

    [SerializeField] GameObject[] MoneyParticlesX1L0;
    [SerializeField] GameObject[] MoneyParticlesX2L0;

    [SerializeField] GameObject[] MoneyParticlesX1L1;
    [SerializeField] GameObject[] MoneyParticlesX2L1;


    [SerializeField] GameObject[] MoneyParticlesX1L2;
    [SerializeField] GameObject[] MoneyParticlesX2L2;


    [SerializeField] GameObject[] MoneyParticlesX1L3;
    [SerializeField] GameObject[] MoneyParticlesX2L3;



    [SerializeField] Vector3 _particlePos;
    [SerializeField] int opType;

    [SerializeField] Vector3 partScale0;
    [SerializeField] Vector3 partScale1;

    // Start is called before the first frame update
    void Start()
    {
        pFirst = transform.localPosition;
        opTransform = transform;

        if (FindObjectOfType<GameManager>()._currentFactory.factoryNum==3&&OpObj1 !=null)
        {
           
            OpObj = OpObj1;
           
        }
        OpObj.SetActive(true);
        animator = OpObj.GetComponent<Animator>();

        SwitchParticle();

        
        moneyParticle.transform.localScale = partScale0* Camera.main.orthographicSize * 0.06f;
        moneyParticle2.transform.localScale = partScale1* Camera.main.orthographicSize * 0.06f;
    }





    public void Product()
    {
        if (fast && tapFast)
        {
            animString = "Working2";
        }
        else if(tapFast||fast) {

            animString = "Working1";
        }
        else
        {
            animString = "Working";
        }


       
            animator.SetTrigger(animString);
            animator.SetTrigger("Idle");

     

        //animator.SetTrigger("Working1");
        //animator.SetTrigger("Idle");
        //}
        //else
        //{

        //    animator.SetTrigger("Working");
        //    animator.SetTrigger("Idle");
        //}
       // moneyParticle.transform.localScale = moneySize * Camera.main.orthographicSize * 0.06f;
        //moneyParticle2.transform.localScale = moneySize * Camera.main.orthographicSize * 0.06f;

        moneyParticle.transform.localScale = partScale0* Camera.main.orthographicSize * 0.06f;
        moneyParticle2.transform.localScale = partScale1* Camera.main.orthographicSize * 0.06f;

        if (doublePay)
        {
            moneyParticle2.Play();
        }
        else
        {
            moneyParticle.Play();
        }
        
        FindObjectOfType<GameManager>()._currentFactory.MoneyInflate();

    }


    void SwitchParticle()
    {
        if (FindObjectOfType<GameManager>().FactoryValue == 0)
        {
            GameObject particle1 = Instantiate(MoneyParticlesX1L0[opType], transform.position + _particlePos, Quaternion.Euler(-90, 0, 0), transform);
            GameObject particle2 = Instantiate(MoneyParticlesX2L0[opType], transform.position + _particlePos, Quaternion.Euler(-90, 0, 0), transform);

            particle1.transform.localPosition = _particlePos;
            particle2.transform.localPosition = _particlePos;

            moneyParticle = particle1.GetComponent<ParticleSystem>();
            moneyParticle2 = particle2.GetComponent<ParticleSystem>();
        }
        else
       if (FindObjectOfType<GameManager>().FactoryValue == 2)
        {
            GameObject particle1 = Instantiate(MoneyParticlesX1L1[opType], transform.position + _particlePos, Quaternion.Euler(-90, 0, 0), transform);
            GameObject particle2 = Instantiate(MoneyParticlesX2L1[opType], transform.position + _particlePos, Quaternion.Euler(-90, 0, 0), transform);

            particle1.transform.localPosition = _particlePos;
            particle2.transform.localPosition = _particlePos;

            moneyParticle = particle1.GetComponent<ParticleSystem>();
            moneyParticle2 = particle2.GetComponent<ParticleSystem>();
        }
        else
       if (FindObjectOfType<GameManager>().FactoryValue == 1)
        {
            GameObject particle1 = Instantiate(MoneyParticlesX1L2[opType], transform.position + _particlePos, Quaternion.Euler(-90, 0, 0), transform);
            GameObject particle2 = Instantiate(MoneyParticlesX2L2[opType], transform.position + _particlePos, Quaternion.Euler(-90, 0, 0), transform);

            particle1.transform.localPosition = _particlePos;
            particle2.transform.localPosition = _particlePos;

            moneyParticle = particle1.GetComponent<ParticleSystem>();
            moneyParticle2 = particle2.GetComponent<ParticleSystem>();
        }
        else
       if (FindObjectOfType<GameManager>().FactoryValue == 3)
        {
            GameObject particle1 = Instantiate(MoneyParticlesX1L3[opType], transform.position + _particlePos, Quaternion.Euler(-90, 0, 0), transform);
            GameObject particle2 = Instantiate(MoneyParticlesX2L3[opType], transform.position + _particlePos, Quaternion.Euler(-90, 0, 0), transform);

            particle1.transform.localPosition = _particlePos;
            particle2.transform.localPosition = _particlePos;

            moneyParticle = particle1.GetComponent<ParticleSystem>();
            moneyParticle2 = particle2.GetComponent<ParticleSystem>();
        }


        partScale0 = moneyParticle.transform.localScale;
        partScale1 = moneyParticle2.transform.localScale;
    }





}
