using UnityEngine;
using System.Collections;

public class Alchemist : Character 
{
    public GameObject Projectile2;
    private GameObject Projectile;

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
            GameObject bullet2 = Instantiate(Projectile2) as GameObject;  //делаем класс пули

            bullet.transform.position = AttackPoint.position;        // выплёвываем пулют из нашей точки
            bullet2.transform.position = AttackPoint.position;        // выплёвываем пулют из нашей точки

            CoulDown = false;
            //CanAttack = false;
        }
    }
}
