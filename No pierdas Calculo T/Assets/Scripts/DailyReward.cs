using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class DailyReward : MonoBehaviour
{
    string url ="http://66.94.101.162:8888/";
                  //"http://localhost/game";
    
    [SerializeField] private GameObject _dailyReward;
    [SerializeField] private GameObject _ranking;
    [Header("Sesion")]
    private SesionManager mySesionManager;
    public bool Sesion;
    public string nombreS;
    [Header("Daily Reward")]
    bool dailyReward = false;
    [SerializeField] GameObject noReward;

    public void Awake()
    {
        mySesionManager = FindObjectOfType<SesionManager>();
        this.Sesion = mySesionManager.Sesion;
        this.nombreS = mySesionManager.nombre2;
    }

    public async void Start()
    {
        StartCoroutine(DailyRewardTa());
        
    }

    public void OnButtonOpen()
    {
        _dailyReward.SetActive(true);
    }
    public void OnButtonOpenRank()
    {
        _ranking.SetActive(true);
    }
    public void OnButtonClose()
    {
        _dailyReward.SetActive(false);
        _ranking.SetActive(false);
    }

    public async void RewardClaim()
    {
        StartCoroutine(ClaimReward());
       
    }

    public IEnumerator ClaimReward()
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", nombreS);
        var www = UnityWebRequest.Post(url+"/ActualizarDaily.php", form);
        yield return www.SendWebRequest();
        noReward.SetActive(true);
    }
    public IEnumerator DailyRewardTa()
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", nombreS);
        var www = UnityWebRequest.Post(url+"/ConsultaDaily.php", form);
        yield return www.SendWebRequest();
        
        var temp = www.downloadHandler.text;
        Debug.Log(temp);
        var dates = JsonUtility.FromJson<RecibirDailyReward>(temp);
        
        dailyReward = dates.dailyReward;
        noReward.SetActive(!dailyReward);
    }
    
}
public class RecibirDailyReward
{
    public string userName;
    public bool dailyReward;
    
}
