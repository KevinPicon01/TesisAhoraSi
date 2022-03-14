using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public GameObject mar;
    public GameObject mex;
    public GameObject postres;
    public List<GameObject> nivelesC;
    private int tmp = 0;

    private void Start()
    {
        nivelesC.Add(mar);
        nivelesC.Add(mex);
        nivelesC.Add(postres);
    }

    public void NextButton()
    {
        if (tmp >= 2) return;
        nivelesC[tmp].SetActive(false);
        nivelesC[tmp + 1].SetActive(true);
        tmp += 1;
    }

    public void BackButton()
    {
        if (tmp <= 0) return;
        nivelesC[tmp].SetActive(false);
        nivelesC[tmp - 1].SetActive(true);
        tmp -= 1; 
    }

    public void Ranked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Play");
    }

}