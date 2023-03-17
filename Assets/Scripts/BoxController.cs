using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using TMPro;

public class BoxController : MonoBehaviour
{

    AudioSource audioSource;

    public Animator animator;


    [SerializeField] TMP_Text boxText;

    public float productCount;
    public int productCapacity = 10;

    [SerializeField] float animSize;


    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        gameManager = GameManager.Instance;
        transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = gameManager._currentFactory.productBoxCount + "/" + gameManager._currentFactory.boxCapacity.ToString();
    }

    private void Update()
    {
        transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = gameManager._currentFactory.productBoxCount + "/" + gameManager._currentFactory.boxCapacity.ToString();
    }




    public void Filled()
    {
        StartCoroutine(Fill());
        Inflate();
    }

    public void Inflate()
    {
        StartCoroutine(Inflating());
    }

    IEnumerator Fill()
    {

        yield return new WaitForSeconds(0.2f);
        transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(0).transform.DOMove(transform.GetChild(0).transform.position, 0.2f);
        transform.GetChild(0).transform.GetChild(1).transform.DOMove(Camera.main.transform.GetChild(1).transform.position, 0.2f);
        transform.GetChild(0).transform.GetChild(1).transform.DOScale(Vector3.one * animSize, 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>().SetTrigger("Fill");
        // transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(1f);
        gameManager.boxAudio.Play();
        transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        gameManager._currentFactory.money += gameManager._currentFactory.boxCapacity * gameManager._currentFactory.boxMultp;
        yield return new WaitForSeconds(1.4f);
        transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).transform.GetChild(1).transform.localScale = Vector3.one;
        transform.GetChild(0).transform.GetChild(1).transform.position = transform.position;
        transform.GetChild(0).transform.GetChild(0).transform.position = transform.position - Vector3.up * 50;
        transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);

    }

    IEnumerator Inflating()
    {
        transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).transform.DOScale(Vector3.one * 1.4f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        transform.GetChild(0).transform.GetChild(4).transform.GetChild(0).transform.DOScale(Vector3.one , 0.1f);
        yield return new WaitForSeconds(0.1f);


    }
}
