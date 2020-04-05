using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OldSellswords;
public class CharacterButton : MonoBehaviour
{
    public CharacterContainer Character;

    void Start()
    {
        GetComponent<Image>().sprite = Character.Icon;
        GetComponentInChildren<Text>().text = Character.Name;
        if (Character.Level == 0)
        {
            transform.Find("Lock").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("Lock").gameObject.SetActive(false);
        }
    }
}

