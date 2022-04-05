using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesionManager : MonoBehaviour
{
    [Header("Daticos")] 
    public bool Sesion;
    public static SesionManager inst;
    public string nombre2;
    public int receta;
    public string url;
    private void Awake()
    {
        if (SesionManager.inst == null)
        {
            SesionManager.inst=this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

}