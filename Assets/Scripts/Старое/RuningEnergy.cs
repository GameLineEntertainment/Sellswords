using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuningEnergy : MonoBehaviour
{
 //   [System.Serializable]
 //   public struct CharSpeed
 //   {
 //       [SerializeField]
 //       string Name;
 //       public float MoveSpeed, CastSpeed;
 //   }

 //   [SerializeField]
 //   CharSpeed[] Speed = new CharSpeed [3];

 //   [SerializeField]
 //   Image SpriteEnergy;

 //   [SerializeField]
 //   [Range(0, 10)]
 //   float CurrentEnergy;

 //   const float MaxEnergy = 10;

 //   [SerializeField]
 //   float RestoreSpeed, TurnCost, CooldownTime;

 //   [SerializeField]
 //   bool IsRest = false, CDprogress; // Отдыхаем ли в данный момент и запущена ли система ожидания отдыха

 //   [SerializeField]
 //   GameLevel GM;

 //   Mini_Artificial_Director ArtDir; // Ссылка на режиссёра

 //   int CurState; // 0 = Всё ок, 1, середина, 2 тяжело, 3 пиздец

 //   private void Start()
 //   {
 //       // SpriteEnergy = transform.GetChild(0).GetComponentInChildren<Image>();
 //       // GM.GetComponentInChildren<GameLevel>();
 //       ArtDir = FindObjectOfType<Mini_Artificial_Director>();
 //   }

 //   public void DecreaseEnergy()
 //   {
 //       if (IsRest == true)
 //       {
 //           IsRest = false;
 //           CurrentEnergy -= TurnCost;         // отнимаем силы    
 //           if (CurrentEnergy < 0)
 //               CurrentEnergy = 0;
 //           return;
 //       }

 //       CancelInvoke("Cooldown");
 //       Invoke("Cooldown", CooldownTime);
 //   }
	
	//// Update is called once per frame
	//void Update ()
 //   {
 //       SpriteEnergy.fillAmount = CurrentEnergy / 10;

 //       if (CurrentEnergy > 7 && CurState != 0)
 //       {
 //           SpriteEnergy.color = Color.green;

 //           ArtDir.AntiCheat = false;
 //           CurState = 0;
 //           ChangeSpeed();
 //       }

 //       else if (CurrentEnergy <= 7 && CurrentEnergy > 3 && CurState != 1)
 //       {
 //           SpriteEnergy.color = Color.yellow;

 //           ArtDir.AntiCheat = false;
 //           CurState = 1;
 //           ChangeSpeed();
 //       }

 //       else if (CurrentEnergy <= 3 && CurState != 2)
 //       {
 //           SpriteEnergy.color = Color.red;

 //           CurState = 2;
           
 //           ChangeSpeed();
 //       }

 //       else if(CurrentEnergy == 0 && CurState != 3)
 //       {
 //           CurState = 3;

 //           ArtDir.AntiCheat = true;
 //           ChangeSpeed();
 //       }

 //       if (IsRest && CurrentEnergy < MaxEnergy && GM.Characters[0].CoolDown != true)
 //       {
 //           CurrentEnergy += RestoreSpeed * Time.deltaTime;

 //           if (CurrentEnergy > MaxEnergy)
 //               CurrentEnergy = MaxEnergy;
 //       }

 //       // если ещё на начали отдыхать начинаем систему отдыха
 //       if(!CDprogress && !GM.Characters[0].CoolDown && !IsRest)
 //       {
 //           CDprogress = true; // запущена система отдыха
 //           Invoke("Cooldown", CooldownTime);
 //       }
 //   }

 //   void ChangeSpeed()
 //   {
 //       for (int i = 0; i < GM.Characters.Length; i++)
 //       {
 //           GM.Characters[i].moveSpeed = (int)Speed[CurState].MoveSpeed;
 //           GM.Characters[i].CoulDownTime = Speed[CurState].CastSpeed;
 //       }
 //   }

 //   void Cooldown()
 //   {        
 //       IsRest = true;
 //       CDprogress = false;        // система отдыха отработала
 //   }      
}
