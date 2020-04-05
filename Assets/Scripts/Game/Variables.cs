using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.Events;
using OldSellswords;

public class Variables : MonoBehaviour
{
    //public static bool GameOpened;  // Проверка на запуск игры игроком
    //public static string UserName; // Имя игрока он же UserID
    public static int Level = 1, // Level - Текущий уровень игрока,
        KillsPerSession = 0,    // Убито за сессию,
        Resurection_Potion,    // Зелья воскрешения
        Health_Potion,        // Зелий лечения
        Score = 0,           // Очки,
        Money = 0,          // Money - деньги,
        Exp = 0,           // Exp - опыт,
        Energy = 4;       //  игровая энергия.

    // Заваленные монстры в общем и в частности.
    public static int Monsters = 0, 
        Meatman = 0, 
        Norman = 0, 
        Bigman = 0, 
        Fastman = 0, 
        Iceman = 0, 
        Bowman = 0, 
        Strongman = 0,
        RedEnemy,  // Цвета противников
        Greenenemy, 
        BlueEnemy, 
        InGameSec, // Время проведённое в игре
        InGameMin, 
        InGameHour;


    public PlayerSettings PlayerSettings;

    public static bool[] OpenCharacters = new bool[9];  // Переменная для проверки откртия персонажей

    public static PlayerItems playerItems;  // предметы хранящиеся у игрока
    public PlayerItems curentPlayerItems;   // загрузка префаба предметов игрока из префаба

    public bool General = false;                            // Главная херня, которая удалаяет остальные vars'ы
    public int EnergyCost;                                 // Цена энергии
    public bool FirstBoot = true;                         // Первый запуск
    public static string UserName = "Me", BooferName;    // Имя пользователя
    public int MinEnergy;                               // Максимальная энергия 
    public int MaxEnergy;                              // Максимальная энергия 
    public int[] Char = new int[3];                   // переменная хранения персонажей, т.е. какие персонажи сейчас выбраны
    [SerializeField]
    CharacterContainer[] SelectedChars;                            //Активные персы
    public static string DataPath = "/Scripts/ScriptableObjects"; // Путь к SriptableObkects
    public const string DataName = "PlayerSettings";             // Имя файла
    public int Левак;                                           // отображение уровня игрока
    public static bool IsRevive = false;          // Проверка загружается ли воскрешение

    public int[] NeedExpForLvl;                 // Количество нужной экспы для левел апа, отсчёт начинается со второго уровня, т.е с 3-го элемента массива
    public Gifts[] GiftsForLvl;                // Класс подарков за уровни
    public Play_Char[] CharUpgrate;           // Ссылка на персонажей, КОТОРЫХ ПРОКАЧИВАЕМ

    static int[] NeedExp;                   // ссылка на нужную экспу для статики
    static Gifts[] RefGifts;               // дополнительная ссылка на подарки
    public static Play_Char[] RefCharUpgrate;  // дополнительная ссылка на прокачку персов
    static Variables LinkVars;                // ссылка на сам Variables

    static GameLevel GameScripts;

    public UnityEvent WasLoaded;

    const int
        Second = 60,
        Minute = 60;

    public int ScorMonsters;

    void Awake()
    {
        if (WasLoaded == null)
            WasLoaded = new UnityEvent();    

        if (General)
        {
            GameObject[] SceneVars = GameObject.FindGameObjectsWithTag("Vars");

            for (int i = 0; i < SceneVars.Length; i++ )
                if (SceneVars[i] != null && SceneVars[i] != gameObject)
                    Destroy(SceneVars[i]);
        }

        if (!General)
        {
            GameObject[] SceneVars = GameObject.FindGameObjectsWithTag("Vars");

            for (int i = 0; i < SceneVars.Length; i++)
                if (SceneVars != null)
                    if (SceneVars[i].GetComponent<Variables>().General)
                        return;
        }


        DontDestroyOnLoad(gameObject);
        NeedExp = NeedExpForLvl;
        RefGifts = GiftsForLvl;
        LinkVars = GetComponent<Variables>();
        GameScripts = GameObject.Find("Scripts").GetComponent<GameLevel>();

        //RefCharUpgrate = CharUpgrate;
    }
    // Use this for initialization
    void Start()
    {
        Левак = Level;
        SceneManager.sceneLoaded += OnLevelLoaded;
        //_playerSettings = Application.dataPath + DataPath + DataName; 
        playerItems = curentPlayerItems;
        //playerItems.saveItems();
        //playerItems.loadItems();

        //foreach (LootItem.Stack item in playerItems.items)
        //    if (item.loot.gameObject.name == "Золото") Money = item.count;  // Гавнокодящий способ вывода денег.
        
        GameScripts.Display_Values();

        WasLoaded.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        //ScorMonsters = Monsters;
    }

