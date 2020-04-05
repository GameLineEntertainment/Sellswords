using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class PartyPanelView : UIView
{
    public GameObject RedCharacterButton, GreenCharacterButton, BlueCharacterButton, ActionButton;

    protected override void Awake()
    {
        base.Awake();
        callPosition = new Vector2(thisPosition.anchoredPosition.x, thisPosition.anchoredPosition.y);
        removePosition = new Vector2(thisPosition.anchoredPosition.x, -thisPosition.anchoredPosition.y);
    }

}

