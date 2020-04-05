using UnityEngine;
using System.Collections;

public class RFX4_ReplaceModelOnCollision : MonoBehaviour
{
    public FallingRock_Rotator FR;
    public GameObject PhysicsObjects;

    private bool isCollided = false;
    Transform t;


    private void OnCollisionEnter(Collision collision)
    {
        if (!isCollided)
        {
            isCollided = true;
            PhysicsObjects.SetActive(true);
            var mesh = GetComponent<MeshRenderer>();
            if (mesh != null)
                mesh.enabled = false;
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }

    void OnEnable()
    {
        isCollided = false;
        PhysicsObjects.SetActive(false);
        var mesh = GetComponent<MeshRenderer>();
        if (mesh!=null)
            mesh.enabled = true;
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;

        switch ((int)FR.Settings.TypeOfDamage)
        {
            case 0: // Red
                {
                    gameObject.tag = "DD";
                    gameObject.layer = 16;
                    break;
                }

            case 1: // Green
                {
                    gameObject.tag = "AoE";
                    gameObject.layer = 17;
                    break;
                }

            case 2: // Blue
                {
                    gameObject.tag = "SS";
                    gameObject.layer = 18;
                    break;
                }
        }
    }
}
