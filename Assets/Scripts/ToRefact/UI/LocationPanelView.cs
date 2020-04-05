using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPanelView : UIView
{
    public Button ButtonInfo, ButtonStart;
    public Text NameLocation, InfoLocation;
    public GameObject[] Loot = new GameObject[4];

    protected override void Awake()
    {
        base.Awake();
        removePosition = new Vector2(thisPosition.anchoredPosition.x, thisPosition.anchoredPosition.y);
        callPosition = new Vector2(thisPosition.anchoredPosition.x, -thisPosition.anchoredPosition.y);
    }
}

