using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public sealed class MapView : MonoBehaviour
{
    public List<GameObject> ButtonLocation;

    private void Awake()
    {
        var button = GetComponentsInChildren<btnLocation>();
        foreach(var btn in button)
        {
            btn.gameObject.name = btn.Location.Name;
            ButtonLocation.Add(btn.gameObject);
        }
    }
}

