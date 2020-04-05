using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SkillInfoPanelView : UIView
{
    public Image SkillImage;
    public Text Name, Info;
    public Button BuyButton;

    protected override void Awake()
    {
        base.Awake();
        callPosition = new Vector2(thisPosition.anchoredPosition.x, -thisPosition.anchoredPosition.y);
        removePosition = new Vector2(thisPosition.anchoredPosition.x, thisPosition.anchoredPosition.y);
    }
}
