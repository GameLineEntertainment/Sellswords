using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OldSellswords;

public sealed class CharacterManager : MonoBehaviour
{
    public CharacterContainer[] CharacterPool;
    [HideInInspector] public List<CharacterContainer> RedGroup = new List<CharacterContainer>();
    [HideInInspector] public List<CharacterContainer> GreenGroup = new List<CharacterContainer>();
    [HideInInspector] public List<CharacterContainer> BlueGroup = new List<CharacterContainer>();
    public CharacterContainer[] CharacterParty = new CharacterContainer[3];
    public CharacterContainer CurrentCharacter;
    [SerializeField] private CharacterManager RegistredChar;
    [SerializeField] private Variables Vars;

    void OnValidate()
    {
        //RegistredChar = Variables.playerItems.GetComponent<CharacterManager>();
        //Vars = FindObjectOfType<Variables>();
       // RegistredChar = Variables.playerItems.GetComponent<CharacterManager>();
    }

    private void Awake()
    {
        // Vars.WasLoaded.AddListener(TakeRef);
        //TakeRef();
        StartCoroutine(MyLoad());
        /*
        load();

        foreach (var character in CharacterPool)
        {
            switch (character.Color)
            {
                case ColorGroup.Red:
                    RedGroup.Add(character);
                    break;
                case ColorGroup.Green:
                    GreenGroup.Add(character);
                    break;
                case ColorGroup.Blue:
                    BlueGroup.Add(character);
                    break;
                default:
                    break;
            }
            if ((CharacterParty[(int)character.Color].Name == "")&&(character.Level>0))
            {
                CharacterParty[(int)character.Color] = character;
            }
            CurrentCharacter = CharacterParty[0];
        }       
        */
    }

    void TakeRef()
    {
        // RegistredChar = Variables.playerItems.GetComponent<CharacterManager>();
        load();
    }

    public void Save()
    {
        /*
        if (RegistredChar != null)
        {
            RegistredChar.CharacterPool = CharacterPool;
            RegistredChar.CharacterParty = CharacterParty;
            print("Хера себе сохранилось!");
        }
        */
        StartCoroutine(MySave());
    }

    IEnumerator MySave()
    {
        yield return null;
        if (RegistredChar != null)
        {
            RegistredChar.CharacterPool = CharacterPool;
            RegistredChar.CharacterParty = CharacterParty;
            //print("Хера себе сохранилось!");
        }
    }

        IEnumerator MyLoad()
    {
        yield return null;
        CharacterPool = RegistredChar.CharacterPool;
        CharacterParty = RegistredChar.CharacterParty;

        foreach (var character in CharacterPool)
        {
            switch (character.Color)
            {
                case ColorGroup.Red:
                    RedGroup.Add(character);
                    break;
                case ColorGroup.Green:
                    GreenGroup.Add(character);
                    break;
                case ColorGroup.Blue:
                    BlueGroup.Add(character);
                    break;
                default:
                    break;
            }
            if ((CharacterParty[(int)character.Color].Name == "") && (character.Level > 0))
            {
                CharacterParty[(int)character.Color] = character;
            }
            CurrentCharacter = CharacterParty[0];
        }
    }

    void load()
    {
        print("Херакс");
        
         CharacterPool = RegistredChar.CharacterPool;
         CharacterParty = RegistredChar.CharacterParty;
        /*
        // if (RegistredChar.CharacterParty.Length <= 0)
        // {
        foreach (var character in CharacterPool)
        {
            switch (character.Color)
            {
                case ColorGroup.Red:
                    RedGroup.Add(character);
                    break;
                case ColorGroup.Green:
                    GreenGroup.Add(character);
                    break;
                case ColorGroup.Blue:
                    BlueGroup.Add(character);
                    break;
                default:
                    break;
            }
            if ((CharacterParty[(int)character.Color].Name == "") && (character.Level > 0))
            {
                CharacterParty[(int)character.Color] = character;
            }
            //   }

            CurrentCharacter = CharacterParty[0];
        }
     */   
    }
    
    
}
