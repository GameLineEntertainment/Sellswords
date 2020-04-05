using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public float NormanSpawnTime;		    // The amount of time between each spawn.nTime;
    public float BigmanSpawnTime;
    public float BowmanSpawnTime;
    public float IcemanSpawnTime;

    public int NormanCount;
    public int BigManCount;
    public int BowManCount;
    public int IceManCount;

    public float NormanDelay;		    // The amount of time between each spawn.nTime;
    public float BigmanDelay;
    public float BowmanDelay;
    public float IcemanDelay;

    public GameObject Norman;
    public GameObject Bigman;
    public GameObject Bowman;
    public GameObject Iceman;

    [HideInInspector]
    public GameObject Left;
    [HideInInspector]
    public GameObject Right;
    [HideInInspector]
    public GameObject Down;

    private float spawnDelay = 1;		// The amount of time before spawning starts.
	//public GameObject[] enemies;		// Array of enemy prefabs.
    private float A_CurValue, B_CurValue, C_CurValue, D_CurValue;
    private float A_New, B_New, C_New, D_New;

    public bool TagLeft, TagRight, TagDown, Ванино, Проверка = false;
    //public int Point = 1;
    //public int[] Point;



    private int EnemyCounter;
    //public bool[] enemies;   


	void Start ()
	{     
        A_CurValue = NormanSpawnTime;
        B_CurValue = BigmanSpawnTime;
        C_CurValue = BowmanSpawnTime;
        D_CurValue = IcemanSpawnTime;

        Debug.Log("Респауню при старте");
        Spawner();
	}

    void Update()
    {
        if (Input.anyKey)
        {
            A_New = NormanSpawnTime;
            B_New = BigmanSpawnTime;
            C_New = BowmanSpawnTime;
            D_New = IcemanSpawnTime;

            if (A_CurValue != A_New || B_CurValue != B_New || C_CurValue != C_New || D_CurValue != D_New)
            {
                A_CurValue = NormanSpawnTime;
                B_CurValue = BigmanSpawnTime;
                C_CurValue = BowmanSpawnTime;
                D_CurValue = IcemanSpawnTime;

                CancelInvoke("SpawnNorman");
                CancelInvoke("SpawnBigman");
                CancelInvoke("SpawnBowman");
                CancelInvoke("SpawnIceman");
                Spawner();
            }
        }

        if (NormanCount <= 0)        
            CancelInvoke("SpawnNorman");            

        if (BigManCount <= 0)             
            CancelInvoke("SpawnBigman");

        if (BowManCount <= 0)               
            CancelInvoke("SpawnBowman");

        if (IceManCount <= 0)               
            CancelInvoke("SpawnIceman");

        if (NormanCount <= 0 && BigManCount <= 0 && BowManCount <= 0 && IceManCount <= 0)
        {
            if (!Проверка)
            {
                Проверка = true;
                Debug.Log("Все среспаунились");
                StartCoroutine(TimeChecker());
            }
        }

    }

    
    void Spawner()
    {
        if (NormanSpawnTime != 0 && NormanCount >= 0)
            InvokeRepeating("SpawnNorman", NormanDelay, NormanSpawnTime);

        if (BigmanSpawnTime != 0 && BigManCount >= 0)
            InvokeRepeating("SpawnBigman", BigmanDelay, BigmanSpawnTime);

        if (BowmanSpawnTime != 0 && BowManCount >= 0)
            InvokeRepeating("SpawnBowman", BowmanDelay, BowmanSpawnTime);

        if (IcemanSpawnTime != 0 && IceManCount >= 0)
            InvokeRepeating("SpawnIceman", IcemanDelay, IcemanSpawnTime); 

        /*
        if (enemies[3] != null)
            InvokeRepeating("SpawnIceman", spawnDelay, BowmanSpawnTime);      
        */

    }


    void SpawnNorman()
	{
		// Instantiate a random enemy.
		//int enemyIndex = Random.Range(0, enemies.Length);
        if (TagLeft)
            Norman.gameObject.tag = "Left_Enemy";
        if (TagRight)
            Norman.gameObject.tag = "Right_Enemy";
        if (TagDown)
            Norman.gameObject.tag = "Down_Enemy";
        Instantiate(Norman, transform.position, transform.rotation);
        NormanCount --;
	}

    void SpawnBigman()
    {
        if (TagLeft)
            Bigman.gameObject.tag = "Left_Enemy";
        if (TagRight)
            Bigman.gameObject.tag = "Right_Enemy";
        if (TagDown)
            Bigman.gameObject.tag = "Down_Enemy";
        Instantiate(Bigman, transform.position, transform.rotation);
        BigManCount --;
    }

    void SpawnBowman()
    {
        if (TagLeft)
            Bowman.gameObject.tag = "Left_Enemy";
        if (TagRight)
            Bowman.gameObject.tag = "Right_Enemy";
        if (TagDown)
            Bowman.gameObject.tag = "Down_Enemy";
        Instantiate(Bowman, transform.position, transform.rotation);
        BowManCount --;
    }

    void SpawnIceman()
    {
        if (TagLeft)
            Iceman.gameObject.tag = "Left_Enemy";
        if (TagRight)
            Iceman.gameObject.tag = "Right_Enemy";
        if (TagDown)
            Iceman.gameObject.tag = "Down_Enemy";
        Instantiate(Iceman, transform.position, transform.rotation);
        IceManCount --;
    }
    /*
    void SpawnIceman()
    {
        Instantiate(enemies[3], transform.position, transform.rotation);
    }
     * */

    IEnumerator TimeChecker()
    {
        if (!Ванино)
        {
            Debug.Log("Все среспаунились, чекаем");
            yield return new WaitForSeconds(6); //считаем время

            Left = GameObject.FindGameObjectWithTag("Left_Enemy");
            Right = GameObject.FindGameObjectWithTag("Right_Enemy");
            Down = GameObject.FindGameObjectWithTag("Down_Enemy");

            if (Left == null && Right == null && Down == null)
            {
                Debug.Log("Заканчиваем игру");
                GameOver.Win = true;
                GameOver.OverGame = true;
            }

            Проверка = false;
        }
    }
}