using UnityEngine;
using System.Collections;

public class NewSpawner : MonoBehaviour 
{
    public GameObject[] Enemies;       //Количество противников
    public int[] count;                //Их число
    public float[] Delay;              //Задержка перед респауном
    public float[] SpawnTime;          //Время между спаунами

    //Переменные для проверки остались враги на сцене
    [HideInInspector]
    public GameObject Left;
    [HideInInspector]
    public GameObject Right;
    [HideInInspector]
    public GameObject Down;

    private float[] CurTime, StartTime;
    public float Время; 

    //private int EnemyCounter;        //Считаем количество врагов
    public int BaseCount;              //Переменная о количестве врагов
    private int CurSpawn;              //Переменная кого сейчас респауним    

	// Use this for initialization
	void Start () 
    {
        StartTime = new float[BaseCount];

        //for (int i = 0; i < BaseCount; i++)
            //StartTime[i] = Time.time;

        CurTime = new float[BaseCount];
	}
	
	// Update is called once per frame
	void Update () 
    {
        Время = Time.time;

        for (int i = 0; i < BaseCount; i++)
        {
            if (count[i] <= 0)
            {
                Debug.Log("Отменяю спаун");
                CancelInvoke("Spawn");
                StartCoroutine(TimeChecker());
            }
        }


        for (int i = 0; i < BaseCount; i++)
        {
            if (Spawn(i)) break;
        }           
	}

    /*
    IEnumerator Starter()
    {
        Debug.Log("Стартанул");
        for (int i = 0; i < BaseCount; i++)
        {
            Debug.Log("Сделал проход" + i);

            if (SpawnTime[i] != 0 && count[i] >= 0)
            {
                Debug.Log("настраиваю спаун");
                yield return new WaitForSeconds(1);
                CurSpawn = i;
                InvokeRepeating("Spawn", Delay[i], SpawnTime[i]);
            }
        } 
     
    }*/

    bool Spawn(int i)
    {
        if ((Time.time > Delay[i]) && count[i] >= 0 && (CurTime[i] < Time.time - SpawnTime[i]))
        {
            Instantiate(Enemies[i], transform.position, transform.rotation);
            count[i]--;
            CurTime[i] = Time.time;
            return true;
        }
        return false;
    }

    IEnumerator TimeChecker()
    {
        yield return new WaitForSeconds(6); //считаем время

        Left = GameObject.FindGameObjectWithTag("Left_Enemy");
        Right = GameObject.FindGameObjectWithTag("Right_Enemy");
        Down = GameObject.FindGameObjectWithTag("Down_Enemy");

        if (Left == null && Right == null && Down == null)
        {
            GameOver.Win = true;
            GameOver.OverGame = true;
        }
    }
}


