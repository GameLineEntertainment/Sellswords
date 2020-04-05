using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIController : MonoBehaviour
{
    private SkillPanelView _skillPanel;
    private SkillInfoPanelView _skillInfoPanel;
    private void Awake()
    {
        _skillPanel = FindObjectOfType<SkillPanelView>();
        _skillInfoPanel = FindObjectOfType<SkillInfoPanelView>();
    }
}

