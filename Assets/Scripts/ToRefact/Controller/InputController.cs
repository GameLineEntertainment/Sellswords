using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InputController : MonoBehaviour
{
    private Touch myTouch;
    private List<RaycastResult> results = new List<RaycastResult>();
    private PointerEventData pointerData = new PointerEventData(EventSystem.current);
    private bool activeLocation;

    private LocationPanelView _locationPanel;
    private InfoPanelView _infoPanel;
    private PartyPanelView _partyPanel;
    private CharacterPanelView _characterPanel;
    private HeadPanelView _headPanel;

    private void Awake()
    {
        _locationPanel = FindObjectOfType<LocationPanelView>();
        _infoPanel = FindObjectOfType<InfoPanelView>();
        _characterPanel = FindObjectOfType<CharacterPanelView>();
        _headPanel = FindObjectOfType<HeadPanelView>();
        _partyPanel = FindObjectOfType<PartyPanelView>();
    }

    private void Update()
    {
        if (!_infoPanel.gameObject.activeSelf)
        {

            if (Input.touchCount == 1)
            {
                myTouch = Input.GetTouch(0);
                activeLocation = false;
                pointerData.position = myTouch.position;
                EventSystem.current.RaycastAll(pointerData, results);
                foreach (var result in results)
                {
                    if (result.gameObject.GetComponent<Button>())
                    {
                        activeLocation = true;
                    }
                }
                if (!activeLocation)
                {
                    _locationPanel.Remove();
                    _characterPanel.Remove();
                    _headPanel.Call();
                    _partyPanel.Call();
                }
            }
        }
    }
}

