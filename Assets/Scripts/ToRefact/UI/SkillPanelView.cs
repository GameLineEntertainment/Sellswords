using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OldSellswords;

public sealed class SkillPanelView : UIView
{
    public Text SkillPointsText;
    public Button[] SkillButton = new Button[6];
    private CharacterContainer _character;
    private SkillInfoPanelView _skillInfoPanel;

    protected override void Awake()
    {
        base.Awake();
        _skillInfoPanel = FindObjectOfType<SkillInfoPanelView>();
        callPosition = new Vector2(-thisPosition.anchoredPosition.x, thisPosition.anchoredPosition.y);
        removePosition = new Vector2(thisPosition.anchoredPosition.x, thisPosition.anchoredPosition.y);
    }

    public void Fill()
    {
        _character = Main.Instance.CharacterManager.CurrentCharacter;
        SkillPointsText.text = string.Format("SkillPoints: {0}", _character.SkillPoints);
        for (int i = 0; i < 6; i++)
        {
            SkillButton[i].gameObject.GetComponent<SkillButton>().Skill = _character.Skill[i];
            var index = i;
            SkillButton[i].onClick.AddListener(delegate { SelectSkill(index); });
            if (_character.Skill[i].isLock)
            {
                SkillButton[i].gameObject.GetComponent<SkillButton>().Lock.SetActive(true);
            }
            else
            {
                SkillButton[i].gameObject.GetComponent<SkillButton>().Lock.SetActive(false);
            }
        }
    }

    private void SelectSkill(int index)
    {
        var skill = Main.Instance.CharacterManager.CurrentCharacter.Skill[index];
        _skillInfoPanel.Call();
        _skillInfoPanel.SkillImage.sprite = skill.Icon;
        _skillInfoPanel.Name.text = skill.Name;
        _skillInfoPanel.Info.text = skill.Info;
        if (skill.isLock)
        {
            _skillInfoPanel.BuyButton.gameObject.SetActive(true);
            _skillInfoPanel.BuyButton.onClick.AddListener(delegate { Buy(index); });
        }
        else
        {
            _skillInfoPanel.BuyButton.gameObject.SetActive(false);
        }
    }

    private void Buy(int index)
    {
        var currentCharacter = Main.Instance.CharacterManager.CurrentCharacter;
        if ((currentCharacter.Skill[index].isLock)&&(currentCharacter.SkillPoints>=currentCharacter.Skill[index].Price))
        {
            Main.Instance.CharacterManager.CurrentCharacter.Skill[index].isLock = false;
            Main.Instance.CharacterManager.CurrentCharacter.SkillPoints -= currentCharacter.Skill[index].Price;
            for (int i=0;i<Main.Instance.CharacterManager.CharacterPool.Length;i++)
            {
                if (Main.Instance.CharacterManager.CharacterPool[i].Name == Main.Instance.CharacterManager.CurrentCharacter.Name)
                {
                    Main.Instance.CharacterManager.CharacterPool[i].Skill[index].isLock = false;
                    Main.Instance.CharacterManager.CharacterPool[i].SkillPoints = Main.Instance.CharacterManager.CurrentCharacter.SkillPoints;
                    break;
                }
            }
            Fill();
        }
    }

}
