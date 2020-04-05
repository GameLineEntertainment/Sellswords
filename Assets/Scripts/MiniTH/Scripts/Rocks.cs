using UnityEngine;
using System.Collections;

public class Rocks : MiniArrow 
{
    //public float Damage;
    public float Height;
    //public GameObject Target;
    public Rigidbody MyRG;
    FixedJoint joint;
    public Rigidbody MyBody;

    public bool KillHimLegolas = true;
    // public GameObject Bullet;

    //debug

    //public GameObject Projectile;
    //public bool Spawn = false;
    // public bool debug = false;
    Vector3 EnemyPos;
   // bool Killed = false;

	// Use this for initialization
    public void Start()
    {
        float num = speed;
        speed = 0;
        MiniArrowStart();
        speed = num;

        RocksStart();
    }


    public void Update()
    {
        RocksUpdate();
    }

    public void RocksStart()
    {
        //Target = GameObject.FindGameObjectWithTag("Left_Enemy"); // ГАВНОКОД, Target есть у mini arrow можно взять за основу.
        if (KillHimLegolas)
            transform.position = target.transform.position + transform.up * Height;

        else
            MyRG.AddForce(transform.forward * speed, ForceMode.Impulse);
    }


    // Update is called once per frame
    public void RocksUpdate() 
    {
        if (target != null)
        {
            if (KillHimLegolas)
            {
                EnemyPos = target.transform.position;
                EnemyPos.y = transform.position.y;
                transform.position = EnemyPos;
                //transform.forward = Target.transform.forward;
                // transform.right = Target.transform.right;
                //transform.up = Target.transform.up;
            }
        }
	}

    public override void ArrowFixedUpdate()
    {
        if (KillHimLegolas)
            MyTransform.position += transform.forward * speed * Time.deltaTime;

        else
            return;
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

