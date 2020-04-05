using UnityEngine;
using System.Collections;

public class Health_Player : MonoBehaviour 
{
    public float CurHealth_Point;            // Количество здоровья
    public GameObject[] Chars;
    public GUIText HealthBar;
	public GameObject[] Bars;
    public bool Couldownn = false;
    public static bool Death_Over;
    public bool LevelLoaded = false;

	// Use this for initialization
	void Start () 
    {        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (CurHealth_Point <= 0 && LevelLoaded)
        {
            GameOver.Lose = true;
            GameOver.OverGame = true;
            Death_Over = true;
        }
	}

    void OnLevelWasLoaded()
    {
        Death_Over = false;

        /*
        Archer HH1 = Chars[0].GetComponent<Archer>();
        Mage HH2 = Chars[1].GetComponent<Mage>();
        Warrior HH3 = Chars[2].GetComponent<Warrior>();

        CurHealth_Point = HH1.Health_Point + HH2.Health_Point + HH3.Health_Point;
         */

        Chars = GameObject.FindGameObjectsWithTag("Char");
        foreach (GameObject Her in Chars)
        {
            CurHealth_Point += Her.GetComponent<Health_Point>().Health;
        }

        HealthBar.text = ("") + CurHealth_Point;

		Check ();
        LevelLoaded = true;
    }

    public void Health(float Damage)  // отнимаем жизни
    {
        if (!Couldownn)
        {
            Couldownn = true;
            CurHealth_Point -= Damage;
            StartCoroutine(Enum());
            
            DefenceGod.GodSave = true;
            HealthBar.text = ("") + CurHealth_Point;
            Check();
        }
    }

    IEnumerator Enum()
    {
        yield return new WaitForSeconds(2);
        Couldownn = false;
    }

    void Check()
    {
        for (int i = 0; i <= CurHealth_Point - 1; i++)
        {
            Bars[i].SetActive(true);
        }

        for (int i = 3; i >= CurHealth_Point; i--)
        {
            Bars[i].SetActive(false);
        }
    }

}
