using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
   string url = "http://66.94.101.162:8888/";
                  //"http://localhost/Game/";

    [SerializeField] private GameObject sceneManag;
    public bool entro=false;
    public string mensaje;
    [Header("Sesion")]
    private SesionManager mySesionManager;
    public bool Sesion;
    public string nombreS; 
    //public string url;
    private void Start()
    {
        mySesionManager = FindObjectOfType<SesionManager>();
        this.Sesion = mySesionManager.Sesion;
        this.nombreS = mySesionManager.nombre2;
       // url = mySesionManager.url;
        
    }
    public void CreateUser(string userName, string email, string pass, string name , string lastName, string doc) 
    {
        StartCoroutine(Cd_createUser(userName, email, pass, name, lastName, doc));
    }

    private IEnumerator Cd_createUser(string userName, string email, string pass, string name, string lastName, string doc)
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userName);
        form.AddField("email", email);
        form.AddField("pass", pass);
        form.AddField("name", name);
        form.AddField("lastName", lastName);
        form.AddField("doc", doc);
       
        UnityWebRequest www = UnityWebRequest.Post(url + "createUser.php", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var x = JsonUtility.FromJson<Response>(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
            if (x.done)
            {
                sceneManag.GetComponent<SceneManager>().m_Text.text = www.downloadHandler.text;
                sceneManag.GetComponent<SceneManager>().ShowLogin();
            }
            else
            {
                sceneManag.GetComponent<SceneManager>().m_Text.text = x.message;
            }
            
        }
    }
        

    public void CheckUser(string userName, string pass)
    {
        StartCoroutine(Cd_checkUser(userName, pass));
    }

    private IEnumerator Cd_checkUser(string userName, string pass)
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userName);
        form.AddField("pass", pass);
        var w = UnityWebRequest.Post(url+"/checkUser.php", form);
        yield return w.SendWebRequest();
        var tmp = w.downloadHandler.text;
        Debug.Log(tmp);
        var data = JsonUtility.FromJson<Response>(tmp);
        entro = data.done;
        mensaje = data.message;

        if (!entro)
        {
            sceneManag.GetComponent<SceneManager>().m_Text.text = mensaje;
        }
        else
        {
            mySesionManager.nombre2 = userName;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }

        
    }
    
}
[Serializable]
public class Response
{
    public bool done ;
    public string message = "";
}
