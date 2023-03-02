using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandController : MonoBehaviour
{

    [SerializeField] MeshRenderer mesh;

    public float y;
    public float x;
    public float speed;
    public int matInt;
    public int multp = 1;

    public bool rotX;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager._currentFactory.tapSpeed > 1)
        {
            multp = 2;
        }
        else
        {
            multp = 1;
        }

        if (rotX)
        {
            x += Time.deltaTime * speed*multp;

            mesh.materials[matInt].SetTextureOffset("_MainTex", new Vector2(x, 0));
        }
        else
        {
            y += Time.deltaTime * speed*multp;

            mesh.materials[matInt].SetTextureOffset("_MainTex", new Vector2(0, y));
        }

    }
}
