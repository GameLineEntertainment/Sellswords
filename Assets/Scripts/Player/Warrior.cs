using UnityEngine;
using System.Collections;

public class Warrior : Character 
{ 
    public GameObject Damage;    
    public GameObject Push;
    


    override public IEnumerator Attack()     // Ф-ция атаки
    {
        ButtonChange.Attack = false;
        if (!CoulDown) // проверка на кулдаун
        {            
            CoulDown = true; //включаем кулдаун

            yield return new WaitForSeconds(1); //считаем время

            GetComponentInChildren<MyAnim>().Attack(true); //Запускаем анимацию атаки
            AttackPoint = GameObject.Find("StartShieldWave").transform;

            GameObject bullet = Instantiate(Damage) as GameObject;  //делаем класс пули
            bullet.transform.position = AttackPoint.position;        // выплёвываем пулют из нашей точки

            GameObject bullet2 = Instantiate(Push) as GameObject;  //делаем класс пули       
            bullet2.transform.position = AttackPoint.position;     // выплёвываем пулют из нашей ещё одной точки    

            CoulDown = false;
            //CanAttack = false;
            
        }
    }
}
