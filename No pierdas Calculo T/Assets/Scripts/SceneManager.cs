using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{


    [Header("Login")] 
    
    [SerializeField] public TMP_InputField m_userNameInputLogin = null;
    [SerializeField] private TMP_InputField m_passwordInputLogin = null;
    

    [Header("Register")]
    
    [SerializeField] private TMP_InputField m_userNameInput      = null;
    [SerializeField] private TMP_InputField m_NameInput      = null;
    [SerializeField] private TMP_InputField m_LastNameInput      = null;
    [SerializeField] private TMP_InputField m_DocInput      = null;
    [SerializeField] private TMP_InputField m_userEmailInput     = null;
    [SerializeField] private TMP_InputField m_userPassInput      = null;
    [SerializeField] private TMP_InputField m_userPassConfInput  = null;
    [SerializeField] public TMP_Text m_Text                     = null;
    [SerializeField] public GameObject m_registerUI            = null;
    [SerializeField] public GameObject m_loginUI            = null;

    [Header("Singleton")] 
    
    
    [SerializeField] public SesionManager mySesion;
    
    private NetworkManager m_networkManager = null;
    private void Awake()
    {
        m_networkManager = GameObject.FindObjectOfType<NetworkManager>();
        
    }
    public async void SubmitLogin()
    {
        
        if ( m_userNameInputLogin.text == "" || m_passwordInputLogin.text == "")
        {
            m_Text.text = "Por favor llena todos los campos";
            return;
        }
        
        m_Text.text = "Procesando...";
        m_networkManager.CheckUser(m_userNameInputLogin.text, m_passwordInputLogin.text);
        
        
        
        
        
    }
    public void ShowLogin()
    {
        m_registerUI.SetActive(false);
        m_loginUI.SetActive(true);
    }
    public void ShowRegister()
    {
        m_registerUI.SetActive(true);
        m_loginUI.SetActive(false);
    }
    public void SubmitRegister()
    {
        if (m_userEmailInput.text == "" || m_userNameInput.text == "" || m_userPassInput.text == "" || m_userPassConfInput.text == "" || m_NameInput.text == "" || m_LastNameInput.text == "" || m_DocInput.text == "")
        {
            m_Text.text = "Por favor llena todos los campos";
            return;
        }
        if (m_userPassInput.text != m_userPassConfInput.text)
        {
            m_Text.text = "Las contraseñas no coinciden";
            return;
        }
        m_Text.text = "Procesando...";
        m_networkManager.CreateUser(m_userNameInput.text, m_userEmailInput.text, m_userPassInput.text,m_NameInput.text, m_LastNameInput.text, m_DocInput.text);

    }
    
}
