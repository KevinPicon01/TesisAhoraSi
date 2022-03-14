using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{

    public void CreateUser(string userName, string email, string pass, Action<Response> response)
    {
        StartCoroutine(Cd_createUser(userName, email, pass, response));
    }

    private IEnumerator Cd_createUser(string userName, string email, string pass, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userName);
        form.AddField("email", email);
        form.AddField("pass", pass);

        WWW w = new WWW("http://66.94.101.162:8888/createUser.php", form);

        yield return w;

        Debug.Log(w.text);
        
        
            response(JsonUtility.FromJson<Response>(w.text));
    }

    public void CheckUser(string userName, string pass, Action<Response> response)
    {
        StartCoroutine(Cd_checkUser(userName, pass, response));
    }

    private IEnumerator Cd_checkUser(string userName, string pass, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", userName);
        form.AddField("pass", pass);

        //UnityWebRequest w = UnityWebRequest.Post("http://localhost:/Game/CheckUser.php",form);
       WWW w = new WWW("http://66.94.101.162:8888/CheckUser.php", form);
    
        yield return w;

     
        
        
        response(JsonUtility.FromJson<Response>(w.text));
        
    }
    
}
[Serializable]
public class Response
{
    public bool done ;
    public string message = "";
}
