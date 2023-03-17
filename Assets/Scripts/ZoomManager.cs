using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManager : MonoBehaviour
{
    public float zoomLimit0;
    public float zoomlimit1;

    public float zoomVar;

    float zooModifier;
    public float zooModifierSpeed;

    Vector3 firstTouchPosPrev;
    Vector3 secondTouchPosPrev;

    float difTouchPosPrev;
    float difTouchPosCur;



    public static ZoomManager instance;
    // Start is called before the first frame update


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        difTouchPosPrev = 1;
        difTouchPosCur = 0;
        zoomVar = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch tFirst = Input.GetTouch(0);
            Touch tSecond = Input.GetTouch(1);

            firstTouchPosPrev = tFirst.position - tFirst.deltaPosition;
            secondTouchPosPrev = tSecond.position - tSecond.deltaPosition;

            difTouchPosPrev = (firstTouchPosPrev - secondTouchPosPrev).magnitude;
            difTouchPosCur = (tFirst.position - tSecond.position).magnitude;

            zooModifier = (tFirst.deltaPosition - tSecond.deltaPosition).magnitude*zooModifierSpeed;




        }
        else
        {
            if (difTouchPosPrev > difTouchPosCur)
            {
                //zoomVar = zooModifier;
                zoomVar = 1f;
            }
            else
            {
                //zoomVar = -zooModifier;
                zoomVar = 0.5f;
            }
        }


   

    }
}
