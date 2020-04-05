using UnityEngine;
using System;
using System.Collections;

public class EnumSpawner : MonoBehaviour 
{    
    //Переменные для проверки остались враги на сцене
    public GameObject Left, Right, Down;
    public GameObject[] Enemies;       //Количество противников
    public GameObject[] Spawners = new GameObject[3];

    public float Delay;
    public int проход = 0;
    public bool Spawned = false;

    void Start()
    {EnumSpawnerStart();}

    void Update()
    { }

    // Use this for initialization
    public void EnumSpawnerStart()
    {
        for(int i = 0; i <= 2; i++)
        {
            Spawners[i] = GameObject.Find("Side" + Convert.ToString(i + 1));
        }
        StartCoroutine(Spawn());
        InvokeRepeating("CheckAlive", 0, 2);
    }

    // Update is called once per frame
    public void EnumSpawnerUpdate()
    {
    }


    private IEnumerator Spawn()
    {
        if (Spawned == false)
        {
            if (проход > Enemies.Length - 1)
            {
                StartCoroutine(TimeChecker());
            }

            Spawned = true;
            yield return new WaitForSeconds(Delay);
            int SpawnIndex = UnityEngine.Random.Range(0, Spawners.Length);
            // 
            Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[проход]);
            проход++;
            Spawned = false;
        }
    }

    public void CheckAlive()            //Проверка наличия противников на сцене
    {
        Left = GameObject.FindGameObjectWithTag("Left_Enemy");
        Right = GameObject.FindGameObjectWithTag("Right_Enemy");
        Down = GameObject.FindGameObjectWithTag("Down_Enemy");

        if (Left == null && Right == null && Down == null)
        {
            StartCoroutine(Spawn());
        }       
    }

    public IEnumerator TimeChecker()
    {
        yield return new WaitForSeconds(0); //считаем время

        Left = GameObject.FindGameObjectWithTag("Left_Enemy");
        Right = GameObject.FindGameObjectWithTag("Right_Enemy");
        Down = GameObject.FindGameObjectWithTag("Down_Enemy");

        if (Left == null && Right == null && Down == null)
        {
            GameLevel.Win = true;
            GameLevel.OverGame = true;
        }
    }
}
