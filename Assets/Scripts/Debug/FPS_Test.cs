using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Water;
using System.Collections;
using System;

public class FPS_Test : MonoBehaviour
{
    public GameObject OverLAbel;
    public Text txt, Over_FPS, Over_Text;

    public int[] MiddleFPS;
    public int GraphLevel;
    public string DeviceTXT;

    public Water SceneWater; 

    float fps;

    void Start()
    {
        StartFPS();
    }

    void Update()
    {
        fps = 1.0f / Time.deltaTime;      
    }

    void ShowFPS()
    {        
        txt.text = "FPS: " + Convert.ToInt32(fps);

        if (fps > 25)
            txt.color = Color.green;

        if (fps < 25)
            txt.color = Color.yellow;

        if (fps < 15)
            txt.color = Color.red;
    }

    public void StartFPS()
    {
        Time.timeScale = 1;

        OverLAbel.SetActive(false);

        MiddleFPS = new int[100];

        InvokeRepeating("ShowFPS", 0.5f, 0.5f);

        InvokeRepeating("WriteMiddle", 0.3f, 0.3f);
    }

    void WriteMiddle()
    {
        for (int i = 0; i < MiddleFPS.Length; i++)
        {
            if (MiddleFPS[i] == 0)
            {
                MiddleFPS[i] = Convert.ToInt32(fps);
                break;
            }

            if (i == MiddleFPS.Length - 1)
                StopThis();
        }
    }

    void StopThis()
    {
        Time.timeScale = 0;
        CancelInvoke();

        OverLAbel.SetActive(true);
        int Temp = 0;
        int Resoult = 0;

        //Складываем все элеметы для высчитывания среднего арифметического
        for (int i = 0; i < MiddleFPS.Length; i++)
            Temp += Convert.ToInt32(MiddleFPS[i]);

        Resoult = Temp / MiddleFPS.Length; // Высчитали среднее арифметическое

        MiddleFPS = null; // освободили массив, для освобождения памяти

        DeviceTXT = "";

        GraphLevel = QualitySettings.GetQualityLevel();

        if (Resoult >= 25 && GraphLevel == 0)
            DeviceTXT = "Сносный девайс";

        if (Resoult < 25 && GraphLevel == 0)
            DeviceTXT = "Выкинь этот телефон";

        if (Resoult < 30 && GraphLevel == 6)
            DeviceTXT = "Чёт не очень";

        if (Resoult >= 30 && GraphLevel == 6)
            DeviceTXT = "Годный девайс";

        Over_FPS.text = "Среднее значение FPS: " + Convert.ToInt32(fps);

        Over_Text.text = DeviceTXT;

        var tmp = new System.Object[1024]; // Выделяем системный массив

        for (int i = 0; i < 1024; i++)
            tmp[i] = new byte[1024]; // Заполняем массив 1024 байтами

        tmp = null; // Освобождаем массив (типа выделяем новую свободную память)

        System.GC.Collect(); // немедленная сборка мусора
    }

    public void GraphsQulity(int num)
    {
        QualitySettings.SetQualityLevel(num, true);
        ChangeGraphics(num);
    }

    public void Close()
    {
        Application.Quit();
    }

    void ChangeGraphics(int Num)
    {
        if (Num == 0)
        { 
            SceneWater.textureSize = 1;
            SceneWater.refractLayers = 0;
            SceneWater.refractLayers = 1;
        }

        if (Num == 6)
        {
            SceneWater.textureSize = 256;
            SceneWater.refractLayers = -1;
        }
    }

    /*
    public float Height, Height2;
    public GameObject Target;
    public GameObject Bullet;
    public GameObject StartProjectile;

    //debug

    public GameObject Projectile1, Projectile2, Projectile3, Projectile4;
    public bool Spawn = false;
    public float WeaponState = 0;
    public bool debug = false;

    public Mini_Artificial_Director Dir;
    public int Rnd;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Spawn)
        {
            if (WeaponState == 0)
                LounchRocks(Projectile1);

            else if (WeaponState == 1)
                LounchRocks(Projectile2);

            else if (WeaponState == 3)
                LounchRocks(Projectile3);
        }

        
            Rnd = Dir.GetIDMostr();
            //Rnd = 1;
        
    }

    public void LounchRocks(GameObject Projectile)
    {
        if (Spawn)
        {
            Spawn = false;

            Target = GameObject.FindGameObjectWithTag("Left_Enemy");

            Bullet = Instantiate(Projectile) as GameObject;


            if (WeaponState == 0)
                Bullet.transform.position = Target.transform.position + transform.up * Height;

            if (WeaponState == 1)
                Bullet.transform.position = Target.transform.position + transform.up * Height2;

            if (WeaponState == 2)
                Bullet.transform.position = StartProjectile.transform.position;

            if (WeaponState == 3)
                Bullet.transform.position = Target.transform.position + transform.right * Height;
        }
    }
     */
}