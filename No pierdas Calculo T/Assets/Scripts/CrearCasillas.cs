using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Random = System.Random;

public class CrearCasillas : MonoBehaviour
{
    string url = "http://66.94.101.162:8888";
        //"http://localhost/Game";
    [SerializeField] private GameObject _ranking;
    public GameObject cartaPrefab;
    public int ancho;
    public List<GameObject> casillas;
    public Transform cartasPadre;
    public Transform cartasPadre2;
    public Sprite[] imagenes;
    public Sprite palabras;
    public GameObject gm;
    public GameObject puntajeF;
    public List<int> numeros;
    public Sprite original;
    private int progreso;
    public int pnt;
    public float tiempo;
    public GameObject final;
    public int lugar = 0;
    public web score;
    public string modo;
    public bool seguro = true;
    [Header("Recetas")] [SerializeField] private List<GameObject> recetaLimites;
    [SerializeField] private List<GameObject> recetaFunciones;
    [SerializeField] private List<GameObject> recetaDerivadas;
    [SerializeField] private TMP_Text nombre;
    public List<List<GameObject>> recetasCompletas = new List<List<GameObject>>();

    [SerializeField] private TMP_Text pasosCal;
    private int recetaActual;




    [Header("Sesion")] private SesionManager mySesionManager;
    public bool Sesion;
    public string nombreS;
    public int receta;


    public void GenerarReceta()
    {
        Random rand = new Random();
        var x = rand.Next(0, recetasCompletas[receta].Count);
        recetaActual = x;



        nombre.text = recetasCompletas[receta][x].GetComponent<Recetas>().nombre;

        var cal = "";

        for (int i = 0; i < 4; i++)
        {
            cal += recetasCompletas[receta][x].GetComponent<Recetas>().pasosCal[i] + " " +
                   recetasCompletas[receta][x].GetComponent<Recetas>().pasosRec[i] + "\n";

        }

        pasosCal.text = cal;

        //recetas.RemoveAt(x);

    }

    private void Awake()
    {


        //mySesion.nombre2 = "hola no unc";

    }

    public void CrearImg()
    {
        //ClearChildren(cartasPadre2);
        //ClearChildren(cartasPadre);
        LlenarNumeros();
        int cont = 0;


        for (int i = 0; i < casillas.Count; i++)
        {

            // GameObject cartaTemp = Instantiate(cartaPrefab, new Vector2(j, i), quaternion.identity);
            var cartaTemp = casillas[i];


            int tmpint = GenerarRandom();
            cartaTemp.GetComponent<Carta>().AsignarImagen(imagenes[tmpint]);


            cartaTemp.GetComponent<Carta>().numCasilla = tmpint;
            cont++;

            cartaTemp.transform.parent = cartasPadre;
        }

    }

    public void prueba()
    {
        Debug.Log("holasirve");

    }

    public void ClearChildren(Transform cartasPadre)
    {


        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[cartasPadre.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in cartasPadre)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }


    }

    private void Start()
    {



        mySesionManager = FindObjectOfType<SesionManager>();
        this.Sesion = mySesionManager.Sesion;
        this.nombreS = mySesionManager.nombre2;
        receta = mySesionManager.receta;

        recetasCompletas.Add(recetaLimites);
        recetasCompletas.Add(recetaDerivadas);
        recetasCompletas.Add(recetaFunciones);




        tiempo = Time.unscaledTime + 60;
        progreso = 0;
        pnt = 0;
        CrearImg();
        GenerarReceta();


    }


    public void volverInicio()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }


    private void LlenarNumeros()
    {
        for (int i = 0; i < 25; i++)
        {
            numeros.Insert(i, i);

        }

    }



    private int GenerarRandom()
    {
        Random r = new Random();

        int tmp = r.Next(0, numeros.Count);

        int rtn = numeros[tmp];




        numeros.Remove(rtn);



        return rtn;

    }

    public void butt()
    {
        lugar = 0;
        verificar();
        CrearImg();
        Restaurar();

    }





    public void verificar()
    {
        gm = GameObject.Find("PrimerObjeto");

        var pr = gm.GetComponent<Transform>().localScale.z;
        gm = GameObject.Find("SegundoObjeto");

        var sg = gm.GetComponent<Transform>().localScale.z;


        var e = recetasCompletas[receta][recetaActual].GetComponent<Recetas>().numeros[progreso];

        if (pr == sg && pr == e)
        {
            progreso++;
            pnt += 25;
            gm = GameObject.Find("Puntaje");
            gm.GetComponent<TextMeshProUGUI>().text = pnt.ToString();

        }

        if (progreso > 3)
        {
            progreso = 0;
            GenerarReceta();
        }




    }

    private void Restaurar()
    {
        gm = GameObject.Find("PrimerObjeto");

        gm.GetComponent<Transform>().localScale = new Vector3(0.1587f, 0.3452f, 1);

        gm.GetComponent<SpriteRenderer>().sprite = original;
        gm = GameObject.Find("SegundoObjeto");

        gm.GetComponent<Transform>().localScale = new Vector3(0.1587f, 0.3452f, 1.5f);

        gm.GetComponent<SpriteRenderer>().sprite = original;
    }

    private void Update()
    {
        gm = GameObject.Find("Tiempo");
        if ((int) (tiempo - Time.unscaledTime) > 0)
        {
            gm.GetComponent<TextMeshProUGUI>().text = ((int) (tiempo - Time.unscaledTime)).ToString();
        }
        if (tiempo - Time.unscaledTime <= 0 && seguro)
        {
            score.miPuntaje = pnt;
            score.miNombre = nombreS;
            score.InsertarHiScore();
            gm.GetComponent<TextMeshProUGUI>().text = "0";
            final.SetActive(true);
            puntajeF.GetComponent<TextMeshProUGUI>().text = pnt.ToString();
            seguro = false;
            modo = AsignarModo();
            StartCoroutine(NuevoPuntaje());
        }
    }

    public String AsignarModo()
    {
        var tmp = "";
        if (receta == 0)
        {
            tmp = "record_limites";
        }
        else if (receta == 1)
        {
            tmp = "record_derivadas";
        }
        else if (receta == 2)
        {
            tmp = "record_funciones";
        }
        return tmp;
    }
    public IEnumerator NuevoPuntaje()
    {
        WWWForm form = new WWWForm();
        form.AddField("userName", nombreS);
        form.AddField("puntaje", pnt);
        form.AddField("modo", modo);
        
        var www = UnityWebRequest.Post(url+"/ActualizarRanking.php", form);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
    }

    public void JugarDeNuevo()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Play");
       /* final.SetActive(false);
        tiempo = Time.unscaledTime + 20;
        Start();
        Restaurar();*/
    }
    public void OnButtonOpenRank()
    {
        _ranking.SetActive(true);
    }

    

    public void OnButtonClose()
    {
        _ranking.SetActive(false);
    }
}
