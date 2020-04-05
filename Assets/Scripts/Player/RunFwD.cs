using UnityEngine;
using System.Collections;

public class RunFwD : MonoBehaviour 
{
    public bool run = false;

    [HideInInspector]
    public GameObject Left, Right, Down;

	// Use this for initialization
	void Start () 
    {
        run = false;
	}
	
	// Update is called once per frame
	void Update () 
    {        
        Check();
	}

    void Check()
    {
        Left = GameObject.FindGameObjectWithTag("Left_Enemy");
        Right = GameObject.FindGameObjectWithTag("Right_Enemy");
        Down = GameObject.FindGameObjectWithTag("Down_Enemy");

        if (Left == null && Right == null && Down == null)
        {
            run = true;
        }

        else
            run = false;

        if(run)
        transform.position += new Vector3(0, 0, 3 * Time.deltaTime);
    }
}
