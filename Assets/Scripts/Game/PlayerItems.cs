using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using OldSellswords;

public class PlayerItems : MonoBehaviour {

    public LootItem[] registeredLoot;
    public List<Item> items = new List<Item>();
    public string fileName;
    public bool useCryptForSave;
    public string criptKey;

    // добавление предмета
    //public void addItem(Item item)
    //{
    //    if (item != null)
    //    {
    //        for (int i = 0; i < items.Count; i++)
    //        {
    //            if (items[i].Name.Contains(item.Name))
    //            {
    //                items[i].count += item.count;
    //                return;
    //            }
    //        }
    //        items.Add(item.copy());
    //        saveItems();
    //    }
    //}

    // удаление предмета
    //public bool removeItem(Item item)
    //{
    //    if (item != null)
    //    {
    //        for (int i = 0; i < items.Count; i++)
    //        {
    //            if (items[i].Name == item.loot && item.count <= items[i].count)
    //            {
    //                items[i].count -= item.count;
    //                if (items[i].count < 1)
    //                {
    //                    items.Remove(items[i]);
    //                    saveItems();
    //                }
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}

    //// совершение сделки
    //public void makeOffer(LootItem.Stack sell, LootItem.Stack buy)
    //{
    //    if (removeItem(sell))
    //    {
    //        addItem(buy);
    //        saveItems();
    //    }
    //}

    // сохранение предметов инвенторя
    //public void saveItems()
    //{
    //    StreamWriter sw = new StreamWriter(Application.dataPath + "/" + fileName + ".ncs");
    //    string sp = " ";

    //    for (int i = 0; i < items.Count; i++)
    //    {
    //        // поиск лута в зарегестрированном луте
    //        int id = -1;
    //        for (int j = 0; j < registeredLoot.Length; j++)
    //        {
    //            if (items[i].loot == registeredLoot[j])
    //            {
    //                id = j;
    //                break;
    //            }
    //        }
    //        if (id != -1)
    //        {
    //            sw.WriteLine(Crypt(i + criptKey + "_id" + sp + id));
    //            sw.WriteLine(Crypt(i + criptKey + "_count" + sp + items[i].count));
    //        }
    //        else
    //        {
    //            Debug.LogError("Ошибка сохранения предметов игрока - незарегестрированный предмет: " + items[i].loot.name, this);
    //        }
    //    }

    //    sw.Close();
    //}

    //// загрузка предметов инвенторя
    //public void loadItems()
    //{
    //    if (File.Exists(Application.dataPath + "/" + fileName + ".ncs"))
    //    {
    //        string[] rows = File.ReadAllLines(Application.dataPath + "/" + fileName + ".ncs");
    //        items = new List<LootItem.Stack>();
    //        for (int i = 0; GetValue(rows, i + criptKey + "_id") != string.Empty; i++)
    //        {
    //            LootItem.Stack newItem = new LootItem.Stack();
    //            newItem.loot = registeredLoot[int.Parse(GetValue(rows, i + criptKey + "_id"))];
    //            newItem.count = int.Parse(GetValue(rows, i + criptKey + "_count"));
    //            items.Add(newItem);
    //        }
    //    }
    //}

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
    string GetValue(string[] line, string pattern)
    {
        string result = "";
        foreach (string key in line)
        {
            if (key.Trim() != string.Empty)
            {
                string value = "";
                if (useCryptForSave) value = Crypt(key); else value = key;

                if (pattern == value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0])
                {
                    result = value.Remove(0, value.IndexOf(' ') + 1);
                }
            }
        }
        return result;
    }
}
