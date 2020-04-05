using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    public static bool OverGame = false;
    public static bool Win = false;
    public static bool Lose = false;
    public int CurTime;
    public float TimerExit;
    public static bool GodMode;
    public bool GodMod;
    public static bool GamePaused = false;

    public GameObject winLabel;
    public GameObject LooseLabel;
    public GameObject PauseMenu;
    public GameObject Controll;
    public GameObject[] LabelsOver;

    public GameObject HUD;

    public bool Lost;

    public bool Победа = false, Поражение = false;

    // Use this for initialization
    void Start()
    {
        //HUD = GameObject.Find("Canvas");
        OverGame = false;
        HUD.SetActive(false);
        Time.timeScale = CurTime;
        Победа = false; 
        Поражение = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OverGame == true && !GodMod)
        {
            StartCoroutine(ПиздаИгре());
        }      
        Lost = Lose;
        GodMode = GodMod;

        if (Поражение == true)
        {
            Lose = true;
            OverGame = true;
            //Health_Player.Death_Over = true;
        }

        if (Победа)
        {
            Win = true;
            OverGame = true;
        }
    }


    public void OverGameNow()
    {
        HUD.SetActive(true);
        Time.timeScale = 0;
    }

    public void Pause(bool Ch)
    {
        if (Ch)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
            Controll.SetActive(false);
            GamePaused = true;
        }

        if (!Ch)
        {
            GamePaused = false;
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            Controll.SetActive(true);
        }
    }

    public void NextLevel(string Ch)
    {
        Application.LoadLevel(Ch);
    }

    public void LoadGame(string Level)
    {
            OverGame = false;
            Time.timeScale = 1;
            Application.LoadLevel(Level);        
    }


    public void Game_Over_win()
    {
        winLabel.SetActive(true);
    }

    public void Game_Over_loose()
    {
        LooseLabel.SetActive(true);

    }

    IEnumerator ПиздаИгре()
    {
        yield return new WaitForSeconds(3f);

        foreach (GameObject Lable in LabelsOver)
        {
            Lable.SetActive(true);
        }

        if (Win)
            winLabel.SetActive(true);

        if (Lose)
            LooseLabel.SetActive(true);

        OverGameNow();
    }
}