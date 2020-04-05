using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using OldSellswords;

#if UNITY_ADS
using UnityEngine.Advertisements; // only compile Ads code on supported platforms
#endif



public class GameLevel : MonoBehaviour
{

    public bool IsMenu = false;

    #region ButtonChange


    public static bool Turn = false, Attack = false;
    [Header("ButtonChange")]
    public Texture2D icon;                                                          //\\переменная для иконки
    //public bool WasTurningRigh; //Поворачивались ли мы только что dghfdj //  Если что то сломалось, то скорее всего из-за махинаций с этой переменной IsLeft  

    private Character Pos;                                                      //\\Переменная для изменения позиции Персонажей
    //public Character[] Characters;

    public RuningEnergy EnergyScript;

    #endregion

    #region GameOver

    [Header("GameOver")]
    public int CurTime;                                                                 //Переменная текущего времени
    public static bool OverGame = false, Win = false, Lose = false;                      // Внешние переменные

    public float TimerExit;
    public static bool GodMode;                                                       //Настройки год мода
    [SerializeField]
    private bool Победа = false, Поражение = false, Lost = false, GodMod = false, GameWasEnded = false;     //Настройки год мода и настроек активации победы и поражений
    public static bool GamePaused = false;                                          //Проверка на пазе ли игра

    public GameObject winLabel, LooseLabel, PauseMenu, Controll, Numlvl, Nxtlvl, Scores;  //Лейблы Game Over'a /Scores очки /winLabel, LooseLabel иконки победы, поражения /Numlvl номер уровня выводящегося на экран 
    public GameObject[] LabelsOver;

    public GameObject HUD;

    [SerializeField]
    List<Item> rewardItems;   // хранилище выбитых с монстров вещей

    #endregion

    #region Counter

    public static int Kills, Falls, Money;
    [Header("Counter")]
    public int Count;
    public GameObject countText, FallsText, MoneyText;

    #endregion

    #region DefenceGod

    public static bool GodSave;
    public float Range = 1;
    public float speed;
    public bool start;

    #endregion

    #region IsKillZ

    public bool IsKillZ;

    #endregion

    #region Health_Player

    [Header("Health_Player")]
    public float CurHealth_Point;            // Количество здоровья
    public float Max_Heath;
    public GUIText HealthBar;
    public GameObject[] Bars;
    public GameObject[] Chars;
    public bool Couldownn = false, LevelLoaded = false;
    public static bool Death_Over;

    #endregion

    #region Menu

    [Header("Menu")]
    public int TutorNum = 0;
    public GameObject Lvl_Player, Money_Player, HealthPotion, ResurectPotion, EnergyLabel, NameLabel, ErrorNameLabel; // Отображение Уровеня и денег игрока и его баночек, энергии
    public Text HighScore; //  хай скор
    public GameObject Menu, Settings, AboutGame, AudioSlider, ChangeLevel, ChangeChars, TutorShow, LeaderBoard; //Menu объект канваса меню, который находится в игровой сцене, Settings канвас настроек, AboutGame в главном меню послание от разработчиков, AudioSlider слайдер звука, TutorShow обучение, leaderboaed - лидер борды
    private bool VisMenu = false, VisSettings = false, VisAboutGame = false, sound = false, VisChangeLevel = false, VisChangeChars = false, VisTutorShow = false, VisLeaderBoard = false; // Проверка видимости одноименных объектов, что на строку выше
    private Slider s;
    public GameObject[] Classes;  // Переключение между классами персонажей
    public GameObject[] Labels;   // Изменение лейблов - картинок выбранного перса
    public Text[] HealthLables;   // Ссылка на дисплей жизней каждого из персов
    public Sprite[] LabelsSprites;
    public GameObject[] InfoChar, TutorPics; // Переключение информации  о персонажах, TutorPics изображения обучения.
    public GameObject[] CharOnStage; // Персонаж на сцене
    public int EnergyRestoreMin;
    public GameObject LabelNoEnergy;
    public Clocks CouldownnClock;    // Ссылка на часы восстановления энергии

    #endregion

    #region Moneytization

    [Header("Moneytization")]
    public GameObject[] Unlocker;
    public GameObject[] BuyButton;
    public int GreenCharOpen, BlueCharOpen, RedCharOpen;
    public Text[] LabelCharCost;
    public int CharCost; // Цена за перса

