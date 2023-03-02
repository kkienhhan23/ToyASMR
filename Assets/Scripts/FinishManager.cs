using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinishManager : MonoBehaviour
{


    [SerializeField] BoxController boxController;


    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;

        boxController = transform.parent.GetComponent<BoxController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProductObj"))
        {
            gameManager.ProductSound();

            if (gameManager._currentFactory.productBoxCount  < gameManager._currentFactory.boxCapacity)
            {
                boxController.animator.SetTrigger("Blob");
                boxController.animator.SetTrigger("Idle");
                gameManager._currentFactory.productBoxCount++;

                if (other.GetComponent<ProductController>().currentForm == 1)
                {
                    gameManager._currentFactory.boxMultp = 1;
                }
                else if (other.GetComponent<ProductController>().currentForm == 2)
                {
                    gameManager._currentFactory.boxMultp = 3;
                }
                else if (other.GetComponent<ProductController>().currentForm == 3)
                {
                    gameManager._currentFactory.boxMultp = 5;
                }
                else if (other.GetComponent<ProductController>().currentForm == 4)
                {
                    gameManager._currentFactory.boxMultp = 7;
                }
                else if (other.GetComponent<ProductController>().currentForm == 5)
                {
                    gameManager._currentFactory.boxMultp = 9;
                }
                else if (other.GetComponent<ProductController>().currentForm == 6)
                {
                    gameManager._currentFactory.boxMultp = 11;
                }



            }
            else
            {
                gameManager._currentFactory.x += 20;
                gameManager._currentFactory.boxCapacity += gameManager._currentFactory.x;
                
                PlayerPrefs.SetInt("X", gameManager._currentFactory.x);
                PlayerPrefs.SetInt("BoxCapacity", gameManager._currentFactory.boxCapacity);
                gameManager._currentFactory.productBoxCount = 0;
                boxController.Filled();
            }
            Destroy(other.gameObject);
        }

    }
    }
