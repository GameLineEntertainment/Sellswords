using UnityEngine;
using System.Collections;

public class MiniSpears : MonoBehaviour
{
    [Header("Тут эта херня влияет только на цвет")]
    public BulletSettings Settings;
    public Rigidbody MyBody;
    public Progectile MyProg;

    FixedJoint joint;

    private void Start()
    {
        Settings = MyProg.Settings;
        gameObject.layer = MyProg.gameObject.layer;

        switch ((int)Settings.TypeOfDamage)
        {
            case 0: // Red
                {
                    gameObject.tag = "DD";                   
                    break;
                }

            case 1: // Green
                {
                    gameObject.tag = "AoE";                    
                    break;
                }

            case 2: // Blue
                {
                    gameObject.tag = "SS";                    
                    break;
                }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        MiniEnemyAI go = other.gameObject.GetComponent<MiniEnemyAI>();

        if (go != null)
        {
            go.Health(MyProg.Settings.Damage, gameObject.tag);
        }

        if (other.gameObject.layer == 12)
        {
            if (joint == null)
            {
                MyProg.Killed = true;
                joint = other.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = MyBody;
                joint.enablePreprocessing = false;
            }
        }
    }
}
