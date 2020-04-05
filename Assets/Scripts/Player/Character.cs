using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OldSellswords;

public abstract class Character : MonoBehaviour
{

    public GameObject Projectile;



    public Transform myTransform { get; private set; }
    public GameObject My_Edge_Position;
    public GameObject Index_Checker;
    public int IndexPoint;
    public List<GameObject> Edge_Positions;    //определяем переменную как хранилище всех стационарных позиций
    public Transform New_Edge_Position;       //внутренняя переменная, для хранения текущей/выбранной позиции
    public int index = 0;
    public bool Walking;

    public bool IsDebug;
    public int rotationSpeed = 4;                  //скорость поворота
    public float maxDistance = 0.5f;               //максимальное приближение к игроку
    public int moveSpeed = 2;                      //скорость перемещения
    public float CoulDownTime = 1.1f;

    public bool CoolDown = false;
    public MyAnim MyAnim;
    public int Current_index;
    public MeshRenderer CharCircle;
    public ColorGroup ColorGroup;
    public string Tag;


    public BulletSettings[] Bullet; // Переделать в лист, чтобы удалять у экземпляра перса на сцене все снаряды, которые не пашут на данном уровне

    public GameObject Enemy_Check;
    public List<Transform> Enemyes;
    public Transform AttackPoint;
    public Transform Enemy;

    public bool CanAttack = false;
    public bool CoulDown = false;
    public bool SortDistance = false;
    public float Health;

    public abstract IEnumerator Attack(); // Ф-ция атаки


    private void Start()
    {
        myTransform = transform;
    }
}