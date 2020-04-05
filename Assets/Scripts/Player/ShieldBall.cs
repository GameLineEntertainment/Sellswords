using UnityEngine;
using System.Collections;

public class ShieldBall : MonoBehaviour 
{
    public float speed = 4;
    private Transform MyTransform;
    public GameObject target;
    public GameObject Character;
    public string Enemy_Tag;
	public int Damage;
    public float LifeTime = 3;
    public bool CanDamage = false;


	// Use this for initialization
    void Start()
    {
        Destroy(gameObject, LifeTime);
        MyTransform = transform;
        Character = GameObject.Find("Char2");

        Warrior got = Character.GetComponent<Warrior>();
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
	void Update () 
    {
        MyTransform.position += transform.forward * speed * Time.deltaTime; 
   	}

    private void Fly(int point)
    {
        if(point == 1)
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

	/*  void OnTriggerEnter(Collider other)
    {
        MonoBehaviour scriptAI = other.gameObject.GetComponent<EnemyAnimated>();
        scriptAI.enabled = false;

        Animator scriptAnim = other.gameObject.GetComponent<Animator>();
        scriptAnim.enabled = false;

        Rigidbody ScriptRigid = other.gameObject.GetComponent<Rigidbody>();
        ScriptRigid.freezeRotation = false;

        RagDoll got = other.gameObject.GetComponent<RagDoll>();
        got.KnockedOut = true;

        SphereCollider Remove = other.gameObject.GetComponent<SphereCollider>();
        DestroyObject(Remove);

		//Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
		//DestroyObject (rigid);
    }*/


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Enemy_Tag)
        {
            EnemyAI go = other.gameObject.GetComponent<EnemyAI>();
            go.Health(Damage, gameObject.tag);
        }
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Enemy_Tag && CanDamage)
        {
            EnemyAI go = other.gameObject.GetComponent<EnemyAI>();
            //go.NonDamaging = false;
            go.Health(Damage, "DD");

            
            //Destroy(other.gameObject);
            //Destroy(gameObject);            
        }

        if (other.gameObject.tag == Enemy_Tag)
        {
            Debug.Log("Тиггер сработал2 ");
            EnemyAI Off = other.gameObject.GetComponent<EnemyAI>();
            Off.Move = false;
        }
    }
     * */
}
