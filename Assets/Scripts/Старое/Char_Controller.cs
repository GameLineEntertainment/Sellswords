using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Char_Controller : MonoBehaviour
{
    #region Базовые функции

    private Transform MyTransform;
    private GameObject My_Edge_Position;   
    private GameObject Index_Checker;
    private int IndexPoint;
    public List<GameObject> Edge_Positions;    //определяем переменную как хранилище всех стационарных позиций
    public Transform New_Edge_Position;        //внутренняя переменная, для хранения текущей/выбранной позиции
    public int index = 0;
    public bool Walking;

    public bool IsDebug;
    public int rotationSpeed = 4;                  //скорость поворота
    public float maxDistance = 0.5f;			   //максимальное приближение к игроку
    public int moveSpeed = 2;		           	   //скорость перемещения

    private bool CoulDown = false;

    void Start()
    {
        rotationSpeed = 7;
        maxDistance = 0.2f;
        moveSpeed = 3;	

        //назначаем новой переменной первое значение - пустой листинг
        Edge_Positions = new List<GameObject>();

        MyTransform = transform;

        AddAllPositions();
        
    }

    void Update()
    {
        if (Health_Player.Death_Over != true)
        { 
            if (MyTransform != New_Edge_Position)
                Move();
        }
    }

    #endregion

    #region Захват позиции

    //поиск и добавление всех противников в список
    public void AddAllPositions()
    {
        //помещаем все позиции в массив go
        GameObject[] go = GameObject.FindGameObjectsWithTag("Positions");
        //каждый элемент из найденных засовываем в массив потенциальных целей
        foreach (GameObject Positions in go)
            AddTarget(Positions);

        Edge_Positions = Edge_Positions.OrderBy(obj => obj.name).ToList();

        New_Edge_Position = Edge_Positions[index].transform;

        Index_Checker = Edge_Positions[index];

        SelectTarget();
    }

    //метод по добавлению в массив очередного элемента
    public void AddTarget(GameObject Positions)
    {
        Edge_Positions.Add(Positions);
    }

    //выбор конкретной цели из списка
    public void TargetPositions(bool IsClockwise)
    {
        if (!CoulDown)
        {

            if (IsClockwise == true)
            {
                index++;
                IndexPoint = index;
            }

            if (IsClockwise == false)
            {
                index--;
                IndexPoint = index;
            }

            if (index < 0 || index > 2)
            {
                if (index < 0)
                    index = 2;

                if (index > 2)
                    index = 0;
            }

            //снимаем пометку с текущей цели
            DeSelectTarget();
            // выбираем следующую
            New_Edge_Position = Edge_Positions[index].transform;

            Index_Checker = Edge_Positions[index];
            //метим ее цветом	

            SelectTarget();

            StartCoroutine(CoulDownTimer());
        }
    }

    //метим выбранный элемент - врага цветом
    private void SelectTarget()
    {
       // Current_Edge_Position.renderer.material.color = Color.red;
    }

    //Убираем пометку выбранной цели
    private void DeSelectTarget()
    {
       // Current_Edge_Position.renderer.material.color = Color.white;
    }

    IEnumerator CoulDownTimer()
    {
        CoulDown = true;
        yield return new WaitForSeconds(1);
        CoulDown = false;
    }

    #endregion
    
    #region Движение

    private void Move() // Двигаемся к сокету
    {
        Walking = true;

        GetComponentInChildren<MyAnim>().Run(moveSpeed); //Запускаем анимацию бега
        if(!IsDebug)
        MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation, Quaternion.LookRotation(New_Edge_Position.position - MyTransform.position), rotationSpeed * Time.deltaTime);
        if(IsDebug)
        transform.LookAt(New_Edge_Position.transform);

        if (Vector3.Distance(New_Edge_Position.position, MyTransform.position) >= maxDistance)
        {
            //двигаемся к цели 
            MyTransform.position += MyTransform.forward * moveSpeed * Time.deltaTime;
        }

        else
        {
            Walking = false;
            GetComponentInChildren<MyAnim>().Run(0); //Запускаем анимацию бега
        }

        if (Walking == false)
            See_Forward();
    }

    private void See_Forward() // поворачиваемся
    {
        GameObject Take_A_Look;
        
        if (index == 0)
        {
            Take_A_Look = GameObject.Find("Side1");
            Look_At_Path(Take_A_Look);  
        }

        if (index == 1)
        {
            Take_A_Look = GameObject.Find("Side2");
            Look_At_Path(Take_A_Look); 
        }

        if (index == 2)
        {
            Take_A_Look = GameObject.Find("Side3");
            Look_At_Path(Take_A_Look);  
        }
    }

    private void Look_At_Path(GameObject Take_A_Look)
    {
        transform.LookAt(Take_A_Look.transform);
       /* MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation,
                                Quaternion.LookRotation(Take_A_Look.transform.position - MyTransform.position),
                                rotationSpeed * Time.deltaTime);*/
    }

    #endregion
}