    void OnLevelLoaded(Scene CurScene, LoadSceneMode Mode)
    {
        GameScripts = GameObject.Find("Scripts").GetComponent<GameLevel>();
    }

    /// <summary>
    /// Получение опыта игроком
    /// </summary>
    /// <param name="num"></param>
    public static void Grow_Exp(int num) // вызывается каждый раз при убийстве моба
    {
        Exp += num;

        if (Level < 1)
        {
            Level = 1;
        }

        for (int i = 2; i < NeedExp.Length; i++)
        {
            if (Exp >= NeedExp[i] && Level < i)
            {
                Debug.Log(i);
                Debug.Log(NeedExp[i]);
                Level++;
                Analytics.CustomEvent("User Level up", new Dictionary<string, object>
            {
                {"Level", Level},
                {"Money", Money},
                {"Monsters Killed", Monsters},
                {"In Game Minute", InGameMin},
                {"In Game Hour", InGameHour}
            });


                for (int y = 0; y < RefGifts.Length; y++)
                    RefGifts[i].GiveGift();

                break;
            }

                //ну типа блядь оптимизировал, если у нас будет 50 уровней, чтобы не перебирать постоянно всё
            else if (i > Level + 2)
                break;
        }
    }
   

    /// <summary>
    /// Подсчёт индивидуального и общего количества убитых монстров
    /// </summary>
    public static void SummEnemies(string text)
    {
        if (text == "Norman" || text == "Norman(Clone)")
            Norman++;

        if (text == "Meatman" || text == "Meatman(Clone)")
            Meatman++;

        if (text == "Bigman" || text == "Bigman(Clone)")
            Bigman++;

        if (text == "Fastman" || text == "Fastman(Clone)")
            Fastman++;

        if (text == "Iceman" || text == "Iceman(Clone)")
            Iceman++;

        if (text == "Bowman" || text == "Bowman(Clone)")
            Bowman++;

        if (text == "Strongman" || text == "Strongman(Clone)")
            Strongman++;

        Monsters += Meatman + Norman + Bigman + Fastman + Iceman + Bowman + Strongman;
    }

    /// <summary>
    /// Подсчёт индивидуального и общего количества убитых монстров
    /// </summary>
    public static void MiniSummEnemies(string text)
    {
        if (text == "Red")
            RedEnemy++;

        if (text == "Green")
            Greenenemy++;

        if (text == "Blue")
            BlueEnemy++;


        Monsters++;
    }


    public void GiveGift(int Num)
    {

    }


    /// <summary>
    /// Сохранение данных игрока
    /// </summary>
    public static void Save()
    {
        PlayerPrefs.SetInt("Level", Level);  //Пишем первую строку из переменной
        PlayerPrefs.SetInt("Money", Money);  //Пишем вторую строку
        PlayerPrefs.SetInt("Exp", Exp);
       // PlayerPrefs.SetInt("Energy", Energy); // Запоминаем сколько у нас энергии
        PlayerPrefs.SetInt("Monsters", Monsters);  // Количество убитых противников (всего)
        PlayerPrefs.SetString("UserName", UserName); // Записываем имя игрока        

        for (int i = 0; i < OpenCharacters.Length; i++)
        {
            PlayerPrefs.SetString("OpenCharacters[" + i + "]", Convert.ToString(OpenCharacters[i])); // Запись текущего уровня героя
        }

        /* старое
        PlayerPrefs.SetString("OpenCharacters[2]", Convert.ToString(OpenCharacters[2]));  // Сохранение персонажей
        PlayerPrefs.SetString("OpenCharacters[3]", Convert.ToString(OpenCharacters[3]));
        PlayerPrefs.SetString("OpenCharacters[4]", Convert.ToString(OpenCharacters[4]));
        PlayerPrefs.SetString("OpenCharacters[6]", Convert.ToString(OpenCharacters[6]));
        PlayerPrefs.SetString("OpenCharacters[7]", Convert.ToString(OpenCharacters[7]));
        PlayerPrefs.SetString("OpenCharacters[8]", Convert.ToString(OpenCharacters[8]));
         */

        PlayerPrefs.SetInt("MaxEnergy", LinkVars.MaxEnergy);
        PlayerPrefs.SetString("FirstBoot", Convert.ToString(LinkVars.FirstBoot));  // первая загрузка


        PlayerPrefs.SetInt("Health_Potion", Health_Potion);
        PlayerPrefs.SetInt("Resurection_Potion", Resurection_Potion);

        RefCharUpgrate = LinkVars.CharUpgrate; // Ссылка на персов

        for (int i = 0; i < RefCharUpgrate.Length; i++)
        {
            PlayerPrefs.SetInt("CharLevel[" + i + "]", RefCharUpgrate[i].CurrentLevel); // Запись текущего уровня героя
        }

        PlayerPrefs.Save(); // Сохранение данных

        /* ВРЕМЕННО ОТКАЗЫВАЕМСЯ ОТ ЭТОЙ ИДЕИ
         * 
         * 
        DataPath = Application.dataPath + '/' + DataName; //Полный путь к файлу = положение нашей игры + название файла (файл будет сохраняться в папку _Data)
        StreamWriter DataWriter = new StreamWriter(DataPath); //Реализует запись в строку и, если нет нашего файла, создаёт его по указанному пути

        DataWriter.WriteLine(Level);  //Пишем первую строку из переменной
        DataWriter.WriteLine(Money);  //Пишем вторую строку
        DataWriter.WriteLine(Exp);
        DataWriter.WriteLine(OpenCharacters[2]);
        DataWriter.WriteLine(OpenCharacters[3]);
        DataWriter.WriteLine(OpenCharacters[4]);
        DataWriter.WriteLine(OpenCharacters[6]);
        DataWriter.WriteLine(OpenCharacters[7]);
        DataWriter.WriteLine(OpenCharacters[8]);


        DataWriter.Flush();  //Очищает буфер
        DataWriter.Close();  //Закрывает объект StreamWriter
         * 
         */
    }

