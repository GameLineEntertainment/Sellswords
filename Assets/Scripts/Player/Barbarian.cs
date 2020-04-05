using UnityEngine;
using System.Collections;

public class Barbarian : Character 
{

	override public IEnumerator Attack()     // Ф-ция атаки
    {
        ButtonChange.Attack = false;
        if (!CoulDown && CanAttack)  // проверка на кулдау
        {
            CoulDown = true; //включаем кулдаун

            yield return new WaitForSeconds(0); //считаем время

            GetComponentInChildren<MyAnim>().Attack(true); //Запускаем анимацию атаки
            GameObject bullet = Instantiate(Projectile) as GameObject;  //делаем класс пули

            bullet.transform.position = AttackPoint.position;        // выплёвываем пулют из нашей точки

            CoulDown = false;
            //CanAttack = false;
        }
    }
}
