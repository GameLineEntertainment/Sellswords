using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Display : MonoBehaviour 
{
    public int Level = 1, Score = 0, Money = 0, Exp = 0, Health_Potion, Resurection_Potion, Energy = 4, Kills = 0, MaxEnergy = 0; // Level - Текущий уровень игрока, Score - Очки, Money - деньги, Exp - опыт, Energy - игровая энергия.
    public int Monsters = 0, Meatman = 0, Norman = 0, Bigman = 0, Fastman = 0, Iceman = 0, Bowman = 0, Strongman = 0,
        RedEnemy, Greenenemy, BlueEnemy, InGameSec, InGameMin, InGameHour; // Заваленные монстры в общем и в частности.
    public  bool[] OpenCharacters;  // Переменная для проверки откртия персонажей

    public Variables vars;

	// Use this for initialization
	void Start () 
    {
        InvokeRepeating("MyUpdate", 1, 1);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void MyUpdate()
    {
        Level = Variables.Level;
        Score = Variables.Score; Money = Variables.Money; Exp = Variables.Exp; Energy = Variables.Energy; // Level - Текущий уровень игрока, Score - Очки, Money - деньги, Exp - опыт, Energy - игровая энергия.
        Monsters = Variables.Monsters; InGameSec = Variables.InGameSec; InGameMin = Variables.InGameMin; InGameHour = Variables.InGameHour; // Заваленные монстры в общем и в частности.
        Kills = Variables.KillsPerSession; MaxEnergy = vars.MaxEnergy;

        OpenCharacters = Variables.OpenCharacters;  // Переменная для проверки откртия персонажей
    }
}
