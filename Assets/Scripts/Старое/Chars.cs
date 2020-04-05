using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class Chars : MonoBehaviour
{
    #region Char_Controller

    public Transform MyTransform;
    public GameObject My_Edge_Position;
    public GameObject Index_Checker;
    public int IndexPoint;
    public List<GameObject> Edge_Positions;    //определяем переменную как хранилище всех стационарных позиций
    public Transform New_Edge_Position;        //внутренняя переменная, для хранения текущей/выбранной позиции
    public int index = 0;
    public bool Walking;

    public bool IsDebug;
    public int rotationSpeed = 4;                  //скорость поворота
    public float maxDistance = 0.5f;			   //максимальное приближение к игроку
    public int moveSpeed = 2;		           	   //скорость перемещения

    public bool CoolDown = false;

    #endregion

    #region Character

    public GameObject Projectile;
    public GameObject Enemy_Check;

    public List<Transform> Enemyes;
    public Transform AttackPoint;
    public Transform Enemy;

    public int Current_index;
    public string Tag;

    public bool CanAttack = false;
    public bool CoulDown = false;
    public bool SortDistance = false;
    public int Health;

    #endregion

    void Start()
    {
        CharsStart();
    }

    void Update()
    {
        CharsUpdate();
    }

    void CharsStart()
    {
        #region Char_Controller

        rotationSpeed = 7;
        maxDistance = 0.2f;
        moveSpeed = 3;

        //назначаем новой переменной первое значение - пустой листинг
        Edge_Positions = new List<GameObject>();

        MyTransform = transform;

        AddAllPositions();

        #endregion

        #region Character
        
        Current_index = index;             // переменная текущей точки index, где мы сейчас стоим

        #endregion
    }

    void CharsUpdate()
    {
        #region Char_Controller

        if (Health_Player.Death_Over != true)
        {
            if (MyTransform != New_Edge_Position)
                Move();
        }

        #endregion

        #region Character

        if (ButtonChange.Turn)
        {
            if (Current_index != index)    // проверка, соответствия переменных о нашем местоположении        
            {
                Enemy_Check = null;
                Current_index = index;     // если не соотвествует, то перезаписываем                           
                Chek_Position();
            }
        }

        #endregion
    }


    #region Char_Controller

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
    }

    //метод по добавлению в массив очередного элемента
    public void AddTarget(GameObject Positions)
    {
        Edge_Positions.Add(Positions);
    }

    //выбор конкретной цели из списка
    public void TargetPositions(bool IsClockwise)
    {
        if (!CoolDown)
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

            // выбираем следующую
            New_Edge_Position = Edge_Positions[index].transform;

            Index_Checker = Edge_Positions[index];
            //метим ее цветом	

            StartCoroutine(CoulDownTimer());
        }
    }

    public IEnumerator CoulDownTimer()
    {
        CoolDown = true;
        yield return new WaitForSeconds(1);
        CoolDown = false;
    }


    public void Move() // Двигаемся к сокету
    {
        Walking = true;

        GetComponentInChildren<MyAnim>().Run(moveSpeed); //Запускаем анимацию бега
        if (!IsDebug)
            MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation, Quaternion.LookRotation(New_Edge_Position.position - MyTransform.position), rotationSpeed * Time.deltaTime);
        if (IsDebug)
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

    public void See_Forward() // поворачиваемся
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

    public void Look_At_Path(GameObject Take_A_Look)
    {
        transform.LookAt(Take_A_Look.transform);
        /* MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation,
                                 Quaternion.LookRotation(Take_A_Look.transform.position - MyTransform.position),
                                 rotationSpeed * Time.deltaTime);*/
    }

    #endregion


    #region Character

    public void Chek_Position()         // проверяем позицию, где стоим, чтобы выбрать наших врагов.
    {
        if (Current_index == 0)
        {
            Enemy_Check = GameObject.FindGameObjectWithTag("Down_Enemy");
            Tag = ("Down_Enemy");
        }

        if (Current_index == 1)
        {
            Enemy_Check = GameObject.FindGameObjectWithTag("Right_Enemy");
            Tag = ("Right_Enemy");
        }

        if (Current_index == 2)
        {
            Enemy_Check = GameObject.FindGameObjectWithTag("Left_Enemy");
            Tag = ("Left_Enemy");
        }

        if (Enemy_Check != null)
        {
            if (!SortDistance)
                StartCoroutine(Attack());                 // запускаем ф-цию атаки

            if (SortDistance)
                StartCoroutine(CheckTargets());
        }
    }

    IEnumerator CheckTargets()
    {
        //Ну субоственно это и есть та самая блядская хуйня
        Enemyes = new List<Transform>();                  // Создаём листинг

        yield return new WaitForSeconds(1);
        Enemy = null;
        Enemyes = null;     // обнуляем листинг
        Enemyes = new List<Transform>();

        if (Enemy_Check != null)
            WorkWithTagets();
    }

    private void WorkWithTagets()
    {

        GameObject[] Enemyes_massive = GameObject.FindGameObjectsWithTag(Tag);
        foreach (GameObject MyEnemy in Enemyes_massive)
            AddTarget(MyEnemy.transform);

        if (Enemyes != null)
            SortByDistance();
    }

    public void AddTarget(Transform MyEnemy)
    {
        Enemyes.Add(MyEnemy);
    }

    private void SortByDistance()
    {
        Enemyes.Sort(delegate(Transform t1, Transform t2)
        {
            return Vector3.Distance(t1.position, MyTransform.position).CompareTo(Vector3.Distance(t2.position, MyTransform.position));
        });

        Enemy = Enemyes[0];                   //Элемент для стрелы (Ближайшая цель)

        StartCoroutine(Attack());             // запускаем ф-цию атаки
    }


    public abstract IEnumerator Attack();     // Ф-ция атаки

    #endregion
}