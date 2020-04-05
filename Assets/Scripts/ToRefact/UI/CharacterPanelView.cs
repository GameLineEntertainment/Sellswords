using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class CharacterPanelView : UIView
{
    public GameObject CharacterButton;

    protected override void Awake()
    {
        base.Awake();
        removePosition = new Vector2(thisPosition.anchoredPosition.x, thisPosition.anchoredPosition.y);
        callPosition = new Vector2(-thisPosition.anchoredPosition.x, thisPosition.anchoredPosition.y);
    }
}