    #endregion

    #region Game

    [Header("Menu")]
    public float TimeOfSession = 0f; // вермя сессии
    public float TimeBeforeHit = 0f; // Время перед первым ударом

    #endregion

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {

        if (!IsMenu)
        {
            #region GameOver

            //HUD = GameObject.Find("Canvas");
            GameWasEnded = false;
            OverGame = false;
            HUD.SetActive(false);
            Time.timeScale = CurTime;
            Победа = false;
            Поражение = false;
            Lose = false;
            Win = false;
            Display_Potion();
            Analytics.CustomEvent("Sessions");

            if (Variables.IsRevive == false)
            {
                Variables.Score = 0;
                Variables.KillsPerSession = 0;
            }

            if (Variables.IsRevive == true) Variables.IsRevive = false;
            // инициализация массива с наградой в виде предметов
            rewardItems = new List<Item>();

            #endregion

            #region Counter

            Kills = 0;

            #endregion

            EnergyScript = GetComponent<RuningEnergy>();

            Analytics.CustomEvent("Game scene Lunched");
        }

        #region Menu

        s = AudioSlider.GetComponent<Slider>();
        s.value = AudioListener.volume;

        for (int i = 0; i < LabelCharCost.Length; i++)
        {
            LabelCharCost[i].text = "" + CharCost + " Gold";
        }

        if (IsMenu)
        {
            SceneManager.sceneLoaded += OnMenuLoaded;
            //Display_Values();
            Variables.Load(); // загружаем сохраненённые данные

            CheckEnergy();
            Display_Values();
            //Display_Potion();

            CouldownnClock = GameObject.Find("Clocks").GetComponent<Clocks>();
            InvokeRepeating("CheckEnergy", 0, 30);
            // Debug.Log("Начали проверять энергию");


        }

        #endregion


    }

