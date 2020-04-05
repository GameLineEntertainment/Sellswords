using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using OldSellswords;

public class PlayerChars : MonoBehaviour
{
    public CharacterContainer[] registeredChar;
    public List<CharacterContainer> Char = new List<CharacterContainer>();
    public string fileName;
    public bool useCryptForSave;
    public string criptKey;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // сохранение выбранных персонажей
    public void saveChars()
    {
        StreamWriter sw = new StreamWriter(Application.dataPath + "/" + fileName + ".ncs");
        string sp = " ";

        // А ещё надо придумать, как сохранять выбранных персов.
        for (int i = 0; i < Char.Count; i++)
        {
            sw.WriteLine(Crypt(i + criptKey + "_id" + sp + Char[i].Level));

            sw.WriteLine(Crypt(i + criptKey + "_id" + sp + Char[i].ActiveSkill));

            // Spells
            for (int j = 0; j < Char[i].Skill.Length; j++)
                sw.WriteLine(Crypt(i + criptKey + "_count" + sp + Char[i].Skill[j].isLock));
        }

        sw.Close();
    }

    string Crypt(string text)
    {
        if (!useCryptForSave) return text;

        string result = string.Empty;
        foreach (char j in text)
        {
            // ((int) j ^ 49) - применение XOR к номеру символа
            // (char)((int) j ^ 49) - получаем символ из измененного номера
            // Число, которым мы XORим можете поставить любое. Эксперементируйте.
            result += (char)((int)j ^ 49);
        }

        return result;
    }
}
