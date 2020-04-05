using UnityEngine;
//using UnityEditor.Audio;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Windows;
using System.Collections;
using OldSellswords;

public class Test : MonoBehaviour
{
    public string DataPath = "/ScriptableObjects/"; // Путь к SriptableObkects
    public string DataName = "PlayerSettings";             // Имя файла
    [SerializeField]
    private Characters _characters;
    //[SerializeField] Characters _chars;
    //[SerializeField] PlayerSettings _playerSettings;
    //public bool Activate;
    //public PrefabType GetPrefabType(Object target);
    //public static GameObject ReplacePrefab(GameObject go, Object targetPrefab, ReplacePrefabOptions options = ReplacePrefabOptions.Default); 
    //public GameObject ReplacePrefab; 
    //public GameObject go, targetPrefab;
    //public Camera m_Camera;
    //public GameObject Text;

    /*
public AudioMixer Микшер;
AudioMixerGroup Group;
float healthBarLength, _curHealth, maxHealth = 5;
public GameObject HealthBar, TextBar;
Scrollbar Bar;
Text txt;
public float Lengh;
public Camera m_Camera;
*/
    /*
            public enum Anim { Standat = 0, Throw = 1, Cast = 2, Dance = 3 }
            public Anim TypeOfAnim = Anim.Throw;

        public GameObject prefab;
        public Transform parent;
        public bool IsTrue;
        */
    //[SerializeField]
    // Additive_Test obj;

    private void Awake()
    {
        // Additive_Test at = GetComponent<Additive_Test>();
        // Additive_Test.Loaded.AddListener(loa);
        // obj.num = 25;
        //obj.Minute = 15;
        //Additive_Test.
        //Instantiate(at, transform.position, transform.rotation);
    }

    void loa()
    {
        print("Shit is overload pants");
    }

    IEnumerator loadChars(CharacterContainer[] CharacterPool)
    {
        _characters.GameCharacters = new CharacterContainer[CharacterPool.Length];
        for (int i = 0; i < CharacterPool.Length; i++)
        {
            _characters.GameCharacters[i] = CharacterPool[i];
            yield return new WaitForEndOfFrame();
        }
    }

    // Use this for initialization
    void Start()
    {
        //var cm = GetComponent<CharacterManager>().CharacterPool;
        // StartCoroutine(loadChars(cm));
        // _playerSettings.GameCharacters = cm;

        //_playerSettings.RedCharacter.Name = "";
        //_playerSettings.GreenCharacter.Name = "";
        //_playerSettings.BlueCharacter.Name = "";
        //_playerSettings.CheckEmptyCharacters();
        //Text.GetComponent<Text>().text = ("Falls: ") + Text;
        // m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //ReplacePrefabOptions ReplacePrefab = ReplacePrefabOptions.ConnectToPrefab.go;
        // PrefabUtility.ReplacePrefab(go, targetPrefab);
        // Микшер.FindMatchingGroups("Music").SetValue(-20,0);

        //Bar = HealthBar.GetComponent<Scrollbar>();
        //txt = TextBar.GetComponent<Text>();
        // GUI.Box(new Rect(10, 40, healthBarLength, 20), _curHealth + "/" + maxHealth);

        // Application.OpenURL("https://yandex.ru/images/search?text=Samsung%20Gear%20шлем&uinfo=sw-1920-sh-1080-ww-1903-wh-1011-pd-1-wp-16x9_1920x1080");
    }


    // Update is called once per frame
    void Update()
    {
        /*
        GetComponent<MiniEnemyAI>().Die();
        if (Activate)
        {

        }
        */
        // transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
        //Object.GetComponent<Character>().CanAttack = false;
        // txt.text = Convert.ToString(Lengh);
        // Bar.size = (Lengh / maxHealth);

        Vector2 inputAxis = Vector2.zero;
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
    }
}
