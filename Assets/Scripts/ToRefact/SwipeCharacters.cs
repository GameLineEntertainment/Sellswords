using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCharacters : MonoBehaviour {
    private Touch myTouch;
    private Touch startPosition;
    private Touch finalPosition;
    private bool touchCheck;
    public float DeadZone; //ставьте 100
    private float deltaTouch;
    private float signTouch;
    private float stayCount;
    public float distanceTouch; //ставьте300
    
    public SwipeType swipeType;

    // Use this for initialization
    void Start ()
    {
        deltaTouch = 0;
        signTouch = 0;
        touchCheck = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //считает Свайп только после отпускания пальца с экрана
        if (swipeType == SwipeType.one)
        {
            if (Input.touchCount == 1) //Проверяю чтобы было только одно касание
            {
                myTouch = Input.GetTouch(0); //Беру это касание
                if (!touchCheck) //Проверяю были ли касания в предыдущем кадре
                {
                    startPosition = myTouch; //Если касаний не было, то записываю это касание как начало свайпа
                }
                touchCheck = true; //Меняю метку о том, что касание в кадре было
            }
            else //Если не было одного касания по экрану
            {
                if (touchCheck) //но при этом в предыдущем кадре касание было
                {

                    finalPosition = myTouch; //значит предыдущее касание было завершающим
                    if (startPosition.position != finalPosition.position) //убеждаюсь что движение было
                    {
                        float dX = finalPosition.position.x - startPosition.position.x; //рассчитываю расстояние свайпа по горизонтали
                        float dY = finalPosition.position.y - startPosition.position.y; //рассчитываю расстояние свайпа по вертикали
                        if (Mathf.Abs(dX) > Mathf.Abs(dY)) //проверяю, чтобы это был горизонтальный свайп 
                        {
                            if (dX > 0)
                            {
                                //GetComponent<GameLevel>().Right();
                            }
                            else
                            {
                                //GetComponent<GameLevel>().Left();
                            }
                        }
                    }
                }
                touchCheck = false; //по завершению свайпа делаю пометку о том что касание закончилось
            }

        }
        //считает свайп после окончания движения пальца в одном направлении
        if (swipeType == SwipeType.two)
        {
            if (Input.touchCount == 1) 
            {
                myTouch = Input.GetTouch(0);
                if((myTouch.phase==TouchPhase.Moved)&&(Mathf.Sign(myTouch.deltaPosition.x)==signTouch) && (myTouch.deltaPosition.x != 0))
                {
                    deltaTouch += myTouch.deltaPosition.x;
                    signTouch = Mathf.Sign(deltaTouch);
                    stayCount = 0;
                }
                else if (myTouch.phase != TouchPhase.Moved)
                {
                    stayCount++;
                }
                var c1 = (Mathf.Sign(myTouch.deltaPosition.x) != signTouch) && (myTouch.deltaPosition.x != 0);
                var c2 = (stayCount>=2);
                if (c1||c2)
                {
                    if (Mathf.Abs(deltaTouch) > DeadZone)
                    {
                        if (signTouch > 0)
                        {
                            //GetComponent<GameLevel>().Right();
                        }
                        else
                        {
                            //GetComponent<GameLevel>().Left();
                        }
                    }
                    deltaTouch = 0;
                    signTouch = Mathf.Sign(myTouch.deltaPosition.x);
                }
            }
            else
            {
                if (Mathf.Abs(deltaTouch) > DeadZone)
                {
                    if (signTouch > 0)
                    {
                        //GetComponent<GameLevel>().Right();
                    }
                    else
                    {
                        //GetComponent<GameLevel>().Left();
                    }
                }
                deltaTouch = 0;
                signTouch = Mathf.Sign(myTouch.deltaPosition.x);
            }
        }
        //считает свайп после преододения мертвой зоны в одном направлении
        if (swipeType == SwipeType.three)
        {
            if (Input.touchCount == 1)
            {
                myTouch = Input.GetTouch(0);
                if ((myTouch.phase == TouchPhase.Moved) && (Mathf.Sign(myTouch.deltaPosition.x) == signTouch) && (myTouch.deltaPosition.x != 0))
                {
                    deltaTouch += myTouch.deltaPosition.x;
                    signTouch = Mathf.Sign(deltaTouch);
                    stayCount = 0;
                    if ((Mathf.Abs(deltaTouch) > DeadZone)&&(!touchCheck))
                    {
                        if (signTouch > 0)
                        {
                            //GetComponent<GameLevel>().Right();
                        }
                        else
                        {
                            //GetComponent<GameLevel>().Left();
                        }
                        touchCheck = true;
                    }
                }
                else if (myTouch.phase != TouchPhase.Moved)
                {
                    stayCount++;
                }
                var c1 = (Mathf.Sign(myTouch.deltaPosition.x) != signTouch) && (myTouch.deltaPosition.x != 0);
                var c2 = (stayCount >= 2);
                if (c1 || c2)
                {
                    touchCheck = false;
                    deltaTouch = 0;
                    signTouch = Mathf.Sign(myTouch.deltaPosition.x);
                }
            }
            else
            {
                touchCheck = false;
                deltaTouch = 0;
                signTouch = Mathf.Sign(myTouch.deltaPosition.x);
            }
        }
        //считает несколько свайпов в одном направлении, если длина свайпа большая 
        if (swipeType == SwipeType.four)
        {
            if (Input.touchCount == 1)
            {
                myTouch = Input.GetTouch(0);
                if ((myTouch.phase == TouchPhase.Moved) && (Mathf.Sign(myTouch.deltaPosition.x) == signTouch) && (myTouch.deltaPosition.x != 0))
                {
                    deltaTouch += myTouch.deltaPosition.x;
                    signTouch = Mathf.Sign(deltaTouch);
                    stayCount = 0;
                    if ((Mathf.Abs(deltaTouch) > DeadZone) && (!touchCheck))
                    {
                        if (signTouch > 0)
                        {
                            //GetComponent<GameLevel>().Right();
                        }
                        else
                        {
                            //GetComponent<GameLevel>().Left();
                        }
                        touchCheck = true;
                    }
                    if (Mathf.Abs(deltaTouch) > distanceTouch)
                    {
                        deltaTouch = 0;
                        touchCheck = false;
                    }
                }
                else if (myTouch.phase != TouchPhase.Moved)
                {
                    stayCount++;
                }
                var c1 = (Mathf.Sign(myTouch.deltaPosition.x) != signTouch) && (myTouch.deltaPosition.x != 0);
                var c2 = (stayCount >= 2);
                if (c1 || c2)
                {
                    touchCheck = false;
                    deltaTouch = 0;
                    signTouch = Mathf.Sign(myTouch.deltaPosition.x);
                }
            }
            else
            {
                touchCheck = false;
                deltaTouch = 0;
                signTouch = Mathf.Sign(myTouch.deltaPosition.x);
            }
        }
    }
}

public enum SwipeType
{
    one,
    two,
    three,
    four
}
