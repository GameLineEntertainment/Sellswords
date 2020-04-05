using System;
using UnityEngine;
using System.Collections;


public class InfinitySpawner : MonoBehaviour 
{
    //Переменные для проверки остались враги на сцене
    //public bool IsDirector = false;
    public GameObject Left, Right, Down;
    public GameObject[] Enemies;       //Количество противников
    public SpawnTut[] Spawners = new SpawnTut[3];

    public float StartDelay, Delay, RefreshCheckTime;
    public int проход = 0;
    public bool Spawned = false;

    [SerializeField]
    protected MiniEnemyAI[] EnemyObject; // Ссылка на скрипт противников в массиве

    // Use this for initialization
    //void Start()
    //{ InfinitySpawnerStart(); }
	
    //public void InfinitySpawnerStart()
    //{
    //    EnemyObject = new MiniEnemyAI[Enemies.Length];

    //    for (int i = 0; i < EnemyObject.Length; i++)
    //    {
    //        EnemyObject[i] = Enemies[i].GetComponent<MiniEnemyAI>();
    //    }


    //    for (int i = 0; i <= 2; i++)
    //    {
    //        Spawners[i] = GameObject.Find("Side" + Convert.ToString(i + 1)).GetComponent<SpawnTut>();
    //    }


    //    InvokeRepeating(nameof(CheckAlive), StartDelay, RefreshCheckTime);
    //}
    //  public virtual IEnumerator Spawn()
    //  {
    //   проход = UnityEngine.Random.Range(0, Enemies.Length);

    //   Spawned = true;
    //   yield return new WaitForSeconds(Delay);
    //   int SpawnIndex = UnityEngine.Random.Range(0, Spawners.Length);
    //   // 
    //   Spawners[SpawnIndex].Spawn(Enemies[проход]);
    //   проход++;
    //   Spawned = false;
    //  }
         

    //public void CheckAlive()            //Проверка наличия противников на сцене
    //{
    //    Left = GameObject.FindGameObjectWithTag("Left_Enemy");
    //    Right = GameObject.FindGameObjectWithTag("Right_Enemy");
    //    Down = GameObject.FindGameObjectWithTag("Down_Enemy");

    //    if (Left == null && Right == null && Down == null)
    //    {
    //        StartCoroutine(Spawn());
    //    }
    //}
}
