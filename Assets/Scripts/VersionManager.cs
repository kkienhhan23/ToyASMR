﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VersionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void Version1()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Version2()
    {
        SceneManager.LoadScene("AlterScene");
    }
}
