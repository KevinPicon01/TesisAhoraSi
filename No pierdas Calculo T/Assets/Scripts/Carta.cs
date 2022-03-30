using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    [SerializeField] private GameObject came;
    public Sprite textura;
    public Sprite cuadrado;
    public int numCasilla = 0;
    public float tiempoDelay;
    public float deltadobleClick;
    public float dobleclickdelay = 1.5f;
   [SerializeField] private int cont1;
    public GameObject gm;
    private GameObject[] crearCasillas;

    string url = "http://66.94.101.162:8888/";
        //"http://localhost/Game";
    

    private void Update()
    {
        deltadobleClick += Time.deltaTime;
    }
    private void Seleccionado()
    {
        came = GameObject.Find("Main Camera");
        if (came.GetComponent<CrearCasillas>().lugar < 1)
        {
            came.GetComponent<CrearCasillas>().lugar = 2;
            gm = GameObject.Find("PrimerObjeto");
            gm.GetComponent<SpriteRenderer>().sprite = textura;
            gm.GetComponent<Transform>().localScale = new Vector3(0.6f, 0.6f, numCasilla);
            crearCasillas = GameObject.FindGameObjectsWithTag("MainCamera");
            crearCasillas[0].GetComponent<CrearCasillas>().CrearImg();
        }
        else
        {
            gm = GameObject.Find("SegundoObjeto");
            gm.GetComponent<SpriteRenderer>().sprite = textura;
            gm.GetComponent<Transform>().localScale = new Vector3(0.6f, 0.6f, numCasilla);
            came.GetComponent<CrearCasillas>().lugar = 0;
        }
    }
    public void sirva()
    {
        if (deltadobleClick <= dobleclickdelay)
        {
            Seleccionado();
        }
        else
        {
            deltadobleClick = 0;
            PonerImagen();
        }
    }
    
    public void AsignarImagen(Sprite imagen)
    {
        GetComponent<Image>().sprite = cuadrado;
        textura = imagen;
    }

    public void VoltearCara()
    {
        GetComponent<Image>().sprite = cuadrado;
    }

    public void PonerImagen()
    {
        GetComponent<Image>().sprite= textura;
        Invoke("VoltearCara", tiempoDelay);
    }


    
}
