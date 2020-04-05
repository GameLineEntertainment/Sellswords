using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OldSellswords;

[System.Serializable]
public class BulletSettings
{
    public string Name = "Projectile";
    // Тип атаки
    
    public ColorGroup TypeOfDamage = ColorGroup.Red;
    // сторона атаки
    public enum SideDamge { Up = 0, Down = 1, Forward = 2, Back = 3, Left = 4, Right = 5 }
    public SideDamge SideOfDamage = SideDamge.Up;
    //Место Атаки    
    public bool FromHand = true, FollowToEnemy, LookAtTarget = false;

    public int Damage; // Урон
    public float Speed, RotationSpeed, LifeTime; // Скорость и время жизни

    public float DistanceTillEnemy; // Дистанция до врага (для рейкаста (*Клим))
    [Tooltip("Убиваем через триггер или рейкастом?")]
    public bool IsKillbyRay; 

    public GameObject Effect, NotDamageEffect; // Эффект и Эффект по неэффективному противнику

    [Tooltip("Стартовая позиция от точки спауна и стартовый поворот")]
    public Vector3 Spawn_Offset, StartRotation; // отступ от точки спауна    

    //public GameObject SpawnPlace;  // Точка создания    

    public Progectile Prjectile_Object;
}
