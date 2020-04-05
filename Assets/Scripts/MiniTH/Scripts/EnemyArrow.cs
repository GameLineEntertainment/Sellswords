using UnityEngine;
using System.Collections;

public class EnemyArrow : MonoBehaviour 
{
    [HideInInspector]
    public GameObject Target;
    public float Speed;
    [HideInInspector]
    public int Damage;
    public float LifeTime;

    Transform MyTransform;

	// Use this for initialization
	void Start () 
    {
        MyTransform = transform;
        Destroy(gameObject, LifeTime);

        transform.LookAt(Target.transform);
	}
	
	// Update is called once per frame
	void Update () 
    {
        MyTransform.position += transform.forward * Speed * Time.deltaTime;
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Target.tag)
        {
            GameLevel HP = GameObject.Find("Scripts").GetComponent<GameLevel>(); //Общие жизни
            HP.Health(Damage);

            Destroy(gameObject);
        }
    }
}
