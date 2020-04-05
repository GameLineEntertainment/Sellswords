using UnityEngine;
using System.Collections;

public class SpawnTut : MonoBehaviour 
{
    public bool Left, Right, Down;
    //public GameObject[] Enemies;       //Количество противников
	

    public void Spawn(GameObject Enemies)
    {
        if(Left)
            Enemies.gameObject.tag = "Left_Enemy";
        if (Right)
            Enemies.gameObject.tag = "Right_Enemy";
        if (Down)
            Enemies.gameObject.tag = "Down_Enemy";

        Instantiate(Enemies, transform.position, transform.rotation);
    }

    public void Spawn(MiniEnemyAI уnemies, float minSpeed, float maxSpeed)
    {
        if (Left)
            уnemies.gameObject.tag = "Left_Enemy";
        if (Right)
            уnemies.gameObject.tag = "Right_Enemy";
        if (Down)
            уnemies.gameObject.tag = "Down_Enemy";

        MiniEnemyAI En = Instantiate(уnemies, transform.position, transform.rotation);
        En.MinSpeed = minSpeed;
        En.MaxSpeed = maxSpeed;
        En.moveSpeed = Random.Range(En.MinSpeed, En.MaxSpeed); // Нахрена мобу ввобще знать свои пороги скорости? Бред!
        print("Скорость = " + En.moveSpeed + ", а должна быть = " + minSpeed);
    }
}