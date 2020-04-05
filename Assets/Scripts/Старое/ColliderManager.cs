using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderManager : MonoBehaviour
{

    public bool Enable;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Enable)
        {
            Collider[] Cols = GetComponentsInChildren<Collider>();
            Rigidbody[] RG = GetComponentsInChildren<Rigidbody>();
            FixedJoint[] FJ = GetComponentsInChildren<FixedJoint>();

            for (int i = 0; i < Cols.Length; i++)
            {
                Cols[i].enabled = true;
                Cols[i].contactOffset = 0.5f;

                RG[i].mass = 3;
                RG[i].interpolation = RigidbodyInterpolation.Interpolate;
            }

            for (int i = 0; i < FJ.Length; i++)
            {
                FJ[i].enablePreprocessing = false;
            }

            Enable = false;
        }
	}
}
