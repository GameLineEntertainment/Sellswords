using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour 
{
    public static int Kills, Falls, Money;
    public int Count;
    public GUIText countText, FallsText, MoneyText;

	// Use this for initialization
	void Start () 
    {
        Kills = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        SetCountText();
	}

    void SetCountText()
    {
        countText.text = ("KILLS: ") + Kills;
        FallsText.text = ("Falls: ") + Falls;
        //MoneyText.text = ("Money: ") + Money;
    }

}
