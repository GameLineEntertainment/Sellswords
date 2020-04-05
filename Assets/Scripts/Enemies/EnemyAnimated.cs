using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAnimated : MonoBehaviour
{
    #region Базовые функции

    //Определяем настраиваемые переменные
    public Transform target;		    //цель
    public float moveSpeed;			    //скорость перемещения   
    public float maxDistance;			//максимальное приближение к игроку
    public Transform selectedTarget;    // Выбранная цель
    public int Health_Point;            // Количество здоровья
    public bool Freezed = false;
    public bool CanAttack;
    public bool stay;
    public float Speed;

    private Animator anim;
    private Transform MyTransform; 	//временная переменная

    public ParticleSystem FreezeParticle;

    //определяем переменную как хранилище всех врагов
    public List<Transform> targets;

    void Awake()  //  Инициализация при активации
    {
        //ссылаемся на свойство transform, чтобы сократить
        //время обращения к нему в теле скрипта
        MyTransform = transform;
    }

    // Начальная инициализация
    void Start()
    {
        anim = GetComponent<Animator>();
        //moveSpeed = 1;
        targets = new List<Transform>();
        MyTransform = transform;
        AddAllEnemies();
        CanAttack = true;
        Speed = 1;
        FreezeParticle = GetComponent<ParticleSystem>();
        //TargetEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health_Point > 0)
        {
            if (!stay)
            {
                anim.SetFloat("Speed", moveSpeed);
                if(target != null)
                FollowToChar();
            }

            if (Input.anyKey)
                // StartCoroutine(EraseList());

                if (Health_Point <= 0)
                {
                    Die();
                }

            // StartCoroutine(Attack());

            // if (Freezed)
            //  StartCoroutine(FreezeUp());

            if (stay)
            {
                moveSpeed = 0;
                anim.SetFloat("Speed", moveSpeed);
            }
        }
    }

    #endregion

    #region Работа с листингом
    
    public void AddAllEnemies() // Добавление всех целей в листинг
    {
        //помещаем всех врагов в массив go
        GameObject[] go = GameObject.FindGameObjectsWithTag("Char");
        //каждый элемент из найденных засовываем в массив потенциальных целей
        foreach (GameObject Character in go)
            AddTarget(Character.transform);

        SortTargetByDistance();
    }

    //метод по добавлению в массив очередного элемента
    public void AddTarget(Transform Character)
    {
        targets.Add(Character);
    }

    //выбор конкретной цели из списка
    private void TargetEnemy()
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
    //метим выбранный элемент - врага цветом
    public void SelectTarget()
    {
        target = selectedTarget;
    }

    //Убираем пометку выбранной цели
    public void DeSelectTarget()
    {
    }

    public IEnumerator EraseList()
    {
        yield return new WaitForSeconds(0.5f); //считаем время
        targets = null;
        targets = new List<Transform>();
        AddAllEnemies();
    }
    
    #endregion

    #region Движение_И_Атака

    void FollowToChar()
    {
        if (Vector3.Distance(target.position, MyTransform.position) >= maxDistance || TestMove.stop == false)
        {
            //двигаемся к цели 
            moveSpeed = Speed;
            anim.SetFloat("Speed", moveSpeed);
        }

        //если дистанция не позволяет, не двигаемся к цели
        if (Vector3.Distance(target.position, MyTransform.position) <= maxDistance || TestMove.stop == true)
        {
            moveSpeed = 0;
            anim.SetFloat("Speed", moveSpeed);
            //Attack();
        }
    }

    public void Attack()
    {
        anim.SetBool("Attack", true);
    }
   
    #endregion

    public IEnumerator FreezeUp(float Hp, float FreezeTime) // Заморозка
    {
        //EnemyAI Hp = GetComponentInParent<EnemyAI>();

        Debug.Log("Всё вроде норм");
            Freezed = false;
            moveSpeed = 0;
            //renderer.material.color = Color.blue;

            Animator scriptAnim = GetComponent<Animator>();          //Ссылка на объект

            //FreezeParticle.Play();

            //Отключение всякого говна                            
            scriptAnim.enabled = false;

            yield return new WaitForSeconds(FreezeTime); //считаем время

            if (Hp > 0)
            {
                //Включение всякого говна
                scriptAnim.enabled = true;

                //renderer.material.color = Color.white;
                moveSpeed = 1;
            }
        
    }

    private void Die()
    {
        RagDoll got = GetComponent<RagDoll>();
        got.KnockedOut = true;

        //Rigidbody rigid = GetComponent<Rigidbody>();
        //DestroyObject (rigid);

        tag = "Untagged";
    }
}