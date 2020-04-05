using UnityEngine;
using System.Collections;

public class FallingSword : MiniArrow 
{
    //public float Damage;
    public float Height;
    //public GameObject Target;
    FixedJoint joint;
    public Rigidbody MyBody;
    
    // public GameObject Bullet;

    //debug

    //public GameObject Projectile;
    //public bool Spawn = false;
    // public bool debug = false;
    Vector3 EnemyPos;
   // bool Killed = false;

	// Use this for initialization
    void Start()
    {
        MiniArrowStart();
        SwordStart();
    }


    void Update()
    {
        SwordUpdate();
    }

    void SwordStart()
    {
        //Target = GameObject.FindGameObjectWithTag("Left_Enemy"); // ГАВНОКОД, Target есть у mini arrow можно взять за основу.
            transform.position = target.transform.position + transform.up * Height;
    }

	
	// Update is called once per frame
    void SwordUpdate() 
    {
        if (target != null)
        {
                EnemyPos = target.transform.position;
                EnemyPos.y = transform.position.y;
                transform.position = EnemyPos;
                //transform.forward = Target.transform.forward;
                // transform.right = Target.transform.right;
                //transform.up = Target.transform.up;
        }
	}

    override public void OnTriggerEnter(Collider other)
    {
        MiniEnemyAI go = other.gameObject.GetComponent<MiniEnemyAI>();
        if (go != null)
            go.Health(Damage);

        if (other.gameObject.layer == 12)
        {
            if (joint == null)
            {
                joint = other.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = MyBody;
                joint.enablePreprocessing = false;
            }
            MyBody.isKinematic = true;
        }
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if (!Killed)
        {
           
            MiniEnemyAI go = other.gameObject.GetComponent<MiniEnemyAI>();
            if (go != null)
            {
                go.Health(Damage);
                Killed = true;
                gameObject.layer = 12;
            }
        }
    }
     */
}

