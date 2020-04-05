using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using OldSellswords;

public class EnemyAI : MonoBehaviour
{
    #region
    [Tooltip("Очки опыта, за убитого умноженные на скорость")]
    public int Score = 0; // Очки за убийство и деньги
   
    public float Health_Point;            // Количество здоровья
    //public bool NonDamaging = false;
    public bool IsAlive = true;

    #endregion

    #region Базовые функции EnemyAI

    //Определяем настраиваемые переменные

    //определяем переменную как хранилище всех врагов
    [HideInInspector]
    public List<Transform> targets;
    //s [HideInInspector]
    public CharacterComponent target;		    //цель
     [HideInInspector]
    public Transform selectedTarget;    // Выбранная цель
    public Transform MyTransform; 	//временная переменная
    [HideInInspector]
    public float StartSpeed;           //Запоминаем начальную скорость, чтобы вернуть после заморозки
    public float StandUpTimer;           
    [HideInInspector]
    public int StarRotation;           //Запоминаем начальную скорость поворота, чтобы вернуть после заморозки
    [HideInInspector]
    public bool Freezed = false;           //проверка заморозки     

    public int RotationSpeed;		    //скорость поворота    
    public int Damage;

    public float maxDistance;			//максимальное приближение к игроку
    public float moveSpeed;             //скорость перемещения  
    [Tooltip("Диапазон скорости")] 
    public float MinSpeed = 4, MaxSpeed = 8;   // Диапазон скорости
    // public float RangeSpeed = 5;            // Диапазон скорости

    public float CastTime = 0.3f;     //Время замаха перед ударом
    public float CoulDown = 1;          //Время перезарядки атаки
    public float FreezeTime = 4;        //Время заморозки  

    public bool Заморожен = false;         //Переменная для дебага, используется в жизнях врага
    //public bool CanFreezed = true;         //Проверка замораживаемый ли объект
    //public bool IsBigGuy = false;          //Можно ли толкнуть
  //  [HideInInspector]
    public bool CanAttack;                //Может ли атаковать
    //[HideInInspector]
    public bool Move = true;               //Можел ли перемещаться
    public bool NearTarget = false;
    //public bool NonDamaging;              //Переменная для боумена
    public bool AoE_ignore, SS_ignore, DD_ignore; //Переменные для проверки, чем может быть нанесён урон.
    public ColorGroup EnemyColor = ColorGroup.Red;

    //public GameObject HealthBar, TextBar;
    public Slider Bar;
    public Text txt;
    public float maxHealth;

    public CharacterComponent[] Player = new CharacterComponent[3]; // = GameObject.Find(Char);
    public GameObject Shield; // Щит который показывает игроку, что мы промахнулись
    public MyAnim[] CharAnim = new MyAnim[3]; 
    public GameLevel HP; // = GameObject.Find("Scripts").GetComponent<GameLevel>(); //Общие жизни

    public NavMeshAgent MyAgent;
    private CharacterComponent[] _characters;

    public void Awake()  //  Инициализация при активации
    {
        //ссылаемся на свойство transform, чтобы сократить
        //время обращения к нему в теле скрипта
        MyTransform = transform;
        _characters = UnityEngine.Object.FindObjectsOfType<CharacterComponent>();
	}

    public void EnemyAIStart()
    {
        MyAgent = GetComponentInChildren<NavMeshAgent>();
        MyAgent.speed = moveSpeed;
        // GetComponent<Rigidbody>().isKinematic = false;
        targets = new List<Transform>();
        MyTransform = transform;        
        CanAttack = true;
        StartSpeed = moveSpeed;
        StarRotation = RotationSpeed;
        //TargetEnemy();

        Bar = GetComponentInChildren<Slider>();
        txt = GetComponentInChildren<Text>();
        maxHealth = Health_Point;

        HP = GameObject.Find("Scripts").GetComponent<GameLevel>(); //Общие жизни
        Player =  new CharacterComponent[3];
        CharAnim = new MyAnim[3];

        for (int i = 0; i < _characters.Length; i++)
        {
            Player[i] = _characters[i]; // GameObject.Find("Char" + i + "(Clone)"); // Находим объект с персонажа 
            CharAnim[i] = _characters[i].GetComponentInChildren<MyAnim>();//Player[i].GetComponentInChildren<MyAnim>();            
        }

        AddAllEnemies();
    }

    // Начальная инициализация
    public virtual void Start()
    {
        EnemyAIStart();
    }

