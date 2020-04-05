using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool IsLeft;
    public GameLevel Script;

    public bool MouseHere = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && MouseHere && IsLeft)
        //{
        //    Script.Left();
        //}

        //else if (Input.GetMouseButtonDown(0) && MouseHere && !IsLeft)
        //{
        //    Script.Right();
        //}



    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseHere = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseHere = false;
    }    
}
