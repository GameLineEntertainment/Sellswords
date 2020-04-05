using UnityEngine;
using System.Collections;

public class Freeze : MonoBehaviour
{
    public float speed = 4;
    private Transform MyTransform;
    public GameObject target;
    public GameObject CharObj;
    public string Enemy_Tag;
	public bool CanDamage = false;
    public float Damage;
    public float LifeTime = 1;


    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, LifeTime);
        MyTransform = transform;
        CharObj = GameObject.Find("Char1(Clone)");

        Character got = CharObj.GetComponent<Character>();
        Enemy_Tag = got.Tag;
        //Warrior go = (Warrior)Character.GetComponent("Warrior");
        //target = go.Enemy;

        if (got.Tag == "Down_Enemy")
            Fly(1);
        if (got.Tag == "Right_Enemy")
            Fly(2);
        if (got.Tag == "Left_Enemy")
            Fly(3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MyTransform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Fly(int point)
    {
        if (point == 1)
        {
            target = GameObject.Find("Side1");
        }

        if (point == 2)
        {
            target = GameObject.Find("Side2");
        }

        if (point == 3)
        {
            target = GameObject.Find("Side3");
        }

        transform.LookAt(target.transform);
    }


    void OnTriggerEnter(Collider other)
    {
        EnemyAI gg = other.gameObject.GetComponent<EnemyAI>();

        if (!gg.SS_ignore)
        {
            if (other.gameObject.tag == Enemy_Tag)
            {
                //  EnemyAI gg = other.gameObject.GetComponent<EnemyAI>();
                gg.StartCoroutine(gg.FreezeUp());

                if (CanDamage)
                {
                    //go.NonDamaging = false;
                    gg.Health(Damage, "SS");

                    //Destroy(other.gameObject);
                    //Destroy(gameObject);            
                }
            }
        }
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        EnemyAI gg = other.gameObject.GetComponent<EnemyAI>();

        if (gg.CanFreezed)
        {
            if (other.gameObject.tag == Enemy_Tag)
            {
                //  EnemyAI gg = other.gameObject.GetComponent<EnemyAI>();
                gg.StartCoroutine(gg.FreezeUp());

                if (CanDamage)
                {
                    EnemyHealth go = other.gameObject.GetComponent<EnemyHealth>();
                    //go.NonDamaging = false;
                    go.Health(Damage);


                    //Destroy(other.gameObject);
                    //Destroy(gameObject);            
                }                
            }
        }
    }
    */
}