    // Update is called once per frame
    void Update()
    {
        if (!IsMenu)
        {
            #region ButtonChange

            //if (Input.GetKeyDown(KeyCode.LeftArrow))
            //    if (!OverGame)
            //        Left();

            //if (Input.GetKeyDown(KeyCode.RightArrow))
            //    if (!OverGame)
            //        Right();

            #endregion

            //Подумать о ожизни. Нахер мне тут Lost = Lose и GodMode = GodMod, наверняка это можно послать нахуй
            #region GameOver

            /* Было раньше в последней рабочей версии
            if (OverGame == true && !GodMod)
            {
                StartCoroutine(ПиздаИгре());
            }
            */

            Lost = Lose;
            GodMode = GodMod;

            if (Поражение == true)
            {
                Lose = true;
                OverGame = true;
            }

            if (Победа)
            {
                Win = true;
                OverGame = true;
            }

            if (OverGame &&!GameWasEnded && !GodMod)
            {
                GameWasEnded = true;
                Invoke("ПиздаИгре", 3);
            }
            #endregion

            // исправить это дерьмо и запихнуть в функцию отнимания жизней. жрёт производительность, минус лишняя переменная
            #region DefenceGod

            if (GodSave)
            {
                transform.localScale += new Vector3(Range, Range, Range) * speed * Time.deltaTime;
                if (transform.localScale.magnitude > 25)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    GodSave = false;
                }
            }

            #endregion

            // исправить это дерьмо и запихнуть в функцию отнимания жизней. жрёт производительность, минус лишняя проверка
            #region Health_Player

            if (CurHealth_Point <= 0 && LevelLoaded)
            {
                Lose = true;
                OverGame = true;
                Death_Over = true;
            }

            #endregion

            #region Game

            TimeCounter();

            #endregion
        }

    }

    // добавление предметов в награду для завершения уровня
    public void addRewardItems(Item[] items)
    {
        for (int i = 0; i < items.Length; i++) {
            rewardItems.Add(items[i]);
        }
    }

    void OnMenuLoaded(Scene CurScene, LoadSceneMode Mode)
    {
        if (IsMenu)
        {
            Variables.Load(); // загружаем сохраненённые данные

            //CheckEnergy();                
            CouldownnClock = GameObject.Find("Clocks").GetComponent<Clocks>();
            InvokeRepeating("CheckEnergy", 0, 30);
            Display_Potion();
            Display_Values();
        }
    }


    public void CheckEnergy()
    {
        if (!Variables.CheckEnergy())
        {
            if (!CouldownnClock.Activated)
            {
                CouldownnClock.SetCoulDown(EnergyRestoreMin);
            }
        }

    }

    public void OnValuesWasLoaded()
    {
    }


    public void Display_Values()
    {
        if (IsMenu)  //ВНИМАНИЕ МОГЛО СЛОМАТЬ ТХ!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            Money_Player.GetComponent<Text>().text = "Money:  " + Variables.Money;
            Lvl_Player.GetComponent<Text>().text = "Level:  " + Variables.Level;
            EnergyLabel.GetComponent<Text>().text = "Energy: " + Variables.Energy;

            Display_Potion();
            DisplayHighScore();
        }
    }

    public void OnCharsWasLoaded()
    {
        #region ButtonChange       
        //Characters = FindObjectsOfType<Character>();
        #endregion

        #region Health_Player

        Death_Over = false;

        Chars = GameObject.FindGameObjectsWithTag("Char");
        foreach (GameObject Her in Chars)
        {
            CurHealth_Point += Her.GetComponent<Character>().Health;
        }

        //HealthBar.text = ("") + CurHealth_Point;

        Max_Heath = CurHealth_Point;

        Check();
        LevelLoaded = true;

        #endregion
    }




	#region ButtonChange

	//public void Left()
	//{
	//    if (WasTurningRigh)
	//    {
	//        ResetCoolDown();
	//    WasTurningRigh = false;
	//    }


	//    if (EnergyScript != null && !Characters[0].CoolDown && !Characters[1].CoolDown && !Characters[2].CoolDown)
	//            EnergyScript.DecreaseEnergy();
	//   // foreach (GameObject Char in Characters)
	//   for(int i = 0; i < Characters.Length; i++)
	//    {
	//        //ВНИМАНИЕ!!!!!!!!!!!!!!!!!!!!!!!!!!    БЛЯДСТВО В КОДЕ!!!
	//        Pos = Characters[i];
	//        Pos.TargetPositions(true);
	//        Characters[i].TurningChars();
	//       // Turn = true;

	//        if (GamePaused == true)
	//            Time.timeScale = 1;
	//    }
	//}

	//public void Right()
	//{
	//    if (!WasTurningRigh)
	//    {            
	//        ResetCoolDown();
	//    WasTurningRigh = true;
	//    }


	//    if (EnergyScript != null && !Characters[0].CoolDown && !Characters[1].CoolDown && !Characters[2].CoolDown)
	//        EnergyScript.DecreaseEnergy();

	//    for (int i = 0; i < Characters.Length; i++)
	//    {
	//        //ВНИМАНИЕ!!!!!!!!!!!!!!!!!!!!!!!!!!    БЛЯДСТВО В КОДЕ!!!
	//        Pos = Characters[i];
	//        Pos.TargetPositions(false);
	//        Characters[i].TurningChars();
	//        // Turn = true;

	//        if (GamePaused == true)
	//            Time.timeScale = 1;
	//    }       
	//}

	//void ResetCoolDown()
	//{
	//    for (int i = 0; i < Characters.Length; i++)
	//    {
	//        Characters[i].CoolDown = false;
	//Characters[i].CancelInvoke("CoulDownTimer");
	//        //Characters[i].StopAllCoroutines();
	//    }
	//}

	//IEnumerator Unturn()
 //   {
 //       yield return new WaitForSeconds(0.2f);
 //       Turn = true;
 //   }

    #endregion


    #region Resurect

    /// <summary>
    /// Воскрешение игрока
    /// </summary>
    public void Resurection()
    {
        if (Variables.Resurection_Potion > 0)
        {
            Variables.Resurection_Potion--;
            Variables.IsRevive = true;
            Variables.Save();
            BootLevel(1);

            /*
            Variables.Resurection_Potion--;

            CurHealth_Point = Max_Heath;

            Death_Over = false;
            OverGame = false;
            HUD.SetActive(false);
            Time.timeScale = CurTime;
            Победа = false;
            Поражение = false;
            Lose = false;
            Win = false;



            OverGame = false;
            HUD.SetActive(false);
            PauseMenu.SetActive(false);
            Death_Over = false;

            GameObject Left, Right, Down;

            Left = GameObject.FindGameObjectWithTag("Left_Enemy");
            Left.GetComponent<EnemyAI>().Health_Point = -10;
            Left.GetComponent<EnemyAI>().CheckDeath();

            Right = GameObject.FindGameObjectWithTag("Right_Enemy");
            Right.GetComponent<EnemyAI>().Health_Point = -10;
            Right.GetComponent<EnemyAI>().CheckDeath();

            Down = GameObject.FindGameObjectWithTag("Down_Enemy");
            Right.GetComponent<EnemyAI>().Health_Point = -10;
            Right.GetComponent<EnemyAI>().CheckDeath();

            Controll.SetActive(true);
            GamePaused = false;
            Time.timeScale = 1;

             */

        }
    }

    #endregion


    #region GameOver

    public void OverGameNow()
    {
        HUD.SetActive(true);

        Variables.Energy--;

        Time.timeScale = 0;

        Analytics.CustomEvent("Time of session", new Dictionary<string, object>
            {
                {"TimeOfSession", TimeOfSession},
                {"TimeBeforeLooseHealth", TimeBeforeHit},
                {"Monsters Killed", Variables.Monsters}
            });

        Variables.AddTime(Convert.ToInt32(TimeOfSession));


        //  Variables.Save();

        //#if UNITY_ANDROID
        // Advertisement.Show();
        // #endif
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



    public void LoadMenu()
    {
        OverGame = false;
        Time.timeScale = 1;
        CancelInvoke();
        StopAllCoroutines();
        System.GC.Collect(); // немедленная сборка мусора
        Application.LoadLevel(0);
    }


    public void Game_Over_win()
    {
        winLabel.SetActive(true);
    }

    public void Game_Over_loose()
    {
        LooseLabel.SetActive(true);

    }

    void ПиздаИгре()
    {
        OverGame = false;
        // сохранение награды
        print("Пизда игре");
        //for (int i = 0; i < rewardItems.Count; i++) Variables.playerItems.addItem(rewardItems[i]);

        Variables.Save(); // Сохранение данных

      //  yield return new WaitForSeconds(3f);

        foreach (GameObject Lable in LabelsOver)
        {
            Lable.SetActive(true);
        }

        if (Win)
        {
            winLabel.SetActive(true);
            LooseLabel.SetActive(false);
            Nxtlvl.SetActive(true);
        }

        if (Lose)
        {
            LooseLabel.SetActive(true);
            winLabel.SetActive(false);
            Nxtlvl.SetActive(false);
        }

        //ВНИМАНИЕ!!!
        //НИЖЕ НАХОДИТСЯ ГАВНО СТРОКИ!!!
        Numlvl.GetComponent<Text>().text = "Level " + ChangeCharacters.Level;
        Scores.GetComponent<Text>().text = "Score: " + Variables.Score;

        OverGameNow();
    }

    #endregion



    #region Counter

    public void SetCountText()
    {
        countText.GetComponent<Text>().text = ("KILLS: ") + Variables.KillsPerSession;
        //FallsText.text = ("Falls: ") + Falls;
        //MoneyText.text = ("Money: ") + Money;
    }

    #endregion



    #region IsKillZ

    void OnTriggerEnter(Collider other)
    {
        if (IsKillZ)
        {
            Destroy(other.gameObject);
            other.gameObject.tag = "Untagged";
            //Destroy(gameObject);
            Counter.Falls++;
        }

        if (!IsKillZ)
        {
            other.gameObject.tag = "Untagged";
        }
    }

    #endregion



    #region Health_Player

    public void Health(float Damage)  // отнимаем жизни
    {
        if (!Couldownn)
        {
            Couldownn = true;

            if (GodMod)
                Damage = 0;

            CurHealth_Point -= Damage;
            StartCoroutine(Enum());

            DefenceGod.GodSave = true;
            //HealthBar.text = ("") + CurHealth_Point;
            Check();
        }
    }

    public void Regeneration()
    {
        if (CurHealth_Point < Max_Heath && Variables.Health_Potion > 0)
        {
            CurHealth_Point++;
            Variables.Health_Potion--;
            Variables.Save();
        }

        Check();
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

        for (int i = 5; i >= CurHealth_Point; i--)
        {
            if (i < 0)
                break;

            Bars[i].SetActive(false);
        }
    }

    #endregion

    #region DisplayHealthChar

    public void DisplayHealthChar()
    {
        for (int i = 0; i < HealthLables.Length; i++)
        {
            HealthLables[i].text = "Health: " + Variables.RefCharUpgrate[i].Health; // Convert.ToString(Variables.RefCharUpgrate[i].Health);
        }
    }

    #endregion



    #region Menu

    //переключение видимых окон
    public void GameMenu(int num)
    {
        switch (num)
        {
            case 0: //Видимость меню (которое в игре)
                VisMenu = !VisMenu;
                Pause(VisMenu);
                break;

            case 1: //Видимость настроек
                VisSettings = !VisSettings;
                Menu.SetActive(!VisSettings);
                Settings.SetActive(VisSettings);
                break;

            case 2: //Видимость выбора уровней
                VisChangeLevel = !VisChangeLevel;
                Menu.SetActive(!VisChangeLevel);
                ChangeLevel.SetActive(VisChangeLevel);
                break;

            case 3: //Видимость выбора персонажей вместо старого
                VisChangeChars = !VisChangeChars;
                Menu.SetActive(!VisChangeChars);
                ChangeChars.SetActive(VisChangeChars);
                break;

            case 4: //Видимость текста от разработчиков
                VisAboutGame = !VisAboutGame;
                AboutGame.SetActive(VisAboutGame);
                break;


            case 5: //Видимость выбора персонажей
                VisChangeChars = !VisChangeChars;
                ChangeLevel.SetActive(!VisChangeChars);
                ChangeChars.SetActive(VisChangeChars);
                break;

            case 6: // Видимость обучения
                VisTutorShow = !VisTutorShow;
                Menu.SetActive(!VisTutorShow);
                TutorShow.SetActive(VisTutorShow);
                break;

            case 7: //Видимость выбора уровней                
                VisLeaderBoard = !VisLeaderBoard;
                Menu.SetActive(!VisLeaderBoard);
                LeaderBoard.SetActive(VisLeaderBoard);
                DisplayHighScore(); // отображаем лидер борд
                break;
        }
    }

    public void Changelvl(string lvl)
    {
        ChangeCharacters.Level = lvl;
    }


    public void CheckLabel()
    {
        int num = ChangeCharacters.PersIndex[0];
        Labels[0].GetComponent<Image>().sprite = LabelsSprites[num];
        Labels[0].GetComponent<Image>().color = Color.white;

        num = ChangeCharacters.PersIndex[1];
        Labels[1].GetComponent<Image>().sprite = LabelsSprites[num];
        Labels[1].GetComponent<Image>().color = Color.white;

        num = ChangeCharacters.PersIndex[2];
        Labels[2].GetComponent<Image>().sprite = LabelsSprites[num];
        Labels[2].GetComponent<Image>().color = Color.white;
    }


    //Загрузка нового уровня
    public void NextLevel()
    {
        string Txt = ChangeCharacters.Level;      //Считываем текущий уровень
        int num = Convert.ToInt32(Txt);           //Преобразуем переменную string в int
        num++;
        ChangeCharacters.Level = Convert.ToString(num);  //Перезаписываем уровень на загружаемый
        CancelInvoke();
        StopAllCoroutines();
        System.GC.Collect(); // немедленная сборка мусора
        Application.LoadLevel("Level_" + Txt);           //Загружаем
    }

    // смена картинок обучения, переключение между бестиарием и началом
    public void Bestiary(int Num)
    {
        TutorPics[TutorNum].SetActive(false);
        TutorNum = Num;
        TutorPics[TutorNum].SetActive(true);
    }


    // смена картинок обучения
    public void ChangeTutor(bool Next)
    {
        if (Next)
        {
            TutorPics[TutorNum].SetActive(false);
            TutorNum++;

            if (TutorNum >= TutorPics.Length) // проверка, если число больше, чем у нас картинок с обучением ставим первую
            {
                TutorNum = 0;
                TutorPics[TutorNum].SetActive(true);
            }

            TutorPics[TutorNum].SetActive(true);
        }

        if (!Next)
        {
            TutorPics[TutorNum].SetActive(false);
            TutorNum--;

            if (TutorNum < 0) // проверка, если число меньше, чем у нас картинок с обучением ставим первую
            {
                TutorNum = 0;
                TutorPics[TutorNum].SetActive(true);
            }

            TutorPics[TutorNum].SetActive(true);
        }



    }

    //Команды загрузок уровней
    public void BootLevel(int num)
    {
        switch (num)
        {
            case 0: // Выключение игры
                Application.Quit();
                break;

            case 1: //Загрузка уровня                
                CancelInvoke();
                StopAllCoroutines();
                Application.LoadLevel("Level_" + ChangeCharacters.Level);
                break;

            case 2: //Открытие группы вк
                Application.OpenURL("https://vk.com/gamelinestudio");
                break;
        }
    }

    public void MiniTHLoadLevel(int num)
    {
        switch (num)
        {
            case 0: // Выключение игры
                Application.Quit();
                break;

            case 1: //Загрузка уровня       
               // if (Variables.Energy >= 1)
               // {
                    System.GC.Collect(); // немедленная сборка мусора
                    CancelInvoke();
                    StopAllCoroutines();
                    Application.LoadLevel(Convert.ToInt32(ChangeCharacters.Level));
               // }

               // else
               //     ShowNoEnergy(true);
                break;

            case 2: //Открытие группы вк
                Application.OpenURL("https://vk.com/gamelinestudio");
                break;
        }
    }

    public void ShowNoEnergy(bool Bin)
    {
        LabelNoEnergy.SetActive(Bin);
    }

    //Настройки игры
    public void SettingQuality(int num)
    {
        QualitySettings.SetQualityLevel(num, true);
    }

    //Настройка звука
    public void setVolume(float v)
    {
        AudioListener.volume = s.value;
    }

    //Переключатель звука
    public void AudioToggle()
    {
        sound = !sound;
    }


    //Команды загрузок уровней
    public void ВчёмПроблемаПацанчеГ(int num)
    {
        switch (num)
        {
            case 0: // Выключение игры
                Application.Quit();
                break;

            case 1: //Загрузка уровня
                    //ChangeCharacters.Level = num;
                CancelInvoke();
                StopAllCoroutines();
                Application.LoadLevel(ChangeCharacters.Level);
                break;
        }
    }

    //Выбор класса
    public void ChangeClass(int num)
    {
        foreach (GameObject хуй in Classes)
            хуй.SetActive(false);

        Classes[num].SetActive(true);
    }

    public void ShowInfoChar(int num)
    {
        foreach (GameObject хуй in InfoChar)
            хуй.SetActive(false);


        foreach (GameObject хуй in CharOnStage)
            хуй.SetActive(false);

        InfoChar[num].SetActive(true);
        CharOnStage[num - 1].SetActive(true);
        DisplayHealthChar();
    }

    public void Display_Potion()
    {
        HealthPotion.GetComponent<Text>().text = "" + Variables.Health_Potion;
        ResurectPotion.GetComponent<Text>().text = "" + Variables.Resurection_Potion;
    }

    public void DisplayHighScore()
    {
        string[] Score = Variables.LeaderBoardCounter();

        HighScore.text = "";

        for (int i = 0; i < Score.Length; i++)
        {
            HighScore.text += Score[i] + ('\n') + ('\n');
        }
    }


    #endregion


    #region Moneytization

    /// <summary>
    /// Покупка персонажа и снимание бабленского
    /// </summary>
    /// <param name="Num"></param>
    public void BuyChar(int Num)
    {
        if (Variables.Money >= CharCost && !Variables.OpenCharacters[Num])
        {
            Variables.Money -= CharCost;

            Variables.OpenCharacters[Num] = true;

            /*
            switch (Num)
            {
                case 1:
                    Variables.OpenCharacters[1] = true;
                    break;

                case 3:
                    Variables.OpenCharacters[3] = true;
                    break;

                case 4:
                    Variables.OpenCharacters[4] = true;
                    break;

                case 6:
                    Variables.OpenCharacters[6] = true;
                    break;

                case 7:
                    Variables.OpenCharacters[7] = true;
                    break;

                case 8:
                    Variables.OpenCharacters[8] = true;
                    break;
            }
             */

            Variables.Save();

            Unlock();
        }
    }


    /// <summary>
    /// Открытие купленых персонажей
    /// </summary>
    public void Unlock()
    {
        if (IsMenu)
        {
            if (Unlocker != null || BuyButton != null)
            {
                for (int i = 0; i < Variables.OpenCharacters.Length; i++)
                {
                    /*
                    Debug.Log(gameObject.name);
                    Debug.Log(Unlocker.Length);
                    Debug.Log(i);
                    Debug.Log(Unlocker[i]);
                    Debug.Log(Unlocker[i].name);
                     */
                    int num = i + 1; // поправка для работы с массивом

                    if (Unlocker[i] != null)
                        if (Unlocker[i].name == "Lock_" + num && Variables.OpenCharacters[i])
                        {
                            //Destroy(Unlocker[i]);]
                            Unlocker[i].SetActive(false);
                        }


                    if (BuyButton[i] != null)
                        if (BuyButton[i].name == "Buy_" + num && Variables.OpenCharacters[i])
                        {
                            // Destroy(BuyButton[i]);
                            BuyButton[i].SetActive(false);
                        }
                }

                // StartCoroutine(HideLocks());
            }
        }


        /*
        if (Variables.OpenCharacters[0] == true)
        {
            Destroy(Unlocker[0]);
            Destroy(Unlocker[1]);
        }

        if (Variables.OpenCharacters[1] == true)
        {
            Destroy(Unlocker[2]);
            Destroy(Unlocker[3]);
        }

        if (Variables.OpenCharacters[2] == true)
        {
            Destroy(Unlocker[4]);
            Destroy(Unlocker[5]);
        }

        if (Variables.OpenCharacters[3] == true)
        {
            Destroy(Unlocker[6]);
            Destroy(Unlocker[7]);
        }

        if (Variables.OpenCharacters[4] == true)
        {
            Destroy(Unlocker[8]);
            Destroy(Unlocker[9]);
        }
        if (Variables.OpenCharacters[5] == true)
        {
            Destroy(Unlocker[10]);
            Destroy(Unlocker[11]);
        }

        if (Variables.OpenCharacters[6] == true)
        {
            Destroy(Unlocker[12]);
            Destroy(Unlocker[13]);
        }

        if (Variables.OpenCharacters[7] == true)
        {
            Destroy(Unlocker[14]);
            Destroy(Unlocker[15]);
        }

        if (Variables.OpenCharacters[8] == true)
        {
            Destroy(Unlocker[16]);
            Destroy(Unlocker[17]);
        }
         */
    }


    public IEnumerator HideLocks()
    {
        yield return new WaitForSeconds(2);


        for (int i = 0; i < Variables.OpenCharacters.Length; i++)
        {
            int num = i + 1; // поправка для работы с массивом

            Debug.Log(Unlocker[i].name);

            if (Unlocker[i] != null)
                if (Unlocker[i].name == "Lock_" + num && Variables.OpenCharacters[i])
                {
                    //Destroy(Unlocker[i]);]
                    Unlocker[i].SetActive(false);
                }


            if (BuyButton[i] != null)
                if (BuyButton[i].name == "Buy_" + num && Variables.OpenCharacters[i])
                {
                    // Destroy(BuyButton[i]);
                    BuyButton[i].SetActive(false);
                }
        }
    }


    /// <summary>
    /// Покупка зелий и снимание бабленского
    /// </summary>
    public void BuyPotion(int Num)
    {
        switch (Num)
        {
            case 1:
                if (Variables.Money >= 40)
                {
                    Variables.Money -= 40;
                    Variables.Health_Potion++;
                    Display_Potion();
                    Display_Values();
                }
                break;


            case 2:
                if (Variables.Money >= 120)
                {
                    Variables.Money -= 120;
                    Variables.Resurection_Potion++;
                    Display_Potion();
                    Display_Values();
                }
                break;
        }
        Variables.Save();
    }




    public void GerEnergy()
    {
#if UNITY_ADS
            if (Advertisement.IsReady())
            {
                Advertisement.Show();

                Variables.AddEnergy();

                Display_Values();
            }
             
           // Variables.AddEnergy();
#endif

    }



    #endregion


    #region Game

    /// <summary>
    /// Счётчики времени
    /// </summary>
    public void TimeCounter()
    {
        TimeOfSession += 1 * Time.deltaTime; // время сесси

        if (CurHealth_Point == Max_Heath)
            TimeBeforeHit += 1 * Time.deltaTime; // время перед до потери первой жизни
    }

    #endregion


}
