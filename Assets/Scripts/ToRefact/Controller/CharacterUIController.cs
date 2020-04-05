using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using OldSellswords;

public class CharacterUIController : MonoBehaviour
{
    private CharacterManager _characterManager;
    private Spawner _spawner;
    private GameObject _model;
    private HeadPanelView _headPanel;
    private PartyPanelView _partyPanel;
    private CharacterPanelView _characterPanel;
    private CharacterInfoPanelView _characterInfoPanel;
    private SkillPanelView _skillPanel;
    private SkillInfoPanelView _skillInfoPanel;
    private CharacterCamera _camera;

    private GameObject _characterPanelContent;
    private GameObject _characterButton;
    private Button _redButton, _greenButton, _blueButton, _actionButton;

    private bool _upgradeIsOpen = false;

    public void Awake()
    {
        _characterManager = Main.Instance.CharacterManager;
        _spawner = FindObjectOfType<Spawner>();
        _camera = FindObjectOfType<CharacterCamera>();
        _headPanel = FindObjectOfType<HeadPanelView>();
        _headPanel.BackButton.onClick.AddListener(Back);
        _partyPanel = FindObjectOfType<PartyPanelView>();
        _characterPanel = FindObjectOfType<CharacterPanelView>();
        _characterInfoPanel = FindObjectOfType<CharacterInfoPanelView>();
        _characterInfoPanel.UpgrateButton.onClick.AddListener(Upgrade);
        _skillPanel = FindObjectOfType<SkillPanelView>();
        _skillInfoPanel = FindObjectOfType<SkillInfoPanelView>();
        _characterPanelContent = _characterPanel.GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        _characterButton = _characterPanel.CharacterButton;
        _redButton = _partyPanel.RedCharacterButton.GetComponent<Button>();
        _redButton.onClick.AddListener(delegate { SelectGroup(ColorGroup.Red); });
        _greenButton = _partyPanel.GreenCharacterButton.GetComponent<Button>();
        _greenButton.onClick.AddListener(delegate { SelectGroup(ColorGroup.Green); });
        _blueButton = _partyPanel.BlueCharacterButton.GetComponent<Button>();
        _blueButton.onClick.AddListener(delegate { SelectGroup(ColorGroup.Blue); });
        _actionButton = _partyPanel.ActionButton.GetComponent<Button>();
        _actionButton.onClick.AddListener(Action);
    }

    private void Start()
    {
        for (int i = (_characterManager.CharacterParty.Length)-1; i >=0; i--)
        {
            SelectCharacter(_characterManager.CharacterParty[i]);
        }
    }

