using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;


public class MissionManager : MonoBehaviour
{



    [SerializeField] GameObject _nextButton;
    [SerializeField] GameObject _closeButton;


    [SerializeField] float[] Rewards;

    [SerializeField] Transform _missionTab;
    [SerializeField] Transform _missionCompleteTab;
    [SerializeField] TMP_Text _missionText;
    [SerializeField] TMP_Text _missionCompleteText;
    [SerializeField] TMP_Text _rewardText;
    [SerializeField] TMP_Text _rewardCompleteText;

    [SerializeField] GameObject _missionCounter;
    [SerializeField] Image _progressBar;


    public Mission currentMission;




    public int missionValue;



    public static MissionManager instance;


    public bool missionStarted;
    bool missionComplete;

    public float countFirst;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;

        //Missions[0].name = "Clicking 1000 times!";
        //Missions[0].count = 1000;
        //Missions[0].reward = Rewards[0];
        //Missions[0].missionType = "tap";

        //Missions[1].name = "Add 10 worker!";
        //Missions[1].count = 10;
        //Missions[1].reward = Rewards[1];
        //Missions[1].missionType = "worker";

    }


    void Start()
    {


        missionValue = PlayerPrefs.GetInt("missionValue");
        currentMission = new Mission();

      if (PlayerPrefs.GetFloat("missionCount") > 0)
            {

            missionStarted = true;
            _missionCounter.SetActive(true);
            }

        currentMission.SetMission(missionValue);



       // currentMission.count = PlayerPrefs.GetInt("missionCount");


        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(PlayerPrefs.GetFloat("missionCount"));

        if (missionStarted)
        {
            _missionCounter.SetActive(true);

            _progressBar.fillAmount = ((countFirst- currentMission.count)/ countFirst);

        }
        else
        {
            _missionCounter.SetActive(false);
        }
    }




    public class Mission
    {
        public string name;
        public float count;
        public float reward;
        public string missionType;

        


        public void SetMission(int value)
        {
            switch (value)
            {

                case 0:
                    name = "Clicking 1000 times!";
                    count = 1000;
                    instance.countFirst = 1000;
                    reward =instance.Rewards[0];
                    missionType = "tap";
                    if (PlayerPrefs.GetFloat("missionCount")>0)
                    {
                        count = PlayerPrefs.GetFloat("missionCount");
                    }
                    
                    
                    break;


                case 1:
                    name = "Add 20 worker!";
                    count = 20;
                    instance.countFirst = 20;
                    reward = instance.Rewards[1];
                    missionType = "worker";
                    if (PlayerPrefs.GetFloat("missionCount") > 0)
                    {
                        count = PlayerPrefs.GetFloat("missionCount");
                    }
                    break;


                case 2:
                    name = "Do 10 merge!";
                    count = 10;
                    instance.countFirst = 10;
                    reward = instance.Rewards[2];
                    missionType = "merge";
                    if (PlayerPrefs.GetFloat("missionCount") > 0)
                    {
                        count = PlayerPrefs.GetFloat("missionCount");
                    }
                    break;

                case 3:

                    instance.ResetMissions();
               
                    break;


            }


        }





    }



    public void DoMission()
    {
        if (missionStarted)
        {
            if (!missionComplete)
            {


                if (currentMission.count > 1)
                {
                    currentMission.count--;

                }
                else
                {
                    missionComplete = true;
                    PlayerPrefs.SetFloat("missionCount", 0);
                    StartCoroutine(MissionComplete());

                }
                }
        }


    }

    public void CurrentMission()
    {
        StartCoroutine(CurMission());

    }


    public void CloseMission()
    {
        _missionCompleteTab.transform.localScale = Vector3.zero;
        _missionCompleteTab.gameObject.SetActive(false);

    }


    IEnumerator CurMission()
    {

        //yield return new WaitForSeconds(2f);
        _missionCompleteTab.gameObject.SetActive(true);
        _nextButton.SetActive(false);
        _closeButton.SetActive(true);
        _rewardCompleteText.text = "$" + currentMission.reward.ToString();
        _missionCompleteText.text = currentMission.name;
        _missionCompleteTab.DOScale(Vector3.one * 1.2f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _missionCompleteTab.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.1f);
  

    }


    IEnumerator MissionComplete()
    {

        //yield return new WaitForSeconds(2f);
        PlayerPrefs.SetFloat("missionCount", 0);
        currentMission.count = 0;
        _missionCompleteTab.gameObject.SetActive(true);
        _nextButton.SetActive(true);
        _closeButton.SetActive(false);
        _rewardCompleteText.text = "$" + currentMission.reward.ToString();
        _missionCompleteText.text = "Mission Complete!";
        _missionCompleteTab.DOScale(Vector3.one * 1.2f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _missionCompleteTab.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.5f);
       

    }

    

    public void NextMission()
    {

        GameManager.Instance.AfterAdsReward();

        if (missionComplete)
        {
            _missionCompleteTab.transform.localScale = Vector3.zero;
            _missionCompleteTab.gameObject.SetActive(false);
            missionComplete = false;
            missionValue++;
            currentMission.SetMission(missionValue);
            StartCoroutine(MissionOpen());
        }
        //else
        //{

        //}
    }

    IEnumerator MissionOpen()
    {

        //yield return new WaitForSeconds(2f);
        _missionTab.gameObject.SetActive(true);
        _rewardText.text = "$" + currentMission.reward.ToString();
        _missionText.text = currentMission.name;
        _missionTab.DOScale(Vector3.one * 1.2f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _missionTab.DOScale(Vector3.one, 0.1f);
        yield return new WaitForSeconds(0.1f);


    }





    public void MissionStart()
    {


        StartCoroutine(MissionStarting());

    }

    IEnumerator MissionStarting()
    {

        //yield return new WaitForSeconds(2f);
        
     
        _missionTab.DOScale(Vector3.one * 1.2f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _missionTab.DOScale(Vector3.zero, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _missionTab.gameObject.SetActive(false);
        _missionCounter.SetActive(true);


    }




    public void FirstMission()
    {
        if (!missionStarted)
        {
            missionStarted=true;
            //mission.SetMission(missionValue);
            StartCoroutine(FirstMissionOpen());
        }
    }

    IEnumerator FirstMissionOpen()
    {

       // yield return new WaitForSeconds(2f);
        _missionTab.gameObject.SetActive(true);
        _rewardText.text = "$" + currentMission.reward.ToString();
        _missionText.text = currentMission.name;
        _missionTab.DOScale(Vector3.one * 1.2f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        _missionTab.DOScale(Vector3.one, 0.5f);
        yield return new WaitForSeconds(0.5f);


    }

     public void ResetMissions()
    {
        PlayerPrefs.SetFloat("missionCount", 0);
        missionValue = 0;
        currentMission = new Mission();
        currentMission.SetMission(missionValue);


    }
    
    public void SaveMission()
    {
        PlayerPrefs.SetFloat("missionCount", currentMission.count);
        PlayerPrefs.SetInt("missionValue", missionValue);

    }

}
