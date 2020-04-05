using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Model;
    public GameObject Instance;
    public Transform StartPosition;
    public float RotationSpeed;
    public float ReturnSpeed;
    public float DeltaAngle;


    private void Awake()
    {
        Instance = this.gameObject;
    }
}

