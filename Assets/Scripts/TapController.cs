
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TapticPlugin;
using Taptic;

public class TapController : MonoBehaviour
{
    public float productSpeedUp;


    public Transform perImage;


    [SerializeField] GameManager gameManager;

    [SerializeField] LevelSettings levelSettings;


    [SerializeField] int _vibValue;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        _vibValue = 3;

    productSpeedUp = gameManager._currentFactory.economy.productSpeedUp;
    }

    // Update is called once per frame
    void Update()
    {


   productSpeedUp = gameManager._currentFactory.economy.productSpeedUp;
    }


    private void OnMouseDown()
    {
        if (levelSettings.vibrationOn) {

            if (MissionManager.instance.currentMission.missionType=="tap")
            {
             
                    MissionManager.instance.DoMission();
                
            }

            if(_vibValue<3)
            {
                _vibValue++;
            }
            else
            {
                Vibration.Vibrate(45, 60, true);
                //TapticManager.Impact(ImpactFeedback.Light);
                _vibValue = 0;
                
            }
           // Debug.Log("Vib: " + _vibValue);
    
            }

            if (gameManager._currentFactory.tapSpeed <2)
            {
            gameManager._currentFactory.tapSpeed += 1f;
            }
            gameManager._currentFactory.productSpeed = productSpeedUp;

        //if (perImage.gameObject.activeSelf == true)
        //{
            StartCoroutine(PerInflate());
        //}
        

   
    }



    IEnumerator PerInflate() {

        perImage.DOScale(Vector3.one * 1.1f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        perImage.DOScale(Vector3.one , 0.1f);
        yield return new WaitForSeconds(0.1f);

    }





}