    /// <summary>
    /// загрузка данных игрока
    /// </summary>
    public static void Load()
    {
        //Analytics.CustomEvent("Menu Loaded");

        if (PlayerPrefs.HasKey("FirstBoot"))
        {
           // GameScripts.NameLabel.SetActive(true);
            Level = PlayerPrefs.GetInt("Level");  //Пишем первую строку из переменной
            LinkVars.FirstBoot = false;
        }

        /*
        if (PlayerPrefs.HasKey("Level"))
        {
            Level = PlayerPrefs.GetInt("Level");  //Пишем первую строку из переменной
            LinkVars.FirstBoot = false;
        }
         * 
         */

        else
        {
           // Level = 1;
            LinkVars.FirstBoot = true;
            //Save();
        }

        //Если мы первый раз запустили игру (так же можно использовать для сброса всех переменных)
        if (LinkVars.FirstBoot)
        {
            Debug.Log("Сброс");
            LinkVars.Reset(); // Откат к заводским настройкам
            //LinkVars.FirstBoot = false;           

            Analytics.CustomEvent("Installed and Lunched");
        }

        Money = PlayerPrefs.GetInt("Money");  //Пишем вторую строку
        Exp = PlayerPrefs.GetInt("Exp");
      //  Energy = PlayerPrefs.GetInt("Energy");
        Monsters = PlayerPrefs.GetInt("Monsters");  // Количество убитых противников (всего)
        UserName = PlayerPrefs.GetString("UserName"); // Записываем имя игрока


        for (int i = 0; i < OpenCharacters.Length; i++)
        {
            OpenCharacters[i] = Convert.ToBoolean(PlayerPrefs.GetString("OpenCharacters[" + i + "]")); // открываем персов
        }

        /* старое
        OpenCharacters[2] = Convert.ToBoolean(PlayerPrefs.GetString("OpenCharacters[2]"));
        OpenCharacters[3] = Convert.ToBoolean(PlayerPrefs.GetString("OpenCharacters[3]"));
        OpenCharacters[4] = Convert.ToBoolean(PlayerPrefs.GetString("OpenCharacters[4]"));
        OpenCharacters[6] = Convert.ToBoolean(PlayerPrefs.GetString("OpenCharacters[6]"));
        OpenCharacters[7] = Convert.ToBoolean(PlayerPrefs.GetString("OpenCharacters[7]"));
        OpenCharacters[8] = Convert.ToBoolean(PlayerPrefs.GetString("OpenCharacters[8]"));
         */

        LinkVars.FirstBoot = Convert.ToBoolean(PlayerPrefs.GetString("FirstBoot"));
        LinkVars.MaxEnergy = PlayerPrefs.GetInt("MaxEnergy");

        Health_Potion = PlayerPrefs.GetInt("Health_Potion");
        Resurection_Potion = PlayerPrefs.GetInt("Resurection_Potion");

        RefCharUpgrate = LinkVars.CharUpgrate; // Ссылка на персов

        for (int i = 0; i < RefCharUpgrate.Length; i++) // цикл переборки всех игровых чаров в массиве
        {
            RefCharUpgrate[i].CurrentLevel = PlayerPrefs.GetInt("CharLevel[" + i + "]"); // Запись текущего уровня героя

            RefCharUpgrate[i].SetHealth(); // Установка жизней героя
        }

        for (int i = 0; i < RefCharUpgrate.Length; i++)
        {
            if (RefCharUpgrate[i].CurrentLevel <= 0)
            {
                RefCharUpgrate[i].CurrentLevel = 1;
                RefCharUpgrate[i].Health = 1;
            }

            RefCharUpgrate[i].SetHealth(); // Записываем жизни в префаб
        }



        GameScripts.Unlock(); // Открытие (снятие замочка) с купленного персонажа

        /* ВРЕМЕННО ОТКАЗЫВАЕМСЯ ОТ ЭТОЙ ИДЕИ
         * 
         * 
        DataPath = Application.dataPath + '/' + DataName; //Полный путь к файлу = положение нашей игры + название файла (файл будет сохраняться в папку _Data)        
        StreamReader DataReader = File.OpenText(DataPath); //Реализует считывание строки, если не находит файл, ругается

        //Считываем данные построчно в том порядке в котором их записали
        Level = Convert.ToInt32(DataReader.ReadLine());
        Money = Convert.ToInt32(DataReader.ReadLine());
        Exp = Convert.ToInt32(DataReader.ReadLine());
        OpenCharacters[2] = Convert.ToBoolean(DataReader.ReadLine());
        OpenCharacters[3] = Convert.ToBoolean(DataReader.ReadLine());
        OpenCharacters[4] = Convert.ToBoolean(DataReader.ReadLine());
        OpenCharacters[6] = Convert.ToBoolean(DataReader.ReadLine());
        OpenCharacters[7] = Convert.ToBoolean(DataReader.ReadLine());
        OpenCharacters[8] = Convert.ToBoolean(DataReader.ReadLine());

        GameObject.Find("Scripts").GetComponent<GameLevel>().Unlock();

        if (Level <= 0)
        {
            Level = 1;
        }

        DataReader.Close();  //Закрывает объект StreamWriter
         * * 
         * 
         */
    }


