using UnityEngine;
using System.Collections;

public class FGmaeMenu : MonoBehaviour 
{

    public GameObject Menu, Settings, TopBar, Changelvl;

	public bool buttonchangetop = false;
    private bool ВладГГ = false;

    void Start()
    {
    }


    public void StartGame(string Level)
    {
        Application.LoadLevel(Level);
    }

	public void bLoadGame() 
    {
		//Application.LoadLevel("LoadLvl");        
        Menu.SetActive(ВладГГ);
        ВладГГ = !ВладГГ;
        Changelvl.SetActive(ВладГГ);

		Debug.Log("Старт уровня");
	}

	public void bExitGame() 
    {
		Application.Quit();
	}

	public void bAboutGame() 
    {
		Debug.Log("Скроллер которого нет");
	}

	public void SettingGame(bool xz) 
    {

		if (xz)
		{
		Menu.SetActive (false);
		Settings.SetActive (true);
		}
        
		Debug.Log("Поменял");
	}

	public void SettingGameBack(bool xyz) 
    {
		
		if (xyz)
		{
			Menu.SetActive (true);
			Settings.SetActive (false);
		}

        if (!xyz)
        {
            Menu.SetActive(true);
            Changelvl.SetActive(false);
        }
        ///Debug.Log("Поменял");Changelvl
	}

	public void bSoundGame() 
    {
		Debug.Log("Звуки которых нет");

	}

	public void TopMenuBar() 
    {
		buttonchangetop = !buttonchangetop;
		 TopBar.SetActive (buttonchangetop);
}
}
