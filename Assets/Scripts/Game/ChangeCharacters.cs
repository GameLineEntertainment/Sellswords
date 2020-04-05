using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeCharacters : MonoBehaviour
{
    //[HideInInspector]
    public static int[] PersIndex = new int[3];                  // Номер и место нашего персонажа 0-Аое 1-саппорт 2-дд
    public GameObject[] Characters = new GameObject[10];         // Массив всех сущестующих ребят
    public Transform[] Places = new Transform[3];                // По сути точка респауна
    public GameObject GameScripts, ObjVar, объектПеременных;     // Объекты и ссылки на них
    public static string Level;                                  // Уровень на котором мы находимся или загружаем
    public string левак;                                         // Не нужная переменная
    public bool IsMenu;                                          // Переменная для проверки находимся ли мы в меню, чтобы не дай бог не среспаунить ребят

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
        Level = System.Convert.ToString(SceneManager.GetActiveScene().buildIndex);
        левак = Level;
        SceneManager.sceneLoaded += LevelLoaded;
    }
    // Use this for initialization
    void Start()
    {
        
        //for (int i = 0; i < 3; i++)
        //Places[0].position = new Vector3(-1f, 0.5f, -0.6f);
        //Places[1].position = new Vector3(1f, 0.5f, -0.6f);
        //Places[2].position = new Vector3(0f, 0.5f, 1f);

        /*
        if (PersIndex[0] == null || PersIndex[0] == 0)
            PersIndex[0] = 1;

        if (PersIndex[1] == null || PersIndex[1] == 0)
            PersIndex[1] = 4;

        if (PersIndex[2] == null || PersIndex[2] == 0)
            PersIndex[2] = 9;

         */
       
       

        if (!IsMenu)
        {
            //Instantiate(Characters[PersIndex[0]], transform.position, transform.rotation);
            //Instantiate(Characters[PersIndex[1]], transform.position, transform.rotation);
            //Instantiate(Characters[PersIndex[2]], transform.position, transform.rotation);

            //GameScripts.GetComponent<GameLevel>().OnCharsWasLoaded();
        }

        /*
        Instantiate(Characters[PersIndex[0]], Places[0].position, transform.rotation);
        Instantiate(Characters[PersIndex[1]], Places[1].position, transform.rotation);
        Instantiate(Characters[PersIndex[2]], Places[2].position, transform.rotation);
         */
        
    }

    void LevelLoaded(Scene CurScene, LoadSceneMode Mode)
    {
        ObjVar = GameObject.FindGameObjectWithTag("Vars");

        bool Check = false;
        GameObject[] SceneVars = GameObject.FindGameObjectsWithTag("Vars");

        for (int i = 0; i < SceneVars.Length; i++)
            if (SceneVars[i] != null && SceneVars[i].GetComponent<Variables>().General)
                Check = true;

        if (Check == false)
            SceneVars[0].GetComponent<Variables>().General = true;

        if (ObjVar == null)
        {
           // Instantiate(объектПеременных, transform.position, transform.rotation);
           // ObjVar = GameObject.FindGameObjectWithTag("Vars");

            Debug.LogError("Всё пиздец, нихуя не нашли");
        }


        if (ObjVar.tag == "Vars")
            PersIndex = ObjVar.GetComponent<Variables>().Char;        
    }

    public void CurLoad(string Text)
    {
        Level = "Level_" + Text;
    }

    public void ChangeChar0(int num)
    {
        PersIndex[0] = num;
    }
    public void ChangeChar1(int num)
    {
        PersIndex[1] = num;
    }
    public void ChangeChar2(int num)
    {
        PersIndex[2] = num;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
