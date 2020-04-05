using UnityEngine;
using System.Collections;

public class KillZ : MonoBehaviour 
{
    public bool IsKillZ;

	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
	void Update () 
    {	
	}

    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.tag == "Down_Enemy")        
            Counter.Falls++;

        if (other.gameObject.tag == "Left_Enemy")
            Counter.Falls++;

        if (other.gameObject.tag == "Right_Enemy")
            Counter.Falls++;
         * */

        if (IsKillZ)
        {
            other.gameObject.GetComponent<MiniEnemyAI>().Health(3000);
            //Destroy(other.gameObject);
           // other.gameObject.tag = "Untagged";   
            //Destroy(gameObject);
           // Counter.Falls++;
        }

        if (!IsKillZ)
        {
            /*MonoBehaviour scriptAI = other.gameObject.GetComponent<EnemyAnimated>();
            scriptAI.enabled = false;

            Animator scriptAnim = other.gameObject.GetComponent<Animator>();
            scriptAnim.enabled = false;

            Rigidbody ScriptRigid = other.gameObject.GetComponent<Rigidbody>();
            ScriptRigid.freezeRotation = false;

            RagDoll got = GetComponent<RagDoll>();
            got.KnockedOut = true;*/

            other.gameObject.tag = "Untagged";
        }        
    }
}
