using UnityEngine;
using System.Collections;

public class MiniArrow : Arrow
{
    public enum TypeDamge { Red = 0, Green = 1, Blue = 2 }
    public TypeDamge TypeOfDamage = TypeDamge.Red;

    public bool Killed = false;
    public bool LookAtTarget = true;

    public GameObject DestroingPrefab;

    // Use this for initialization

     void Start()
    {
        MiniArrowStart();
     }


    public void MiniArrowStart()
    {
        Destroy(gameObject, LifeTime);
        MyTransform = transform;

        if (TypeOfDamage == TypeDamge.Red)// == ("MiniArrow1(Clone)"))
        {
            gameObject.tag = "DD";
            gameObject.layer = 16;
            CharObj = GameObject.Find("Char2(Clone)");
        }

        if (TypeOfDamage == TypeDamge.Green)
        {
            gameObject.tag = "AoE";
            gameObject.layer = 17;
            CharObj = GameObject.Find("Char0(Clone)");
        }

        if (TypeOfDamage == TypeDamge.Blue)
        {
            gameObject.tag = "SS";
            gameObject.layer = 18;
            CharObj = GameObject.Find("Char1(Clone)");
        }

        Character got = CharObj.GetComponent<Character>();
        Enemy_Tag = got.Tag;
        Character go = CharObj.GetComponent<Character>();
        target = go.Enemy;

        if(LookAtTarget)
        transform.LookAt(target.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void OnTriggerEnter(Collider other)
    {
        if (Killed == false)
        {
            MiniEnemyAI go = other.gameObject.GetComponentInParent<MiniEnemyAI>();

            if (go == null)
                return;

           // Engage(go);            

            bool Suc = go.Health(Damage, gameObject.tag);
            gameObject.layer = 12; // Это может породить некоторы баги, так что тут смотри внимательнее! Если ты залез искать шибку, вот она!
            Killed = true;

            if (!Suc)
	            if (DestroingPrefab != null)
		            Instantiate(DestroingPrefab, transform.position, Quaternion.identity);
        }
    }   
}













    #region Старое


/*
bool Killed = false;

// Use this for initialization
void Start()
{
    Destroy(gameObject, 3);
    MyTransform = transform;

    if (gameObject.name == ("MiniArrow1(Clone)"))
    CharObj = GameObject.Find("Char0(Clone)");

    if (gameObject.name == ("MiniArrow2(Clone)"))
        CharObj = GameObject.Find("Char1(Clone)");

    if (gameObject.name == ("MiniArrow3(Clone)"))
        CharObj = GameObject.Find("Char2(Clone)");

    Character got = CharObj.GetComponent<Character>();
    Enemy_Tag = got.Tag;
    Character go = CharObj.GetComponent<Character>();
    target = go.Enemy;

    transform.LookAt(target.transform);
    StartCoroutine(LifeCycle(LifeTime));
}

// Update is called once per frame
void Update()
{

}

override public void OnTriggerEnter(Collider other)
{
    if (Killed == false)
    {            
        MiniEnemyAI go = other.gameObject.GetComponent<MiniEnemyAI>();
        go.Health(Damage, gameObject.tag);
        Killed = true;
    }
}
 */

#endregion

