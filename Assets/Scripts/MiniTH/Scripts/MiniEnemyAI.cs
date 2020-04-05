using UnityEngine;
using System;
using System.Collections;
using OldSellswords;

public class MiniEnemyAI : EnemyAI
{
   // public enum TypeEnemy { Red = 0, Green = 1, Blue = 2 }
   // public TypeEnemy TypeOfEnemy = TypeEnemy.Red;
    
    public Enemy EnemyDate;

    [HideInInspector]
    public bool AntiGravity = false;
    public bool DistantEnemy = false;
    public GameObject Bullet;
    public GameObject StartBullet;
    public string Name = "Зомби";


    // Use this for initialization
    public override void Start()
    {
        moveSpeed = UnityEngine.Random.Range(MinSpeed, MaxSpeed);

        EnemyAIStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health_Point > 0)
        {
            if (target != null)
                FollowToChar();

            if (Input.anyKey)
            {
                StartCoroutine(EraseList());
                SelectTarget();
            }
        }
    }

    override public IEnumerator Attack()
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
                else
                    StartCoroutine(Shoot(2));
            }

            /*
            if (target.name == "Char0(Clone)" && CanAttack) //Если можем атаковать и если цель...
            {
                CanAttack = false; // не можем атаковать ещё раз
                if (!DistantEnemy)
                    StartCoroutine(Exicution(0));
                else
                    StartCoroutine(Shoot(0));
            }

            if (target.name == "Char1(Clone)" && CanAttack)
            {
                CanAttack = false;
                if (!DistantEnemy)
                    StartCoroutine(Exicution(1));
                else
                    StartCoroutine(Shoot(1));
            }


            if (target.name == "Char2(Clone)" && CanAttack)
            {
                CanAttack = false;
                if (!DistantEnemy)
                    StartCoroutine(Exicution(2));
                else
                    StartCoroutine(Shoot(2));
            }
            */
        }
    }

    //Непосредственное отнимание жизней
    //ВНИМАНИЕ ВО ВТОРОЙ СТРОЧКЕ ЕСТЬ GETCOMPONENT можно придумать хитрый способ для оптимизации и избавления от этого действия
    public IEnumerator Shoot(int Char)
    {
       // GameObject Player = GameObject.Find(Char);

        float distance = Vector3.Distance(target.transform.position, transform.position); //вводим переменную distance и вычисляем расстояние между игроком и его целью

        if (distance < maxDistance && Health_Point > 0)
        {
           CharAnim[Char].Damage(true); //Запускаем анимацию урона у перса

           if (Bullet != null)
           {
	           GameObject Projectile = Instantiate(Bullet) as GameObject;

	           Projectile.transform.position = StartBullet.transform.position;
	           EnemyArrow MyArrow = Projectile.GetComponent<EnemyArrow>();
	           MyArrow.Target = target.gameObject;
	           MyArrow.Damage = Damage;
           }
        }

        DefenceGod.GodSave = true;
        yield return new WaitForSeconds(CoulDown); //считаем время
        CanAttack = true;
    }

    public override void KilledByPlayer()
    {
	    Die();
	    OnKilledByPlayerChange?.Invoke(this);
	}


	public event Action<MiniEnemyAI> OnDieChange;
	public event Action<MiniEnemyAI> OnKilledByPlayerChange;
	public override void Die()
    {
        MyAgent.speed = 0;
        if (MyAgent.enabled)
            MyAgent.SetDestination(MyTransform.position);
        MyAgent.enabled = false;
        gameObject.tag = "Dead";
        //Counter.Kills++;
        ///////////////////////////////////////////////////////////////////////////////////////////////// НЕ ГАВНОКОД, А ГАВНОКОДИЩЕ!!!!!!!!!!!!!!!!!!!!!!
        RagOn rg = GetComponent<RagOn>();
        if (!AntiGravity)
            rg.TriggerOn();
        else
            rg.TriggerOn(true);
        //Counter.Kills++;
        Variables.Score += Score;
        ///////////////////////////////////////////////////////////////////////////////////////////////// НЕ ГАВНОКОД, А ГАВНОКОДИЩЕ!!!!!!!!!!!!!!!!!!!!!!
        GameLevel HP = GameObject.Find("Scripts").GetComponent<GameLevel>(); //Общие жизни
        Variables.KillsPerSession++;
        HP.SetCountText();
        //Variables.Money += Money;
        Variables.Grow_Exp(Score * (int)StartSpeed);        
        Variables.MiniSummEnemies(Convert.ToString(EnemyColor));

        // спавн предметов после смерти
        EnemyDrop enemyDrop = GetComponent<EnemyDrop>();
        if (enemyDrop != null) enemyDrop.dropItems();
        OnDieChange?.Invoke(this);
	}
}