    public void EnemyAIUpdate()
    {
        if (target != null)
        {
            FollowToChar();
        }

        if (Input.anyKey)
        {
            StartCoroutine(EraseList());
            SelectTarget();
        }

        //Lengh = Health_Point;
        //transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
        txt.text = Convert.ToString(Health_Point);
        Bar.maxValue = maxHealth;
        Bar.value = Health_Point;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAIUpdate();
    }

    #endregion

    #region EnemyAI

    public void AddAllEnemies() // Добавление всех целей в листинг
    {
        /*
        //помещаем всех врагов в массив go
        GameObject[] go = GameObject.FindGameObjectsWithTag("Char");
        //каждый элемент из найденных засовываем в массив потенциальных целей
        foreach (GameObject Character in go)
            AddTarget(Character.transform);
            */
        //SortTargetByDistance();
        SelectTarget();
    }

    //метод по добавлению в массив очередного элемента
    public void AddTarget(Transform Character)
    {
        targets.Add(Character);
    }
    /*
    //выбор конкретной цели из списка
    public void TargetEnemy()
    {
        selectedTarget = targets[0];

        SelectTarget();
    }

    //сортировка элементов списка по расстоянию до игрока
    //перый - самый ближний
    private void SortTargetByDistance()
    {
        targets.Sort(delegate(Transform t1, Transform t2)
        {
            return Vector3.Distance(t1.position, MyTransform.position).CompareTo(Vector3.Distance(t2.position, MyTransform.position));
        });

        TargetEnemy();
    }
    */
    //метим выбранный элемент - врага цветом
    public void SelectTarget()
    {
        //target = selectedTarget;

        for (int i = 0; i < Player.Length; i++)
        {
            if (Player[i].Index == 2)
                target = Player[i];
        }
    }

    //Убираем пометку выбранной цели
    public void DeSelectTarget()
    {
    }

    public IEnumerator EraseList()
    {
        yield return new WaitForSeconds(0.5f); //считаем время
       // targets = null;
       // targets = new List<Transform>();
       SelectTarget();
        AddAllEnemies();
    }

    

    public void FollowToChar() // Движение
    {

        //если дистанция позволяет, можем двигаться к цели
        if (Vector3.Distance(target.transform.position, MyTransform.position) > maxDistance && !NearTarget)
        {
            //двигаемся к цели             
            // MyTransform.position += MyTransform.forward * moveSpeed * Time.deltaTime; // последнее что было

            // GetComponentInChildren<EnemyAnimated>().Speed = moveSpeed;       // устарело 300 миллионов лет назад     
            if (MyAgent.enabled)
                MyAgent.SetDestination(target.transform.position);
        }

        if (Vector3.Distance(target.transform.position, MyTransform.position) <= maxDistance)
        {
            MyAgent.SetDestination(MyTransform.position); // если дошли до игроков стоим на месте
            NearTarget = true;
            StartCoroutine(Attack());
            var Anima = GetComponentInChildren<EnemyAnimated>();
            Anima.Attack();
            Anima.stay = true;
        }

        if (NearTarget)
        {
            // GetComponentInChildren<EnemyAnimated>().Speed = moveSpeed;
            StartCoroutine(Attack());
        }
    }



    public IEnumerator FreezeUp() // Заморозка
    {
        if (!SS_ignore)
        {
            //Freezed = false;
            Заморожен = true;
            moveSpeed = 0;
            RotationSpeed = 0;

            MyAgent.speed = moveSpeed;            

            //renderer.material.color = Color.blue;
            EnemyAnimated Гиви = GetComponentInChildren<EnemyAnimated>();
            StartCoroutine(Гиви.FreezeUp(Health_Point, FreezeTime));
            //Гиви.FreezeUp(Health_Point); //Freezed = true;            

            yield return new WaitForSeconds(FreezeTime); //считаем время

            Заморожен = false;
            CheckDeath();
            //renderer.material.color = Color.white;
            moveSpeed = StartSpeed;
            RotationSpeed = StarRotation;
            MyAgent.speed = moveSpeed;
        }
        //moveSpeed = StartSpeed;
    }

    public IEnumerator Pushing() // Толкание
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(0.75f); //считаем время

        if (!Заморожен)
            moveSpeed = StartSpeed;

