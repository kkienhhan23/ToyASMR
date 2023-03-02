using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMachineController : MonoBehaviour
{

    private Animator animator;
    public static bool fast;
    public static bool tapFast;

    public string animString;
    // Update is called once per frame
    void Update()
    {
        if (OperatorController.fast)
        {
            fast = true;
        }

        if (OperatorController.tapFast)
        {
            tapFast = true;
        }


        Product();
    }


    public void Product()
    {
        if (fast && tapFast)
        {
            animString = "Working2";
        }
        else if (tapFast || fast)
        {

            animString = "Working1";
        }
        else
        {
            animString = "Working";
        }



        //animator.SetTrigger(animString);
        //animator.SetTrigger("Idle");



    }

}
