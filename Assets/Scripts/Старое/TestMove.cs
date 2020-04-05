using UnityEngine;
using System.Collections;


public class TestMove : MonoBehaviour 
{
    public static bool stop;

    public Transform target;		    //цель
    public float moveSpeed;			    //скорость перемещения
    public int rotationSpeed;		    //скорость поворота
    public float maxDistance;			//максимальное приближение    
    public bool Move = true;
    public bool IsCube;                 //Переключение между гиви и Кубами

    private Animator anim;
    private Transform MyTransform; 	    //временная переменная
    public float NullSpeed;

	// Use this for initialization
	void Start () 
    {
        target = GameObject.Find("Target").transform;
        MyTransform = transform;

        if(!IsCube)
            anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        FollowToChar();
	}

    void FollowToChar() // Движение
    {
        if (IsCube)
        {
            //чертим вспомогательнгую линию от нас к игроку
            //(видима в окне редактора только)

            Debug.DrawLine(target.position,
                            MyTransform.position,
                            Color.yellow);

            //поворачиваемся в сторону игрока (цели)
            MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation,
                                    Quaternion.LookRotation(target.position - MyTransform.position),
                                    rotationSpeed * Time.deltaTime);

            //если дистанция позволяет, можем двигаться к цели
            if (Vector3.Distance(target.position, MyTransform.position) >= maxDistance)
            {
                //двигаемся к цели 
                MyTransform.position += MyTransform.forward * moveSpeed * Time.deltaTime;

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(IsCube)
        moveSpeed = 0;

        if (!IsCube)
        {
            stop = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsCube)
            moveSpeed = 2;

        if (!IsCube)
        {
            stop = false;
        }
    }
}
