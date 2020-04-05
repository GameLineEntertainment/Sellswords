using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progectile : MonoBehaviour
{
    //[HideInInspector]
    public BulletSettings Settings;

    [HideInInspector]
    public Collider MyCol;
    [HideInInspector]
    public Transform Target, ParentChar; // Цель
    
    protected Transform MyTransform; // Ссылка на траснформ объекта
    protected Vector3 _targetPosition;
    [Header("Триггер коллайдер должен быть вторым в списке!")]
    public bool Killed = false;  // Проверка нанесла ли стрела урон, чтобы избежать повторных атак и рекурсий  

    [Header("Данный скрипт не трогать, он работает, как по волшебству! Абракадабра!")]
    public bool Понятно;

    public virtual void ProgectileStart()
    {
        Destroy(gameObject, Settings.LifeTime);
        MyTransform = transform;

        if (MyCol != null)
        {
            MyCol.contactOffset = 1;
            print(MyCol.contactOffset);
        }        
        
        switch ((int)Settings.TypeOfDamage)
        {
            case 0: // Red
                {
                    gameObject.tag = "DD";
                    gameObject.layer = 0;//16;
                    break;
                }

            case 1: // Green
                {
                    gameObject.tag = "AoE";
                    gameObject.layer = 0;//17;
                    break;
                }

            case 2: // Blue
                {
                    gameObject.tag = "SS";
                    gameObject.layer = 0; //18;
                    break;
                }
        }
        // если запускаем не из руки
        if (!Settings.FromHand)
        {
            Vector3 pos = Target.position; // смотрим где стоит противник
            pos += Target.forward;        // Двигаем снаряд чутка вперёд от противника
            transform.position = pos;    // профит 

            if (Target != null)
            {
               // transform.LookAt(Target);  // Смотрим на противника
                MyTransform.parent = Target;  // Считаем относительно координат противника (немножечко вкусненького гавнокода)
                MyTransform.localPosition = new Vector3(0, 0, 0);
                MyTransform.localPosition += Settings.Spawn_Offset; // делаем отступ от точки спауна

                if (!Settings.FollowToEnemy)
                    MyTransform.parent = null; //Возвращаем всё на место 

                if (Settings.LookAtTarget)
                    transform.LookAt(Target);  // Смотрим на противника

                else
                    transform.rotation = Quaternion.Euler(Settings.StartRotation);
            }
            else
            {
                Debug.LogError("Нет цели");
            }
        }

        else
        {
            MyTransform.parent = ParentChar; // Ставим гг, как родителя
            MyTransform.localPosition += Settings.Spawn_Offset; // делаем отступ от точки спауна

            if (!Settings.LookAtTarget)
                transform.rotation = Quaternion.Euler(Settings.StartRotation);

            MyTransform.parent = null; //Возвращаем всё на место 

            if (Settings.LookAtTarget)
                transform.LookAt(Target);  // Смотрим на противника            
        }        
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        transform.position += transform.forward * Settings.Speed * Time.deltaTime;


        if (!Killed && !Settings.IsKillbyRay)
        {          
            MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation,
                                    Quaternion.LookRotation(GetTargetPosition),
                                    Settings.RotationSpeed * Time.deltaTime);
            print(Target.name);
        }
    }
    
    protected void Engage()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (Killed == false)
        {
            MiniEnemyAI go = other.gameObject.GetComponent<MiniEnemyAI>();

            if (go == null)
                return;

            Killed = true;                    

            bool Suc = go.Health(Settings.Damage, Settings.TypeOfDamage);
            gameObject.layer = 12; // Это может породить некоторы баги, так что тут смотри внимательнее! Если ты залез искать шибку, вот она!

            if (!Suc)
            {
                if (Settings.NotDamageEffect != null)
                {
                    Instantiate(Settings.NotDamageEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Метод для убийства врага с помощью рейкаста. (*Клим)
    /// </summary>
    protected virtual void KillEnemyByRay()
    {
        if (Killed) return;
    }

    /// <summary>
    /// Свойство для получения места бота, чтобы отойти от Update
    /// </summary>
    protected Vector3 GetTargetPosition
    {
        get
        {
            return _targetPosition = Target.position - MyTransform.position;
        }

        private set
        {
            _targetPosition = value;
        }
    }
}
