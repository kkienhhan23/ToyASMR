using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] Transform OpTutorial;
    [SerializeField] Transform MergeTutorial;
    [SerializeField] Transform TapTutorial;


    [SerializeField] GameObject OpText;
    [SerializeField] GameObject MergeText;
    [SerializeField] GameObject TapText;

    [SerializeField] float y;

    Vector3[] posFirst;
    // Start is called before the first frame update




    void Start()
    {
        

        StartCoroutine(Op());
        StartCoroutine(Merge());
        StartCoroutine(Tap());

      
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpImageOpen()
    {
     

        OpTutorial.GetChild(0).gameObject.SetActive(true);
        OpTutorial.GetChild(1).GetComponent<Image>().enabled = true;
    }

    public void OpImageClose()
    {
   

        OpTutorial.GetChild(0).gameObject.SetActive(false);
        OpTutorial.GetChild(1).GetComponent<Image>().enabled = false;



    }

    public void MergeImageOpen()
    {
      

        MergeTutorial.GetChild(0).gameObject.SetActive(true);
        MergeTutorial.GetChild(1).GetComponent<Image>().enabled = true;
    }

    public void MergeImageClose()
    {
        MergeTutorial.GetChild(0).gameObject.SetActive(false);
        MergeTutorial.GetChild(1).GetComponent<Image>().enabled = false;


    }


    public void TapImageOpen()
    {
        TapTutorial.GetChild(0).gameObject.SetActive(true);
        TapTutorial.GetChild(1).GetComponent<Image>().enabled = true;
    }

    public void TapImageClose()
    {
        TapTutorial.GetChild(0).gameObject.SetActive(false);
        TapTutorial.GetChild(1).GetComponent<Image>().enabled = false;
    }




    IEnumerator Op()
    {
        while (true)
        {
           
            OpTutorial.GetChild(1).transform.DOScale(Vector3.one * 0.8f, 0.2f);
            yield return new WaitForSeconds(0.2f);
            OpTutorial.GetChild(1).transform.DOScale(Vector3.one, 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Merge()
    {
        while (true)
        {
       
            MergeTutorial.GetChild(1).transform.DOScale(Vector3.one * 0.8f, 0.2f);
            yield return new WaitForSeconds(0.2f);
            MergeTutorial.GetChild(1).transform.DOScale(Vector3.one, 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Tap()
    {
        while (true)
        {
            TapTutorial.GetChild(1).transform.DOScale(Vector3.one * 0.8f, 0.1f);
            yield return new WaitForSeconds(0.1f);
            TapTutorial.GetChild(1).transform.DOScale(Vector3.one, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
