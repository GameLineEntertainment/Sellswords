using UnityEngine;
using System.Collections;

public class ArtificalReset : MonoBehaviour 
{
    public bool HardReset = false;

    public Variables Vars;

	// Use this for initialization
	void Start () 
    {
      //  if (ResetAll)
        //    Reset();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (HardReset)
            Vars.Reset();

	}

    
}
