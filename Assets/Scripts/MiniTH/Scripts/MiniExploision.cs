using UnityEngine;
using System.Collections;

public class MiniExploision : MiniArrow 
{
    public float Range;    
    public float MaxSize;
    public float SpeedExpolision;
    public bool Exploision = false;

    Vector3 RangeVector;

	// Use this for initialization
    void Start()
    {
        MiniExploisionStart();
    }

    void Update()
    {
        MiniExploisionUpdate();
    }

    public void MiniExploisionStart() 
    {
        MiniArrowStart();

        RangeVector = new Vector3(Range, Range, Range);
       // target = GameObject.FindGameObjectWithTag("Left_Enemy").transform;
	}
	
	// Update is called once per frame
    public void MiniExploisionUpdate() 
    {
        if (Exploision && transform.localScale.y < MaxSize)
        {
            transform.localScale += RangeVector * SpeedExpolision * Time.deltaTime; 
        }

        if (transform.localScale.y >= MaxSize)
            Destroy(gameObject);
	}

    override public void OnTriggerEnter(Collider other)
    {
        MiniEnemyAI go = other.gameObject.GetComponent<MiniEnemyAI>();
        if (go != null)
        {
            if (Killed == false)
            {
                go.Health(Damage, gameObject.tag);
                speed = 0;

                Exploision = true;
                Killed = true;
            }
        }
    }
}
