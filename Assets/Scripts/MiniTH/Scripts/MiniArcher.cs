using UnityEngine;
using System.Collections;
using OldSellswords;

public class MiniArcher : Archer 
{
    public GameObject SpawnScript;
    public GameObject[][] Girls;
    public Mini_Artificial_Director MID;
     
	// Use this for initialization
	void Start ()
    {
        SpawnScript = GameObject.FindGameObjectWithTag("MiniRespawner");
        if (SpawnScript != null)
        {
            MID = SpawnScript.GetComponent<Mini_Artificial_Director>();

            //CharsStart();

            InvokeRepeating("CheckGirls", 1, 0.5f);
        }
	}

    // УДАЛИТЬ НАХУЙ и перенести в Character !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //override public IEnumerator Attack()     // Ф-ция атаки
    //{
    //    ButtonChange.Attack = false;
    //    //if (!CoulDown && Enemy != null) // проверка на кулдаун и наличие цели
    //    if (!CoulDown && CanAttack)  // проверка на кулдау
    //    {
    //        CoulDown = true; //включаем кулдаун
    //        //В общем тут блядская хуйня в парент классе, которая уже подождала, надо типа исправить
    //        yield return new WaitForSeconds(CoulDownTime); //считаем время
           
    //        if (Index != 2)
    //        {
    //            CoulDown = false;
    //            yield break;
    //        }

    //        GetComponentInChildren<MyAnim>().Attack(true); //Запускаем анимацию атаки

    //        int Num = Random.Range(0, Bullet.Length);
    //        Progectile Bull = Instantiate(Bullet[Num].Prjectile_Object) as Progectile;
    //        //BulletSettings BullShit = Bull.GetComponent<Progectile>().Settings;

    //        Bull.Settings = Bullet[Num];
    //        Bull.Target = Enemy;
    //        print(Enemy);
    //        Bull.transform.position = AttackPoint.position;        // выплёвываем пулют из нашей точки
    //        Bull.ParentChar = myTransform;

    //       // print(BullShit.LifeTime);

    //        Bull.ProgectileStart(); // Запуск логики снаряда

    //        // GameObject bullet = Instantiate(Projectile) as GameObject;  //делаем класс пули
    //        //  bullet.GetComponent<Progectile>().Settings.Target = Enemy;
    //        // bullet.transform.position = AttackPoint.position;        // выплёвываем пулют из нашей точки

    //        CoulDown = false;
    //        //CanAttack = false;

    //    }
    //}

    void CheckGirls() // Записываем, кто где из девочек
    {       
        if (index == 2 && Bullet[0].TypeOfDamage == ColorGroup.Red)
        {
            MID.IsGreen = false;
            MID.IsBlue = false;
            MID.IsRed = true;
        }

        if (index == 2 && Bullet[0].TypeOfDamage == ColorGroup.Green)
        {
            MID.IsGreen = true;
            MID.IsBlue = false;
            MID.IsRed = false;            
        }

        if (index == 2 && Bullet[0].TypeOfDamage == ColorGroup.Blue)
        {
            MID.IsGreen = false;
            MID.IsBlue = true;
            MID.IsRed = false;
        }

    }
	

}
