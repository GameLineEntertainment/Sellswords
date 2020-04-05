using UnityEngine;
using System.Collections;

public class RagOn : MonoBehaviour 
{
    public float TimeDeadBody = 10;
    bool ЩаПридумаем = false;
    bool counted = false;
    Vector3 StartPos;

	// Use this for initialization
	void Start () 
    {	
	}
	
	// Update is called once per frame
	void Update () 
    {
       /* if (ЩаПридумаем == true)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.position = StartPos;                
        }
        */
	}

    /// <summary>
    /// Включение Гравитации и рэгдола
    /// </summary>
    public void TriggerOn()
    {
        GetComponent<Collider>().enabled = false;

        Collider Col = gameObject.GetComponent<Collider>();
        Col.enabled = false;

        gameObject.tag = "Untagged";

        RagDoll Dis = GetComponentInChildren<RagDoll>();
        Dis.KnockedOut = true;

        EnemyAI Off = GetComponent<EnemyAI>();
        Off.enabled = false;

        Rigidbody OffKinem = GetComponent<Rigidbody>();
        OffKinem.isKinematic = true;

        Rigidbody[] special = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody Rig in special)
        {
            Rig.useGravity = true;
            Rig.GetComponent<Collider>().enabled = true;
        }        
             
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().freezeRotation = false;
       
        Col.enabled = false;

        System.GC.Collect(); // немедленная сборка мусора
        Destroy(gameObject, TimeDeadBody);
    }


    /// <summary>
    /// Выключение Гравитации и включение рэгдола
    /// </summary>
    public void TriggerOn(bool AntiGravity)
    {
        GetComponent<Collider>().enabled = false;

        Collider Col = gameObject.GetComponent<Collider>();
        Col.enabled = false;

        gameObject.tag = "Untagged";

        RagDoll Dis = GetComponentInChildren<RagDoll>();
        Dis.KnockedOut = true;

        EnemyAI Off = GetComponent<EnemyAI>();
        Off.enabled = false;

        Rigidbody OffKinem = GetComponent<Rigidbody>();
        OffKinem.isKinematic = true;

        Rigidbody[] special = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody Rig in special)
        {
            Rig.useGravity = false;
            Rig.GetComponent<Collider>().enabled = true;
        }

        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().freezeRotation = false;

        Col.enabled = false;

        Destroy(gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "MiniGod" || other.name == "NullZone")
        {
            TriggerOn();
            ЩаПридумаем = true;
            StartPos = transform.position;
            Count();
        }
    }

    void Count()
    {
        if (!counted)
        {
            counted = true;
            Counter.Falls++;
        }
    }
}
