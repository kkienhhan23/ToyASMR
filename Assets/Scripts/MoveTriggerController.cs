using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTriggerController : MonoBehaviour
{

    [SerializeField] Transform _parent;

    public float turnTime;


    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        turnTime = gameManager._currentFactory.economy.turnTime / gameManager.rewardSpeedMultp;
        
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Stand"))
        {
            _parent.transform.parent = other.transform;
            _parent.transform.DOLocalMoveX(0, turnTime);
            _parent.transform.DOLocalRotate(Vector3.zero, 0.35f);
            
        }





    }


}
