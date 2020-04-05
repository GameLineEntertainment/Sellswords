using UnityEngine;
using System.Collections;

public class ButtonChange : MonoBehaviour 
{
    public static bool Turn = false;
    public static bool Attack = false;

    public Texture2D icon;              //переменная для иконки
    public bool IsLeft;
    public GameObject Warrior;
    public GameObject Wizzard;
    public GameObject Arc;

    private Char_Controller Pos;
    public GameObject[] Characters;

	// Use this for initialization
    void Start()
    {       
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.Space))
            Attack = true;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Left();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Right();
	}

    void OnLevelWasLoaded()
    {
        Warrior = GameObject.Find("Char2(Clone)");
        Wizzard = GameObject.Find("Char1(Clone)");
        Arc = GameObject.Find("Char0(Clone)");

        Characters = GameObject.FindGameObjectsWithTag("Char");
    }

    public void Left()
    {
        foreach (GameObject Char in Characters)
        {
            Pos = (Char_Controller)Char.GetComponent("Char_Controller");
            Pos.TargetPositions(true);
            Turn = true;

            if (GameOver.GamePaused == true)
                Time.timeScale = 1;
        }
    }

    public void Right()
    {
        foreach (GameObject Char in Characters)
        {
            Pos = (Char_Controller)Char.GetComponent("Char_Controller");
            Pos.TargetPositions(false);
            Turn = true;

            if (GameOver.GamePaused == true)
                Time.timeScale = 1;
        }
    }

    IEnumerator Unturn()
    {
        yield return new WaitForSeconds(0.2f);
        Turn = true;
    }


        /*
    void OnGUI()
    {
        if (IsLeft)
            if (GUI.Button(new Rect(100, Screen.height / 2 - 50, 100, 150), new GUIContent(icon)))
            {                
                foreach (GameObject Char in Characters)
                {
                    Pos = (Char_Controller)Char.GetComponent("Char_Controller");
                    Pos.TargetPositions(false);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Time.timeScale = 1;
                }
            }
        
        if (!IsLeft)
            if (GUI.Button(new Rect(Screen.width - 200, Screen.height / 2 - 50, 100, 150), new GUIContent(icon)))
            {
                foreach (GameObject Char in Characters)
                {
                    Pos = (Char_Controller)Char.GetComponent("Char_Controller");
                    Pos.TargetPositions(true);
                }
            }*/


      /*  if (GUI.Button(new Rect(0, 0, 100, 50), new GUIContent(icon)))
            Is_Sister_Active = !Is_Sister_Active;*/
    //}
}
