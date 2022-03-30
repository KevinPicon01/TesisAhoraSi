using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Levels : MonoBehaviour
{
    public GameObject mar;
    public GameObject mex;
    public GameObject postres;
    public List<GameObject> nivelesC;
    private int tmp = 0;
    
    [Header("Sesion")]
    private SesionManager mySesionManager;
    public bool Sesion;
    public string nombreS; 
    string url = "http://66.94.101.162:8888/";
        //"http://localhost/Game";
    private void Start()
    {
        mySesionManager = FindObjectOfType<SesionManager>();
        this.Sesion = mySesionManager.Sesion;
        this.nombreS = mySesionManager.nombre2;
        //url = mySesionManager.url;
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
        var modo = "";
        mySesionManager.receta = tmp;
        if (tmp==0)
        {
            modo="limites";
        }
        else if(tmp == 1)
        {
            modo = "derivados";
        }
        else
        {
            modo = "funciones";
        }
        
        Sumarpuntos(modo);
        
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("Play");
    }
    public async void Sumarpuntos(string modo)
    {
        StartCoroutine(SumarPuntosTask(modo));
    }

    public IEnumerator SumarPuntosTask(string modo)
    {
        
        var form = new WWWForm();
        form.AddField("userName", nombreS);
        form.AddField("modo", modo);
        var www = UnityWebRequest.Post(url+"/ContadorJuegos.php", form);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
    public void Back(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }
}
