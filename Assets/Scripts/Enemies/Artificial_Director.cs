using UnityEngine;
using System.Collections;

public class Artificial_Director : InfinitySpawner 
{
    float[] MSV = new float[] 
    // 1   2   3   4   5   6   7   8   9  10  11  12  13  14  15   lvl
    {100, 60, 60, 50, 50, 30, 30, 20, 25, 20, 20, 20, 20, 15, 15,  // Meatman
       0,  0,  0, 25, 25, 25, 25, 20, 15, 15, 15, 15, 15, 15, 15,  // Fastman
       0,  0,  0,  0,  0, 20, 20, 20, 20, 15, 15, 15, 15, 15, 15,  // Norman
       0,  0,  0,  0,  0,  0,  0,  0, 20, 15, 15, 15, 10, 10, 10,  // Bigman
       0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 15, 15, 10, 10, 10,  // Bowman
       0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0, 10, 10, 10,  // Iceman
       0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  5,  5,  // Strongman
       0, 40, 40, 25, 25, 25, 25, 20, 20, 20, 20, 20, 20, 20, 20}; // Tankman

    int Monster;
    //public GameObject[] Enemies;
    //public GameObject[] Spawners;

	// Use this for initialization
	void Start () 
    {
        //InvokeRepeating("Spawn", 0, 2); 
        //InfinitySpawnerStart();
	}

    
    int GetIDMostr()
    {
        int i = 0;

        while (true)
        {
            int Rand = Random.Range(1, 100);

            if (Rand <= MSV[(Variables.Level - 1) + i * 15])
            {
                return i;
            }

            if (++i >= 8)
            {                
                i = 0;
            }
        }
    }

     public IEnumerator Spawn() // Функция Спауна
    {
        if (Spawned == false)
        {
            Debug.Log("Вошли в нужную функцию");
            int Monster = GetIDMostr(); // Получаем нашего спаунящегося монстра

            Spawned = true;
            yield return new WaitForSeconds(Delay);
            int SpawnIndex = UnityEngine.Random.Range(0, Spawners.Length);
            // 
            Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[Monster]);     //Передаём респауну, кого спаунить       
            Spawned = false;
        }
    }

    /*
    void Spawn()
    {
        int Monster = GetIDMostr();

        Spawners[0].GetComponent<SpawnTut>().Spawn(Enemies[Monster]);
    }
     * */
     
}
