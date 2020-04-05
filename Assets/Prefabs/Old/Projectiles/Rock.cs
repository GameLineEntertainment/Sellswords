using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public FallingRock_Rotator FR;
    public GameObject PhysicsObjects;

    private bool isCollided = false;
    Transform t;

    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.gameObject.name);
        if (!isCollided)
        {
            isCollided = true;

                MiniEnemyAI go = collision.gameObject.GetComponent<MiniEnemyAI>();  
            
            if (FR.Killed == false )
            {
                if (go != null)
                {
                    Kill(go);
                }

                else
                    go = collision.gameObject.GetComponentInParent<MiniEnemyAI>();

                if(go != null)
                    Kill(go);


            }

            FR.Killed = true;
            PhysicsObjects.SetActive(true);
            var mesh = GetComponent<MeshRenderer>();
            if (mesh != null)
                mesh.enabled = false;
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;

        }
    }

    void Kill(EnemyAI go)
    {
        FR.Killed = go.Health(FR.Settings.Damage, FR.gameObject.tag);
        gameObject.layer = 12; // Это может породить некоторы баги, так что тут смотри внимательнее! Если ты залез искать шибку, вот она!
        //FR.Killed = true;
        Destroy(FR, FR.Settings.LifeTime);
    }

    void OnEnable()
    {
        isCollided = false;
        PhysicsObjects.SetActive(false);
        var mesh = GetComponent<MeshRenderer>();
        if (mesh != null)
            mesh.enabled = true;
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }
}
