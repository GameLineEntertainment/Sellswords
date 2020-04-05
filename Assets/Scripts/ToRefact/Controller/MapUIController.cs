using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapUIController : MonoBehaviour
{
    private MapView _map;
    private LocationPanelView _locationPanel;
    private InfoPanelView _infoPanel;
    private PartyPanelView _partyPanel;
    private CharacterPanelView _characterPanel;
    private HeadPanelView _headPanel;

    private Button[] _location;

    private void Awake()
    {
        _map = FindObjectOfType<MapView>();
        _locationPanel = FindObjectOfType<LocationPanelView>();
        _infoPanel = FindObjectOfType<InfoPanelView>();
        _characterPanel = FindObjectOfType<CharacterPanelView>();
        _headPanel = FindObjectOfType<HeadPanelView>();
        _partyPanel = FindObjectOfType<PartyPanelView>();
        _infoPanel.ButtonClose.onClick.AddListener(CloseInfo);
        foreach (var button in _map.ButtonLocation)
        {
            button.GetComponent<Button>().onClick.AddListener(delegate { SelectLocation(button.GetComponent<btnLocation>().Location); });
        }
        _infoPanel.gameObject.SetActive(false);

    }

    public void SelectLocation(LocationContainer location)
    {
        _locationPanel.NameLocation.text = location.Name;
        _locationPanel.InfoLocation.text = location.ShortInfo;
        _locationPanel.Call();
        _partyPanel.Remove();
        _characterPanel.Remove();
        _locationPanel.ButtonInfo.onClick.AddListener(delegate { ShowInfo(location); });
        _locationPanel.ButtonStart.onClick.AddListener(delegate { LoadLocation(location); });

    }

    private void ShowInfo(LocationContainer location)
    {
        _infoPanel.NameText.text = location.Name;
        _infoPanel.InfoText.text = location.Info;
        _locationPanel.Remove();
        _headPanel.Remove();
        _infoPanel.gameObject.SetActive(true);
    }

    private void CloseInfo()
    {
        _infoPanel.gameObject.SetActive(false);
        _headPanel.Call();
        _locationPanel.Remove();
        _partyPanel.Call();
    }

    private void LoadLocation(LocationContainer location)
    {
        Main.Instance.MapMenu.SetActive(false);
        Main.Instance.CharacterMenu.SetActive(false);
        Main.Instance.UpgradeMenu.SetActive(false);
        Main.Instance.MainMenu.SetActive(true);
        SceneManager.LoadScene(location.SceneNumber);
    }
}