    public void SelectGroup(ColorGroup color)
    {
        for (int i = 0; i < _characterPanelContent.transform.childCount; i++)
        {
            Destroy(_characterPanelContent.transform.GetChild(i).gameObject);
        }
        List<CharacterContainer> currentGroup = new List<CharacterContainer>();
        switch (color)
        {
            case ColorGroup.Red:
                currentGroup = Main.Instance.CharacterManager.RedGroup;
                break;
            case ColorGroup.Green:
                currentGroup = Main.Instance.CharacterManager.GreenGroup;
                break;
            case ColorGroup.Blue:
                currentGroup = Main.Instance.CharacterManager.BlueGroup;
                break;
            default:
                break;
        }
        for (int i = 0; i < currentGroup.Count; i++)
        {
            if ((Main.Instance.MapMenu.activeSelf) && (currentGroup[i].Level == 0)) continue;
            var btn = Instantiate(_characterButton, _characterPanelContent.transform);
            var currentCharacter = currentGroup[i];
            btn.GetComponent<CharacterButton>().Character = currentCharacter;
            btn.GetComponent<Button>().onClick.AddListener(delegate { SelectCharacter(currentCharacter); });
        }
        _characterPanelContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)(currentGroup.Count * (_characterButton.GetComponent<RectTransform>().rect.height + _characterPanelContent.GetComponent<VerticalLayoutGroup>().spacing - 1) + _characterPanelContent.GetComponent<VerticalLayoutGroup>().padding.bottom + _characterPanelContent.GetComponent<VerticalLayoutGroup>().padding.top));
        _characterPanel.Call();
    }

    public void SelectCharacter(CharacterContainer character)
    {
        StartCoroutine(CharSelection(character));

        /*
        _characterManager.CurrentCharacter = character;
        _characterInfoPanel.Fill();
        if (character.Level > 0)
        {
            _characterManager.CharacterParty[(int)character.Color] = character;
            switch (character.Color)
            {
                case ColorGroup.Red:
                    _redButton.gameObject.GetComponentInChildren<Text>().text = character.Name;
                    _redButton.gameObject.GetComponent<Image>().sprite = character.Icon;
                    break;
                case ColorGroup.Green:
                    _greenButton.gameObject.GetComponentInChildren<Text>().text = character.Name;
                    _greenButton.gameObject.GetComponent<Image>().sprite = character.Icon;

                    break;
                case ColorGroup.Blue:
                    _blueButton.gameObject.GetComponentInChildren<Text>().text = character.Name;
                    _blueButton.gameObject.GetComponent<Image>().sprite = character.Icon;

                    break;
                default:
                    break;
            }
            if (Main.Instance.MapMenu.activeSelf)
            {
                _characterPanel.Remove();
            }
        }
        if (_model != null)
        {
            _model.SetActive(false);
        }
        foreach(var model in _spawner.Model)
        {
            if (model.name == character.Name)
            {
                _model = model;
                _model.SetActive(true);
            }
        }

        _characterManager.Save();
        */
    }

    IEnumerator CharSelection(CharacterContainer character)
    {
        yield return null;

        _characterManager.CurrentCharacter = character;
        _characterInfoPanel.Fill();
        if (character.Level > 0)
        {
            _characterManager.CharacterParty[(int)character.Color] = character;
            switch (character.Color)
            {
                case ColorGroup.Red:
                    _redButton.gameObject.GetComponentInChildren<Text>().text = character.Name;
                    _redButton.gameObject.GetComponent<Image>().sprite = character.Icon;
                    break;
                case ColorGroup.Green:
                    _greenButton.gameObject.GetComponentInChildren<Text>().text = character.Name;
                    _greenButton.gameObject.GetComponent<Image>().sprite = character.Icon;

                    break;
                case ColorGroup.Blue:
                    _blueButton.gameObject.GetComponentInChildren<Text>().text = character.Name;
                    _blueButton.gameObject.GetComponent<Image>().sprite = character.Icon;

                    break;
                default:
                    break;
            }
            if (Main.Instance.MapMenu.activeSelf)
            {
                _characterPanel.Remove();
            }
        }
        if (_model != null)
        {
            _model.SetActive(false);
        }
        foreach (var model in _spawner.Model)
        {
            if (model.name == character.Name)
            {
                _model = model;
                _model.SetActive(true);
            }
        }

        _characterManager.Save();
    }

    public void Action()
    {
        if (Main.Instance.MapMenu.activeSelf)
        {
            _headPanel.HeadText.text = "Select Characters";
            _actionButton.gameObject.GetComponentInChildren<Text>().text = "To Map";
            _characterPanel.Remove();
            _characterInfoPanel.gameObject.SetActive(true);
            Main.Instance.MapMenu.SetActive(false);
            Main.Instance.MainCamera.SetActive(false);
            Main.Instance.CharacterCamera.SetActive(true);
        }
        else
        {
            _headPanel.HeadText.text = "Select Adventure";
            _actionButton.gameObject.GetComponentInChildren<Text>().text = "Change Party";
            _characterPanel.Remove();
            _characterInfoPanel.gameObject.SetActive(false);
            Main.Instance.MapMenu.SetActive(true);
            Main.Instance.MainCamera.SetActive(true);
            Main.Instance.CharacterCamera.SetActive(false);

        }
    }

    public void Upgrade()
    {
        if (!_upgradeIsOpen)
        {
            _characterInfoPanel.UpgrateButton.gameObject.GetComponentInChildren<Text>().text = "Level Up";
            _headPanel.HeadText.text = "Upgrade Character";
            _skillPanel.Fill();
            _skillPanel.Call();
            _partyPanel.Remove();
            _characterPanel.Remove();
            _camera.UpgradePos();
            _upgradeIsOpen = true;
        }
        else
        {
            Main.Instance.CharacterManager.CurrentCharacter.Level++;
            Main.Instance.CharacterManager.CurrentCharacter.SkillPoints++;
            for (int i = 0; i < Main.Instance.CharacterManager.CharacterPool.Length; i++)
            {
                if (Main.Instance.CharacterManager.CharacterPool[i].Name == Main.Instance.CharacterManager.CurrentCharacter.Name)
                {
                    Main.Instance.CharacterManager.CharacterPool[i] = Main.Instance.CharacterManager.CurrentCharacter;
                    break;
                }
            }
            switch (Main.Instance.CharacterManager.CurrentCharacter.Color)
            {
                case (ColorGroup.Red):
            for (int i = 0; i < Main.Instance.CharacterManager.RedGroup.Count; i++)
            {
                if (Main.Instance.CharacterManager.RedGroup[i].Name == Main.Instance.CharacterManager.CurrentCharacter.Name)
                {
                    Main.Instance.CharacterManager.RedGroup[i] = Main.Instance.CharacterManager.CurrentCharacter;
                    break;
                }
            }
                    break;
                case (ColorGroup.Green):
                    for (int i = 0; i < Main.Instance.CharacterManager.GreenGroup.Count; i++)
            {
                if (Main.Instance.CharacterManager.GreenGroup[i].Name == Main.Instance.CharacterManager.CurrentCharacter.Name)
                {
                    Main.Instance.CharacterManager.GreenGroup[i] = Main.Instance.CharacterManager.CurrentCharacter;
                    break;
                }
            }
                    break;
                case (ColorGroup.Blue):
                    for (int i = 0; i < Main.Instance.CharacterManager.BlueGroup.Count; i++)
            {
                if (Main.Instance.CharacterManager.BlueGroup[i].Name == Main.Instance.CharacterManager.CurrentCharacter.Name)
                {
                    Main.Instance.CharacterManager.BlueGroup[i] = Main.Instance.CharacterManager.CurrentCharacter;
                    break;
                }
            }
                    break;
            default:
            break;
        }
            _characterInfoPanel.Fill();
            _skillPanel.Fill();
        }

        _characterManager.Save();
    }

    public void Back()
    {
        if (Main.Instance.MapMenu.activeSelf)
        {
            Main.Instance.MainMenu.SetActive(true);
            Main.Instance.MapMenu.SetActive(false);
            Main.Instance.UpgradeMenu.SetActive(false);
            Main.Instance.CharacterMenu.SetActive(false);
        }
        else
        {
            if (_upgradeIsOpen)
            {
                _headPanel.HeadText.text = "Select Characters";
                _partyPanel.Call();
                _skillPanel.Remove();
                _skillInfoPanel.Remove();
                _characterInfoPanel.UpgrateButton.gameObject.GetComponentInChildren<Text>().text = "Upgrade";
                _camera.CharacterPos();
                _upgradeIsOpen = false;
            }
            else
            {
                Main.Instance.MainMenu.SetActive(true);
                Main.Instance.UpgradeMenu.SetActive(false);
                Main.Instance.CharacterMenu.SetActive(false);
                Main.Instance.CharacterCamera.SetActive(false);
                Main.Instance.MainCamera.SetActive(true);
            }
        }
    }

    public void OnMapMenu()
    {
        _partyPanel.ActionButton.GetComponentInChildren<Text>().text = "Change Party";
        _characterInfoPanel.gameObject.SetActive(false);
    }

    public void OnCharacterMenu()
    {
        _partyPanel.ActionButton.GetComponentInChildren<Text>().text = "To Map";
        _characterInfoPanel.gameObject.SetActive(true);
    }
}

