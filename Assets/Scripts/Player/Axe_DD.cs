using UnityEngine;
using System.Collections;

public class Axe_DD : MonoBehaviour 
{

    public float speed = 4;
    private Transform MyTransform;
    public GameObject target;
    public GameObject CharObj;
    public string Enemy_Tag;
    public int Damage;
    public float LifeTime = 3;
    public bool CanDamage = false;


    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, LifeTime);
        MyTransform = transform;
        CharObj = GameObject.Find("Char2(Clone)");

        Character got = CharObj.GetComponent<Character>();
        Enemy_Tag = got.Tag;

        if (got.index == 0)
            Fly(1);
        if (got.index == 1)
            Fly(2);
        if (got.index == 2)
            Fly(3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MyTransform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Fly(int point)
    {
        if (point == 1)
        {
            target = GameObject.Find("Side1");
        }

        if (point == 2)
        {
            target = GameObject.Find("Side2");
        }

        if (point == 3)
        {
            target = GameObject.Find("Side3");
        }

        transform.LookAt(target.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Пересёк чёт");
        if (other.gameObject.tag == Enemy_Tag)
        {
            EnemyAI go = other.gameObject.GetComponent<EnemyAI>();
            go.Health(Damage, gameObject.tag);
            Debug.Log("Пересёк ёбу");
            Debug.Log("Отправил " + gameObject.tag);
        }           
    }
}
