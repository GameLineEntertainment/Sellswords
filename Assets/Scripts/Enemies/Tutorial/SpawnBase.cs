using UnityEngine;
using System.Collections;

public class SpawnBase : MonoBehaviour 
{
    public GameObject[] Enemies;       //Количество противников
    public GameObject EnemyAlive;
    public GameObject[] Spawners;
    public string Tag;
    public int[] count;                //Их число

    public GameObject[] Фразочки;   

    //Переменные для проверки остались враги на сцене
    [HideInInspector]
    public GameObject Left, Right, Down;


    //private int EnemyCounter;        //Считаем количество врагов
    public int BaseCount;              //Переменная о количестве врагов   
    public int alive = 0;
    public int проход = 0;
    private bool tutor = false;

    public bool NormanTutor = false;
    public bool BigmanTutor = false;
    //private Animator anim;

    // Use this for initialization
    void Start()
    {        
        StartCoroutine(Spawn(alive));
        Tag = Enemies[alive].gameObject.tag;
        tutor = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BaseCount; i++)
        {
            if (count[i] <= 0)
            {
                Debug.Log("Отменяю спаун");
                CancelInvoke("Spawn");
            }
        }

        CheckAlive();
       
    }


    IEnumerator Spawn(int i)
    {
        int SpawnIndex = Random.Range(0, Spawners.Length);

        if (BigmanTutor)
        {
            проход++;
            //anim = Фразочки[проход - 1].GetComponent<Animator>();

            if (проход == 1)
            {
                Spawners[2].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                count[0]--;
                SpawnIndex = 0;
                Spawners[1].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                count[0]--;

                Фразочки[проход - 1].SetActive(true);
                Фразочки[проход - 1].GetComponent<Animator>().SetBool("Active", true);

                yield return new WaitForSeconds(3f);
                GameOver.GamePaused = true;
                Time.timeScale = 0;

                yield return new WaitForSeconds(0.5f);
                Фразочки[проход - 1].GetComponent<Animator>().SetBool("Active", false);
                yield return new WaitForSeconds(0.5f);
                Фразочки[проход - 1].SetActive(false);
            }

            if (проход == 2)
            {
                Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                count[0]--;
                SpawnIndex = Random.Range(0, Spawners.Length);
                SpawnIndex = 0;
                Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                count[0]--;
            }

            if (проход == 3)
            {
                Spawners[2].GetComponent<SpawnTut>().Spawn(Enemies[1]);
                count[0]--;

                Фразочки[1].SetActive(true);
                Фразочки[1].GetComponent<Animator>().SetBool("Active", true);

                yield return new WaitForSeconds(4.5f);
                GameOver.GamePaused = true;
                Time.timeScale = 0;

                yield return new WaitForSeconds(0.5f);
                Фразочки[1].GetComponent<Animator>().SetBool("Active", false);
                yield return new WaitForSeconds(0.5f);
                Фразочки[1].SetActive(false);
            }

            if (проход == 4)
            {
                Spawners[0].GetComponent<SpawnTut>().Spawn(Enemies[1]);
                count[0]--;
            }


            /*
                if (проход == 6)
                {
                    Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[1]);
                    count[1]--;
                }

                if (проход == 7)
                {
                    Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[2]);
                    count[2]--;
                }

                if (проход == 8)
                {
                    Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[3]);
                    count[3]--;
                } 
             */                

                if (проход > 5)
                {
                    StartCoroutine(TimeChecker());
                }
            

            #region Туториал Норманы
            if (NormanTutor)
            {
                проход++;
                //int SpawnIndex = Random.Range(0, Spawners.Length);
                //anim = Фразочки[проход - 1].GetComponent<Animator>();

                if (проход == 1)
                {
                    Spawners[2].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                    count[0]--;
                    Фразочки[проход - 1].SetActive(true);
                    Фразочки[проход - 1].GetComponent<Animator>().SetBool("Active", true);
                }

                if (проход == 2)
                {
                    Spawners[0].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                    count[0]--;
                }

                if (проход == 3)
                {
                    Spawners[0].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                    count[0]--;
                }

                if (проход == 4)
                {
                    SpawnIndex = 0;
                    Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                    count[0]--;

                    SpawnIndex = 2;
                    Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                    count[0]--;
                }

                if (проход == 5)
                {
                    SpawnIndex = 0;
                    Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                    count[0]--;

                    SpawnIndex = 1;
                    Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                    count[0]--;

                    SpawnIndex = 2;
                    Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[0]);
                    count[0]--;
                }


                /*
                    if (проход == 6)
                    {
                        Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[1]);
                        count[1]--;
                    }

                    if (проход == 7)
                    {
                        Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[2]);
                        count[2]--;
                    }

                    if (проход == 8)
                    {
                        Spawners[SpawnIndex].GetComponent<SpawnTut>().Spawn(Enemies[3]);
                        count[3]--;
                    } 
                 */

                if (проход <= 1)
                {
                    yield return new WaitForSeconds(3);

                    Фразочки[5].SetActive(true);
                    GameOver.GamePaused = true;
                    Time.timeScale = 0;

                    if (!tutor)
                    {
                        Фразочки[4].SetActive(true);
                        Фразочки[проход - 1].GetComponent<Animator>().SetBool("Active", false);
                        yield return new WaitForSeconds(0.5f);
                        Фразочки[4].SetActive(false);
                        Фразочки[проход - 1].SetActive(false);

                        Фразочки[проход].SetActive(true);
                        Фразочки[проход].GetComponent<Animator>().SetBool("Active", true);
                        yield return new WaitForSeconds(0.5f);
                        Time.timeScale = 0;

                        Фразочки[проход].GetComponent<Animator>().SetBool("Active", false);
                        yield return new WaitForSeconds(0.5f);
                        Фразочки[проход].SetActive(false);

                        Фразочки[проход + 1].SetActive(true);
                        Фразочки[проход + 1].GetComponent<Animator>().SetBool("Active", true);
                        yield return new WaitForSeconds(0.5f);
                        Time.timeScale = 0;
                        Фразочки[проход + 1].GetComponent<Animator>().SetBool("Active", false);
                        yield return new WaitForSeconds(0.5f);
                        Фразочки[проход + 1].SetActive(false);


                        Фразочки[6].SetActive(true);
                        Фразочки[7].SetActive(true);
                        Фразочки[7].GetComponent<Animator>().SetBool("Active", true);
                        yield return new WaitForSeconds(0.5f);
                        Time.timeScale = 0;

                        yield return new WaitForSeconds(2);
                        Фразочки[7].GetComponent<Animator>().SetBool("Active", false);

                        yield return new WaitForSeconds(0.5f);
                        Фразочки[7].SetActive(false);

                        tutor = true;
                    }
                }

                if (проход > 4)
                {
                    StartCoroutine(TimeChecker());
                    Фразочки[проход - 1].GetComponent<Animator>().SetBool("Active", false);
                }
            }
            #endregion
        }
    }

      void CheckAlive()
      {

          Left = GameObject.FindGameObjectWithTag("Left_Enemy");
          Right = GameObject.FindGameObjectWithTag("Right_Enemy");
          Down = GameObject.FindGameObjectWithTag("Down_Enemy");

          if (Left == null && Right == null && Down == null)
          {
              StartCoroutine(Spawn(alive));
              alive++;
          }

          /*
          EnemyAlive = GameObject.FindGameObjectWithTag("Left_Enemy");
          if (EnemyAlive == null)
          {
              StartCoroutine(Spawn(alive));
              alive++;
              
              if (EnemyAlive == null)
              {
                  EnemyAlive = GameObject.FindGameObjectWithTag("Right_Enemy");
                  StartCoroutine(Spawn(alive));
                  alive++;

                  if (EnemyAlive == null)
                  {
                      EnemyAlive = GameObject.FindGameObjectWithTag("Down_Enemy");
                      if (EnemyAlive == null)
                      {
                          StartCoroutine(Spawn(alive));
                          alive++;

                          if (EnemyAlive == null)
                          {
                              StartCoroutine(TimeChecker());
                          }
                      }
                  }
              }
          }
           */ 
      }

    IEnumerator TimeChecker()
    {
        yield return new WaitForSeconds(6); //считаем время

        Left = GameObject.FindGameObjectWithTag("Left_Enemy");
        Right = GameObject.FindGameObjectWithTag("Right_Enemy");
        Down = GameObject.FindGameObjectWithTag("Down_Enemy");

        if (Left == null && Right == null && Down == null)
        {
            GameOver.Win = true;
            GameOver.OverGame = true;
        }
    }
}