    /// <summary>
    /// Прокачка персонажа NumOfChar - элемент массива т.е персонажа
    /// </summary>
    public void LevelUpChar(int NumOfChar)
    {
        CharUpgrate[NumOfChar].Upgrate();
        GameScripts.DisplayHealthChar();
    }

    /// <summary>
    /// Проверка полная ли у нас энергия true - полная, false - нет
    /// </summary>
    /// <returns></returns>
    public static bool CheckEnergy()
    {
        if (Energy < LinkVars.MaxEnergy)
            return false;

        return true;
    }


    public static void AddEnergy()
    {
        if (Energy < LinkVars.MaxEnergy)
        {
           // Energy++;
            GameScripts.Display_Values();
            Save();
        }
    }

    public static string[] LeaderBoardCounter()
    {
        string[] Players = new string[] 
           {"Alex Block: Score 1000", 
            "Niko Gogol: Score 900", 
            "Lev Pudge: Score 800", 
            "Lex Pushkin: Score 600", 
            "Miha Lermontov: Score 400", 
            "Petr Chaika: Score 200", 
            "Mickle Broken Nose: Score 100",
           "" + UserName + ": " + Variables.Exp};

        /*
        Player1 = "Alex Block: Score 1000",
        Player2 = "Niko Gogol: Score 900", 
        Player3 = "Lev Pudge: Score 800", 
        Player4 = "Lex Pushkin: Score 600", 
        Player5 = "Miha Lermontov: Score 400", 
        Player6 = "Petr Chaika: Score 200", 
        Player7 = "Mickle Broken Nose: Score 100";
         */

        //string[] Players;

        return Players;
    }


