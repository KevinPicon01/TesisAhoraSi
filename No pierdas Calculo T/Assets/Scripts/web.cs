using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class web : MonoBehaviour
{
    public Transform tabla;
    public GameObject plantillaRegistros;
    string url = "http://66.94.101.162:8888/";
        //"http://localhost/Game";
    public int miPuntaje;
    public string miNombre;
    public DateTime dateTime;
    
    public int numRegistros = 0;
    public estructuraDatosWeb datos;

    [System.Serializable]
    public struct estructuraDatosWeb
    {
         [System.Serializable]
        public struct registro
        {
            public string nombre;
            public int puntaje;
        }

        public List<registro> registros;
        
    }

    [ContextMenu("Leer Json")]
    public void LeerJson(System.Action empLeer)
    {
        StartCoroutine(CorrutinaLeerJson(empLeer));
    }
   
    [ContextMenu("Escribir Json")]
    public void EscribirJson()
    {
        StartCoroutine(CorrutinaEscribirJson());
    }

   
   
    private IEnumerator CorrutinaLeerJson(System.Action empLeer)
    {
        UnityWebRequest web =  UnityWebRequest.Get(url+"RankingNPCJson.txt");
        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            datos = JsonUtility.FromJson<estructuraDatosWeb>(web.downloadHandler.text);
            empLeer();
        }
        else
        {
            Debug.LogWarning(("Hubo un problema en el servidor"));
        }


    }
    private IEnumerator CorrutinaEscribirJson()
    {

        WWWForm form = new WWWForm();
        form.AddField("archivo","RankingNPCJson");
        form.AddField("texto", JsonUtility.ToJson(datos));
        Debug.Log(JsonUtility.ToJson(datos));
        UnityWebRequest web =  UnityWebRequest.Post(url+"EscribirDatos.php",form);
        yield return web.SendWebRequest();
        if (!web.isNetworkError && !web.isHttpError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.LogWarning(("Hubo un problema en el servidor"));
            
        }


    }

    [ContextMenu("crear tabla")]
    void CrearTabla()
    {
        for (int i = 0; i < numRegistros; i++)
        {
            GameObject inst = Instantiate(plantillaRegistros, tabla);
            inst.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,i*-150);
            inst.name = i.ToString();
        }
    }

    [ContextMenu("Pasar Datos a tabla")]
    void pasarDatosTabla()
    {
        for (int i = 0; i < numRegistros; i++)
        {
            tabla.GetChild(i).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = datos.registros[i].nombre;
            tabla.GetChild(i).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = datos.registros[i].puntaje.ToString();
            
        }
    }
    
    [ContextMenu("Insertar")]
    public void InsertarHiScore()
    {
        for (int i = 0; i < 5; i++)
        {
            
            if (miPuntaje > datos.registros[i].puntaje)
            {
                datos.registros.Insert(i,new estructuraDatosWeb.registro()
                {
                    nombre = miNombre,
                    puntaje = miPuntaje
                    
                });
                
                EscribirJson();
                pasarDatosTabla();
                break;
            }
        }
    }

    void CrearTablaPasarDatosYChequear()
    {
        CrearTabla();
        pasarDatosTabla();

    }

    private void Start()
    {
        
        LeerJson(CrearTablaPasarDatosYChequear);
        
    }

   

    

    public void play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Levels");
    
    }
    


}   
