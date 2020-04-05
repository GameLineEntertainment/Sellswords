using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OldSellswords;

public sealed class CharacterInfoPanelView : UIView
{
    public Image Portrait;
    public Text Level;
    public Text Name, Info;
    public Button UpgrateButton;
    private CharacterContainer _character;

        public void Fill()
    {
        _character = Main.Instance.CharacterManager.CurrentCharacter;
        Name.text = _character.Name;
        Info.text = _character.InfoChar;
        Level.text = _character.Level.ToString();

        Portrait.sprite = _character.Portrait;
        if (_character.Level == 0)
        {
            UpgrateButton.gameObject.GetComponentInChildren<Text>().text = "Buy";
        }
        else
        {
            UpgrateButton.gameObject.GetComponentInChildren<Text>().text = "Upgrade";
        }
    }
}
