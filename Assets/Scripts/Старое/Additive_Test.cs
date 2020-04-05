using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class Additive_Test : MonoBehaviour
{
    public static UnityEvent Loaded;

    void Start()
    {
        if (Loaded == null)
            Loaded = new UnityEvent();

        Additive_Test.Loaded.AddListener(Ping);

        Loaded.Invoke();
    }

    void Update()
    {
        if (Input.anyKeyDown && Loaded != null)
        {
            Loaded.Invoke();
        }
    }

    void Ping()
    {
        Debug.Log("Was Craped");
    }
}
