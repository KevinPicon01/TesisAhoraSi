using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyReward : MonoBehaviour
{

    [SerializeField] private GameObject _dailyReward;
    [SerializeField] private GameObject _ranking;
    
    
   

   
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
    
    
    
}
