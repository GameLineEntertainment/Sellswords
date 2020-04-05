using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Mage : Character 
{   
    override public IEnumerator Attack()     // Ф-ция атаки
    {
        ButtonChange.Attack = false;
        //if (!CoulDown && Enemy != null) // проверка на кулдаун и наличие цели
        if (!CoulDown && CanAttack)  // проверка на кулдау
        {
            CoulDown = true; //включаем кулдаун

            yield return new WaitForSeconds(1); //считаем время

            GetComponentInChildren<MyAnim>().Attack(true); //Запускаем анимацию атаки
            GameObject bullet = Instantiate(Projectile) as GameObject;  //делаем класс пули

            bullet.transform.position = AttackPoint.position;        // выплёвываем пулют из нашей точки

            CoulDown = false;
            //CanAttack = false;
        }
    }   
}