        yield return new WaitForSeconds(0.75f); //считаем время
        if (moveSpeed == 0)
            StartCoroutine(Pushing());
    }

    public virtual IEnumerator Attack()
    {
        yield return new WaitForSeconds(CastTime); //считаем время

        float distance = Vector3.Distance(target.transform.position, transform.position); //вводим переменную distance и вычисляем расстояние между игроком и его целью
        Vector3 dir = (target.transform.position - transform.position).normalized;        //вычисляем единичный вектор направления к цели

        float direction = Vector3.Dot(dir, transform.forward); //вычисляем нахождение цели в поле зрения (значение 0 и отрицательное - сзади). Значение + впереди. Значение меняется от -1 до 1

        

        if (distance < maxDistance && direction > 0) //Проверка достаём ли до цели и находится ли она спереди
        {
            for (int i = 0; i < Player.Length; i++)
            {
                if (Player[i].Index == 2)
                {
                    CanAttack = false;
                    StartCoroutine(Exicution(2));
                }
            }

            /*
            if (target.name == "Char0(Clone)" && CanAttack) //Если можем атаковать и если цель...
            {
                CanAttack = false; // не можем атаковать ещё раз
                StartCoroutine(Exicution(0));
            }

            if (target.name == "Char1(Clone)" && CanAttack)
            {
                CanAttack = false;
                StartCoroutine(Exicution(1));
            }

            if (target.name == "Char2(Clone)" && CanAttack)
            {
                CanAttack = false;
                StartCoroutine(Exicution(2));
            }
            */
        }
    }

    //Непосредственное отнимание жизней
    //ВНИМАНИЕ ВО ВТОРОЙ СТРОЧКЕ ЕСТЬ GETCOMPONENT можно придумать хитрый способ для оптимизации и избавления от этого действия
    public virtual IEnumerator Exicution(int Char)
    {
        //yield return new WaitForSeconds(0.2f); //считаем время
        float distance = Vector3.Distance(target.transform.position, transform.position); //вводим переменную distance и вычисляем расстояние между игроком и его целью

        if (distance < maxDistance && Health_Point > 0)
        {
            CharAnim[Char].Damage(true); //Запускаем анимацию урона у перса
            HP.Health(Damage); //Атакуем общие жизни
        }
        //DefenceGod.GodSave = true;
        yield return new WaitForSeconds(CoulDown); //считаем время
        CanAttack = true;
    }


    #endregion

    #region EnemyHealth


    // Use this for initialization

    //public virtual void Die()
    //{
    //    IsAlive = false;
    //    //Counter.Kills++;
    //    RagOn rg = GetComponent<RagOn>();
    //    rg.TriggerOn();
    //    Counter.Kills++;
    //    Variables.Score += Score;
    //    Variables.Score += Score * (int)StartSpeed;
    //    // Variables.Money += Money;
    //    Variables.Grow_Exp(Score);
    //    Variables.SummEnemies(gameObject.name);
    //}


    /// <summary>
    /// отнимаем жизни и проверяем умер ли, вычитаем из жизней Damage с проверкой на HowDD возвращает true или false в зависимости от проверки
    /// </summary>
    /// <param name="Damage"></param>
    /// <param name="HowDD"></param>
    public bool Health(float Damage, string HowDD)  // отнимаем жизни
    {
        if (!(DD_ignore && HowDD == "DD") && !(SS_ignore && HowDD == "SS") && !(AoE_ignore && HowDD == "AoE"))
        {
            Health_Point -= Damage;
            CheckDeath();

            return true;
        }
        else
        {
            if (Shield != null)
            {
                Shield.SetActive(true);
                Invoke(nameof(DisableShield), 2);
            }
        }

        return false;
    }

    public bool Health(float Damage, ColorGroup color)  // отнимаем жизни
    {
        if(color == EnemyColor)
        {
            Health_Point -= Damage;
            CheckDeath();

            return true;
        }
        else
        {
            if (Shield != null)
            {
                Fall();
                Shield.SetActive(true);
                Invoke(nameof(DisableShield), 2);
            }
        }

        return false;
    }

    public void DisableShield()
    {
        Shield.SetActive(false);
    }



    /// <summary>
    /// Просто отнимаем у жизни без проверки 
    /// </summary>
    /// <param name="Damage"></param>
    public void Health(float Damage)
    {
        Health_Point -= Damage;

        CheckDeath();
    }

    public virtual void Die(){ }
    public virtual void KilledByPlayer() { }


	public void CheckDeath()
    {
        if (Заморожен == false)
            if (Health_Point <= 0)
            {
	            KilledByPlayer();

                var Anima = GetComponentInChildren<EnemyAnimated>();
                Anima.Health_Point = 0;
            }
            else 
            {
                print("Got mem but i'm still alive, bitch!");
                Fall();                
            }
    }



    public virtual void Fall()
    {
        Invoke(nameof(StandUp), StandUpTimer);
    } 
    
    public virtual void StandUp()
    {
    }
}

#endregion
