using UnityEngine;
using System.Collections;

public class MiniKnuckle : MiniArrow 
{
    //public float Speed;
    //public float Damage;
    public float Distance;
   // public GameObject Target;

    public bool KillHimLegolas = true;
   // public GameObject Bullet;

    //debug

   // public GameObject Projectile;
   // public bool Spawn = false;
  //  public bool debug = false;
    Vector3 EnemyPos;
    //bool Killed = false;

    // Use this for initialization
    void Start()
    {
        MiniArrowStart();

        MiniKnuckleStart();
    }

    void Update()
    {
        MiniKnuckleUpdate();
    }

    void MiniKnuckleStart()
    {
       // Target = GameObject.FindGameObjectWithTag("Left_Enemy");


       // if (KillHimLegolas)
          //  Destroy(gameObject, 2);

        transform.position = target.transform.position + transform.right * Distance;
        transform.LookAt(target.transform);
    }

    // Update is called once per frame
    void MiniKnuckleUpdate()
    {
        if (target != null)
        {
            if (KillHimLegolas)
            {
                EnemyPos = transform.position;
               // EnemyPos = Target.transform.position;
                EnemyPos.z = target.transform.position.z;
                transform.position = EnemyPos;

                transform.position += transform.forward * speed * Time.deltaTime;
                //transform.forward = Target.transform.forward;
                // transform.right = Target.transform.right;
                //transform.up = Target.transform.up;
            }
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