    /// <summary>
    /// Сброс нахер всего к заводским настройкам
    /// </summary>
    public void Reset()
    {
        Level = 1;
        Exp = 0;
       // Money = 0;
        Monsters = 0;
       // MaxEnergy = LinkVars.MinEnergy;
       // Energy = LinkVars.MaxEnergy;
        LinkVars.FirstBoot = false;


        for (int i = 2; i < 8; i++)
            PlayerPrefs.SetString("OpenCharacters[" + i + "]", "false");

        // Вот ниже это просто полный анал... 
        Variables.OpenCharacters[GameScripts.RedCharOpen] = true;
        Variables.OpenCharacters[GameScripts.GreenCharOpen] = true;
        Variables.OpenCharacters[GameScripts.BlueCharOpen] = true;        
        
        while (true)
        {
            GameScripts.NameLabel.SetActive(true);

            if (GameScripts.NameLabel.activeInHierarchy == true)
                break;
        }

        Save();

        GameScripts.Display_Values();
        GameScripts.Unlock();
    }

    public void InputName(string txt) // больше не нужно
    {
        BooferName = txt;
    }

    public void SetUserName(string txt)
    {
        BooferName = txt;

        if (BooferName.Length < 3)
        {
            GameScripts.ErrorNameLabel.SetActive(true);
        }

        else
        {
            UserName = txt;
            Save();
            GameScripts.NameLabel.SetActive(false);
            Analytics.SetUserId(UserName);
            LinkVars.FirstBoot = false;
        }
    }

    public void HideNameError()
    {
        GameScripts.ErrorNameLabel.SetActive(false);
    }



    [System.Serializable]
    public class Gifts
    {
        public string Name;
        public int Level, Money, Hl_Potion, Resurec_Potion;

        public void GiveGift()
        {
            if (Level == Variables.Level)
            {
                Variables.Money += Money;
                Variables.Health_Potion += Hl_Potion;
                Variables.Resurection_Potion += Resurec_Potion;
            }
        }
    }

    public void BuyEnergy()
    {
        if (Variables.Money >= EnergyCost)
        {
            Variables.Money -= EnergyCost;
            LinkVars.MaxEnergy++;
            Variables.Save();
        }
    }


    public static void AddTime(int num)
    {
        InGameSec += num;

        if (InGameSec >= Second)
            InGameMin += InGameSec / Second;

        if (InGameMin >= InGameHour)
            InGameHour += InGameMin / Minute;

    }

    [System.Serializable]
    public class Play_Char
    {
        public string Name;              // Имя элемента
        public GameObject Character;    // Ссылка на префаб (именно префаб, что лежит в проекте, не на сцене)
        //public int Cost;               // Цена чара
        public int Health;            // Жизни
        public int CurrentLevel = 1; // Текузий уровень
        public int[] MaxLevel;        // Максимальный уровень чара это количество элементов массива, а его цена это элемент массива

        public void Upgrate()
        {
            if (CurrentLevel < MaxLevel.Length - 1 && Variables.Money >= MaxLevel[CurrentLevel + 1])
            {
                CurrentLevel++;
                Variables.Money -= MaxLevel[CurrentLevel];
                SetHealth();
                GameScripts.Display_Values();
                Variables.Save();
            }
        }

        public void SetHealth()
        {
            Health = CurrentLevel; // Присваиваем параметру жизней, параметр уровня
            Character.GetComponent<Character>().Health = Health; // Досылаем всё это дело в префаб
        }
    }

}


/*  просто ёбаный позор

   /// <summary>
    /// Получение опыта игроком
    /// </summary>
    /// <param name="num"></param>
    public static void Grow_Exp(int num)
    {
        Exp += num;

        if (Level < 1)
        {
            Level = 1;            
        }

        if (Exp >= 100 && Level < 2)
        {
            Level++;
            Money += 10;
        }

        if (Exp >= 250 && Level < 3)
        {
            Level++;
        }

        if (Exp >= 375 && Level < 4)
        {
            Level++;
            Money += 35;
        }

        if (Exp >= 563 && Level < 5)
        {
            Level++;
        }

        if (Exp >= 844 && Level < 6)
        {
            Level++;
            Money += 75;
        }

        if (Exp >= 1265 && Level < 7)
        {
            Level++;
        }

        if (Exp >= 1898 && Level < 8)
        {
            Level++;
            Money += 100;
        }

        if (Exp >= 2848 && Level < 9)
        {
            Level++;
        }

        if (Exp >= 4272 && Level < 10)
        {
            Level++;
            Money += 150;
        }

        if (Exp >= 6407 && Level < 11)
        {
            Level++;
        }

        if (Exp >= 9611 && Level < 12)
        {
            Level++;
            Money += 200;
        }

        if (Exp >= 14417 && Level < 13)
        {
            Level++;
        }

        if (Exp >= 21625 && Level < 14)
        {
            Level++;
            Money += 250;
        }

        if (Exp >= 32437 && Level < 15)
        {
            Level++;
        }

    }

*/