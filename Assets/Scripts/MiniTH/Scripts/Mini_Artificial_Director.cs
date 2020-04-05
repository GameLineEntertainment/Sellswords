using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mini_Artificial_Director : InfinitySpawner 
{
   public bool IsGreen = false, IsBlue = false, IsRed;
    //public GameObject[] Enemies;
    //public GameObject[] Spawners;
   public int Monster = 0;
   //int i = 0;

    [SerializeField]
    private float _newMinSpeed = 4; //  Античитерская скорость
                                  // private float[] _newMinSpeed; //  Античитерская скорость
    [SerializeField]
    private float _maxSpeed = 8; // максимальная скорсоть противника
    public bool AntiCheat { get; set; }

    List <int> EnemyID;

	// SpawnTut[] Spawners; // Ссылка на спаунеры

	//// Use this for initialization
	//void Start () 
	//{
	//    InfinitySpawnerStart();
	//}


	//public int GetIDMostr()
	//{
	//    InitializeID();

	//    if (IsGreen)
	//     EnemyID.Remove(0); 
	//    if (IsBlue)
	//     EnemyID.Remove(1); 
	//    if (IsRed)
	//     EnemyID.Remove(2);

	//    int Num = EnemyID[Random.Range(0, EnemyID.Count)];

	//    return (Num);
	//}

	//void InitializeID()
	//{
	//    int i = 0;

	//    EnemyID = new List<int>();

	//    EnemyID.Add(i);
	//    i++;
	//    EnemyID.Add(i);
	//    i++;
	//    EnemyID.Add(i);
	//}

	//public IEnumerator Spawn() // Функция Спауна
	//{
	//	if (Spawned == false)
	//	{
	//		Spawned = true;
	//		yield return new WaitForSeconds(Delay);
	//		Monster = GetIDMostr(); // Получаем нашего спаунящегося монстра
	//		int SpawnIndex = UnityEngine.Random.Range(0, Spawners.Length);
	//		if (!AntiCheat)
	//			Spawners[SpawnIndex].Spawn(Enemies[Monster]);     //Передаём респауну, кого спаунить      
	//		else
	//			Spawners[SpawnIndex].Spawn(EnemyObject[Monster], _newMinSpeed, _maxSpeed);
	//		//Spawners[SpawnIndex].Spawn(EnemyObject[Monster], _newMinSpeed[SpawnIndex] - 2, _maxSpeed);     //Передаём респауну, кого спаунить      
	//		Spawned = false;
	//	}
	//}

}