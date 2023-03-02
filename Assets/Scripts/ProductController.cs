using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProductController : MonoBehaviour
{
    [SerializeField] Transform[] Form;
    [SerializeField] Transform _enter;

    public int currentForm;

    [SerializeField] Vector3[] Scale;

    public float speed;
    public float speedUp;
    public float speedDown;

    private bool falling;

    [SerializeField] TrailRenderer _trail;

    [SerializeField] int[] Payment;
    [SerializeField] float PaymentMultp;
    public float currentPayment;

    public bool upgrading;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        for(int i = 0; i < Payment.Length; i++)
        {
            Payment[i] = gameManager._currentFactory.economy.payment[i];
        }

        PaymentMultp= gameManager._currentFactory.economy.paymentMultiplier;


        speedUp = gameManager._currentFactory.economy.speedUp ;
        speedDown = gameManager._currentFactory.economy.speedDown ;
        PaymentMultp = gameManager._currentFactory.economy.paymentMultiplier ;


    }

    // Update is called once per frame
    void Update()
    {


        PaymentMultp = gameManager._currentFactory.economy.paymentMultiplier * GameManager.RewardPriceMultp;


        if (falling)
        {
            transform.localPosition += Vector3.back * Time.deltaTime * speed*2;
            transform.localPosition += Vector3.down * Time.deltaTime * speed*2;
        }
        if (!falling)
        {
            transform.localPosition += Vector3.back * Time.deltaTime * speed;



                if (gameManager._currentFactory.tapSpeed > 1)
                {
                    speed = speedUp * GameManager.RewardSpeedMultp;
                    _trail.time = 0.15f;
                }
                else
                {
                    speed = speedDown * GameManager.RewardSpeedMultp;

                    if (gameManager.rewardSpeedMultp == 2)
                    {
                        _trail.time = 0.15f;
                    }
                    else
                    {
                        _trail.time = Mathf.Lerp(_trail.time, 0, Time.deltaTime * 10f);
                    }
                }
            

     
        }


    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Operator"))
        {



            if (upgrading == false)
            {
                upgrading = true;

           
                    currentPayment = Payment[0] * PaymentMultp;

                gameManager._currentFactory.money += Payment[0] * PaymentMultp;
                
                other.GetComponent<OperatorController>().currentPayment = currentPayment;
                other.GetComponent<OperatorController>().Product();
                if (currentForm < 1)
                {
                    if (gameManager._currentFactory.tapSpeed >= 1 || GameManager.RewardSpeedMultp == 2)
                    {

                        StartCoroutine(UpgradingUp(1));
                    }
                    else
                    {
                        StartCoroutine(Upgrading(1));
                    }


                }
                else
                {
                    StartCoroutine(Upgrading0());

                }
            }
        }


        if (other.CompareTag("Op1"))
        {



            if (upgrading == false)
            {
                upgrading = true;

          

                    currentPayment = Payment[1] * PaymentMultp;

                gameManager._currentFactory.money += Payment[1] * PaymentMultp;
                
                other.GetComponent<OperatorController>().currentPayment = currentPayment;
                other.GetComponent<OperatorController>().Product();

                if (currentForm < 2)
                {
                    if (gameManager._currentFactory.tapSpeed >= 1 || GameManager.RewardSpeedMultp == 2)
                    {
                        StartCoroutine(UpgradingUp(2));
                    }
                    else
                    {
                        StartCoroutine(Upgrading(2));


                    }



                }
                else
                {
                    StartCoroutine(Upgrading0());

                }

            }
        }

            if (other.CompareTag("Op2"))
            {
                if (upgrading == false)
                {
                    upgrading = true;


                gameManager._currentFactory.money += Payment[2] * PaymentMultp;

                        currentPayment = Payment[2] * PaymentMultp;
                    
                    other.GetComponent<OperatorController>().currentPayment = currentPayment;
                other.GetComponent<OperatorController>().Product();
                if (currentForm < 3)
                    {
                        if (gameManager._currentFactory.tapSpeed >= 1 || GameManager.RewardSpeedMultp == 2)
                        {
                            StartCoroutine(UpgradingUp(3));
                        }
                        else
                        {
                            StartCoroutine(Upgrading(3));
                        }
                    }
                    else
                    {
                        StartCoroutine(Upgrading0());

                    }

                }
            }

            if (other.CompareTag("Op3"))
            {


                if (upgrading == false)
                {
                    upgrading = true;

                gameManager._currentFactory.money += Payment[3] * PaymentMultp;

                        currentPayment = Payment[3] * PaymentMultp;
                    
                    other.GetComponent<OperatorController>().currentPayment = currentPayment;
                other.GetComponent<OperatorController>().Product();

                if (currentForm < 4)
                    {
                        if (gameManager._currentFactory.tapSpeed >= 1||GameManager.RewardSpeedMultp==2)
                        {
                            StartCoroutine(UpgradingUp(4));
                        }
                        else
                        {
                            StartCoroutine(Upgrading(4));
                        }
                    }
                    else
                    {
                        StartCoroutine(Upgrading0());

                    }

                }

            

        }

        if (other.CompareTag("Op4"))
        {



            if (upgrading == false)
            {
                upgrading = true;

                gameManager._currentFactory.money += Payment[4] * PaymentMultp;

                    currentPayment = Payment[4] * PaymentMultp;
                
                other.GetComponent<OperatorController>().currentPayment = currentPayment;
                other.GetComponent<OperatorController>().Product();

                if (currentForm < 5)
                {
                    if (gameManager._currentFactory.tapSpeed >= 1 || GameManager.RewardSpeedMultp == 2)
                    {
                        StartCoroutine(UpgradingUp(5));
                    }
                    else
                    {
                        StartCoroutine(Upgrading(5));
                    }
                }
                else
                {
                    StartCoroutine(Upgrading0());

                }

            }
        }

        if (other.CompareTag("Op5"))
        {




            if (upgrading == false)
            {
                upgrading = true;
          
                    currentPayment = Payment[5] * PaymentMultp;

                FindObjectOfType<GameManager>()._currentFactory.money += Payment[5] * PaymentMultp;
                
                other.GetComponent<OperatorController>().currentPayment = currentPayment;
                other.GetComponent<OperatorController>().Product();

                if (currentForm < 6)
                {
                    if (gameManager._currentFactory.tapSpeed > 1 || GameManager.RewardSpeedMultp == 2)
                    {
                        StartCoroutine(UpgradingUp(6));
                    }
                    else
                    {
                        StartCoroutine(Upgrading(6));
                    }

                }
                else
                {
                    StartCoroutine(Upgrading0());

                }


            }
        }




            if (other.CompareTag("Box"))
            {



                StartCoroutine(Falling(other.gameObject));
            }



        }

        IEnumerator Upgrading0()
        {

            yield return new WaitForSeconds(0.08f);
            upgrading = false;


        }


        IEnumerator Upgrading(int i)
        {

            yield return new WaitForSeconds(0.04f);

            Form[currentForm].DOScale(Vector3.one * 2f, 0.08f);

            yield return new WaitForSeconds(0.08f);
            upgrading = false;
            Form[currentForm].gameObject.SetActive(false);
            currentForm = i;
            Form[currentForm].gameObject.SetActive(true);
            Form[currentForm].DOScale(Scale[0] * 1.5f, 0.02f);

            yield return new WaitForSeconds(0.02f);
            Form[currentForm].DOScale(Scale[0], 0.08f);
            yield return new WaitForSeconds(0.08f);

        }

        IEnumerator UpgradingUp(int i)
        {
        
            yield return new WaitForSeconds(0.02f);
            
            Form[currentForm].DOScale(Vector3.one * 2f, 0.02f);
            
            yield return new WaitForSeconds(0.02f);
            upgrading = false;
            Form[currentForm].gameObject.SetActive(false);
            currentForm = i;
            Form[currentForm].gameObject.SetActive(true);
            // yield return new WaitForSeconds(0.02f);
            Form[currentForm].DOScale(Scale[0] * 1.5f, 0.02f);

            yield return new WaitForSeconds(0.02f);
            GetComponent<Collider>().enabled = true;
            Form[currentForm].DOScale(Scale[0], 0.01f);
            yield return new WaitForSeconds(0.01f);

    }

        IEnumerator Falling(GameObject other)
        {

            falling = true;
            yield return new WaitForSeconds(0.5f);
            if (other.gameObject.activeSelf == false)
            {
                gameObject.SetActive(false);
            }
            GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(0.5f);
        if (other.gameObject.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(0.2f);
    


        }




    

}
