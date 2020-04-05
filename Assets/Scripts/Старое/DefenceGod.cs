using UnityEngine;
using System.Collections;

public class DefenceGod : MonoBehaviour
{
    public static bool GodSave;
    public float Range = 1;
    public float speed, ContactRadius;
    public bool start;

	// Use this for initialization
	void Start ()     
    {
        GetComponent<Collider>().contactOffset = ContactRadius;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (GodSave)
        {
            transform.localScale += new Vector3(speed, speed, speed) * Time.deltaTime;
            if (transform.localScale.magnitude > Range)
            {                
                transform.localScale = new Vector3(1, 1, 1);
                GodSave = false;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject GO = other.gameObject;

        if (GO.layer == 13 || GO.layer == 14 || GO.layer == 15)
        {
            other.gameObject.GetComponent<MiniEnemyAI>().Die();
        }
    }   
}
