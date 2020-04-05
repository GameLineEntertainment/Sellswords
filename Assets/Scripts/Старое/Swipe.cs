using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Loot
{
    public string Name;
    public GameObject LootObj;
    public int SellCost, BuyCost, Rank, Count;
    public float Chance;

}

public class Swipe : MonoBehaviour
{ 
    public Loot[] MyLoot;

    public enum SwipeDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    /*
   // public static event Action<SwipeDirection> Swiper;
    private bool swiping = false;
    private bool eventSent = false;
    private Vector2 lastPosition;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0)
        {
            if (swiping == false)
            {
                swiping = true;
                lastPosition = Input.GetTouch(0).position;
                return;
            }
            else
            {
                if (!eventSent)
                {
                    if (Swiper != null)
                    {
                        Vector2 direction = Input.GetTouch(0).position - lastPosition;

                        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                        {
                            if (direction.x > 0)
                                Swiper(SwipeDirection.Right);
                            else
                                Swiper(SwipeDirection.Left);
                        }
                        else
                        {
                            if (direction.y > 0)
                                Swiper(SwipeDirection.Up);
                            else
                                Swiper(SwipeDirection.Down);
                        }

                        eventSent = true;
                    }
                }
            }
        }
        else
        {
            swiping = false;
            eventSent = false;
        }
    }
    */
}
