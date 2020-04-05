using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Archer : Character 
{

    public AudioSource Sound;
    public bool musicOn = true;

    override public IEnumerator Attack()     // Ф-ция атаки
    {
        ButtonChange.Attack = false;
        //if (!CoulDown && Enemy != null) // проверка на кулдаун и наличие цели
        if (!CoulDown && CanAttack)  // проверка на кулдау
        {
            CoulDown = true; //включаем кулдаун


            //В общем тут блядская хуйня в парент классе, которая уже подождала, надо типа исправить
            yield return new WaitForSeconds(CoulDownTime); //считаем время

            GetComponentInChildren<MyAnim>().Attack(true); //Запускаем анимацию атаки
            GameObject bullet = Instantiate(Projectile) as GameObject;  //делаем класс пули

            bullet.transform.position = AttackPoint.position;        // выплёвываем пулют из нашей точки

            CoulDown = false;
            //CanAttack = false;
        }
    }

    /*
    public GameObject Arrow;
    public GameObject MeleeAtack;
    public GameObject Enemy_Check;
    public Transform Enemy;
    public int Current_index;
    public Transform MyTransform;
    public string Tag;
    public bool CoulDown = false;
    public bool CanAttack = true;
    public int Health_Point;
    public GUIText HealthBar;
    public List<Transform> Enemyes;
    public static bool IsMeleeArch = false;
    public Transform AttackPoint;

    private float Timer = 0.2f;

	// Use this for initialization
	void Start () 
    {
        MyTransform = transform;
        Char_Controller index_Position = (Char_Controller)GetComponent("Char_Controller"); // переменная из класса Char_Controller на объекте, на котором лежит скрипт
        Current_index = index_Position.index;                                              // переменная текущей точки index, где мы сейчас стоим
        Health(0);
        Enemyes = new List<Transform>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (ButtonChange.Turn)
        {
            Char_Controller index_Position = (Char_Controller)GetComponent("Char_Controller");

            if (Current_index != index_Position.index)    // проверка, соответствия переменных из разных классов о нашем местоположении        
            {
                Enemy_Check = null;
                Current_index = index_Position.index;     // если не соотвествует, то перезаписываем                           
                Chek_Position();
            }
        }

        if (ButtonChange.Attack == true && Current_index == 2)
            Chek_Position();
    }

    void Chek_Position()         // проверяем позицию, где стоим, чтобы выбрать наших врагов.
    {
        
        if (Current_index == 0)  //добавляем всех врагов в листинг "низ"
        {
            Tag = "Down_Enemy";
            Enemy_Check = GameObject.FindGameObjectWithTag("Down_Enemy");
            StartCoroutine(CheckTargets());      
        }

        if (Current_index == 1) //добавляем всех врагов в листинг "право"
        {
            Tag = "Right_Enemy";
            Enemy_Check = GameObject.FindGameObjectWithTag("Right_Enemy");
            StartCoroutine(CheckTargets());
        }

        if (Current_index == 2) //добавляем всех врагов в листинг "лево"
        {
            Tag = "Left_Enemy";
            Enemy_Check = GameObject.FindGameObjectWithTag("Left_Enemy");
            StartCoroutine(CheckTargets());
        }
    }

    IEnumerator CheckTargets()
    {
        yield return new WaitForSeconds(1);
        Enemy = null;
        Enemyes = null;     // обнуляем листинг
        Enemyes = new List<Transform>();

        if(Enemy_Check != null)
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



    IEnumerator Attack()     // Ф-ция атаки
    {
        ButtonChange.Attack = false;
        //if (!CoulDown && Enemy != null) // проверка на кулдаун и наличие цели
        if (!CoulDown && CanAttack)  // проверка на кулдау
        {
            CoulDown = true; //включаем кулдаун

            yield return new WaitForSeconds(0); //считаем время

            GetComponentInChildren<MyAnim>().Attack(true); //Запускаем анимацию атаки
            GameObject bullet = Instantiate(Arrow) as GameObject;  //делаем класс пули

            bullet.transform.position = AttackPoint.position;        // выплёвываем пулют из нашей точки


            //float rad = Mathf.Atan2(MyTransform.position - Enemy.transform.position, mouse.x - transform.position.x);
            //float rad = Mathf.Atan2(1, 1);
            //bullet.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * rad));

            CoulDown = false;
            //CanAttack = false;
        }

    }


    public void Health(int Damage)  // отнимаем жизни
    {
        Health_Point = Health_Point - Damage;
        HealthBar.text = ("Archer Health: ") + Health_Point;
    }   
     */
}
