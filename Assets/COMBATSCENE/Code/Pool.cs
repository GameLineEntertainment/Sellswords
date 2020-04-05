using UnityEngine;

namespace CombatScene
{
    public struct Pool 
    {
        public GameObject[] Array { get; private set; }

        public GameObject[] StartPool(dynamic obj, int size, Vector3 pos, Quaternion rot, int maxRandomRange) // dynamic для разных объектов пулы
        {
            Array = new GameObject[size];
            for (int i = 0; i < size; i++)
            {
                if (maxRandomRange > 1)
                {
                    Array[i] = Object.Instantiate(obj[Random.Range(0, maxRandomRange)], pos, rot);
                }
                else
                {
                    Array[i] = Object.Instantiate(obj, pos, rot);
                }
            }

            return Array;
        }
    }
}
