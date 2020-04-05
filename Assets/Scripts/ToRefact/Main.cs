using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Main : MonoBehaviour
{
    public static Main Instance;
    public CharacterManager CharacterManager;
    public GameObject MapMenu, CharacterMenu, UpgradeMenu, CharacterCamera, MainMenu, MainCamera;
    private GameObject _controllerGameObject;

    private void Awake()
    {
        Instance = this;
        CharacterManager = FindObjectOfType<CharacterManager>();
        _controllerGameObject = new GameObject { name = "Controllers" };
        _controllerGameObject.AddComponent<SpawnerController>();
        _controllerGameObject.AddComponent<CharacterUIController>();
        _controllerGameObject.AddComponent<InputController>();
        _controllerGameObject.AddComponent<UpgradeUIController>();
        _controllerGameObject.AddComponent<MapUIController>();

    }

    private void Start()
    {
        MapMenu.SetActive(false);
        CharacterMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        CharacterCamera.SetActive(false);
    }

    public void MapMenuClick()
    {
        MainMenu.SetActive(false);
        MapMenu.SetActive(true);
        CharacterMenu.SetActive(true);
        UpgradeMenu.SetActive(true);
        _controllerGameObject.GetComponent<CharacterUIController>().OnMapMenu();
    }

    public void CharacterMenuClick()
    {
        MainMenu.SetActive(false);
        MainCamera.SetActive(false);
        CharacterMenu.SetActive(true);
        UpgradeMenu.SetActive(true);
        CharacterCamera.SetActive(true);
        _controllerGameObject.GetComponent<CharacterUIController>().OnCharacterMenu();
    }
}

