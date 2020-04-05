using UnityEngine;
using System.Collections;

public class GameStopper : MonoBehaviour 
{
    public float StartTime = 0, EndTime = 0, Время;
    bool stop;

	// Use this for initialization
	void Start () 
    {
       StartCoroutine(TimeCheck());
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (stop == true && Input.anyKey)
            Time.timeScale = 1;

        Время = Time.time;
	}

    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(StartTime);
        yield return new WaitForSeconds(EndTime);
        Time.timeScale = 0;
        stop = true;
    }
}
