using System;
using UnityEngine;
using UnityEngine.UI;

public class FPS_Display : MonoBehaviour
{
    public Text txt;
    double fps;

    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("ShowFPS", 0.5f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        fps = 1.0f / Time.deltaTime;       

    }

    void ShowFPS()
    {
        txt.text = "FPS: " + fps;

        if (fps > 25)
            txt.color = Color.green;

        if (fps < 25)
            txt.color = Color.yellow;

        if (fps < 15)
            txt.color = Color.red;
    }
}
