using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    [HideInInspector]
    public string Enemy_Tag;
    public Transform MyTransform;

    public GameObject CharObj;
    public Transform target;
    public float speed = 4;
    public int Damage;
    public float LifeTime;

    // Use this for initialization
    void Start()
    {
        ArrowStart();
    }

    public void ArrowStart()
    {
        Destroy(gameObject, LifeTime);
        MyTransform = transform;
        CharObj = GameObject.Find("Char0(Clone)");

        Character got = CharObj.GetComponent<Character>();
        Enemy_Tag = got.Tag;
        Character go = CharObj.GetComponent<Character>();
        target = go.Enemy;

        transform.LookAt(target.transform);
        //StartCoroutine(LifeCycle(LifeTime));
    }

    void FixedUpdate()
    {
        ArrowFixedUpdate();
    }

    public virtual void ArrowFixedUpdate()
    {
        MyTransform.position += transform.forward * speed * Time.deltaTime;

        //Просто аккуратно поворачиваем билборд
        //transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.up);
    }

    

    /* ЁБАНЫЙ СТЫД 
     * 
    public IEnumerator LifeCycle(float Life)
    {
        yield return new WaitForSeconds(LifeTime);
        Damage = 0;
        MyTransform.position -= transform.up * -speed * 4;
    }

     */ 

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Enemy_Tag)
        {
            EnemyAI go = other.gameObject.GetComponent<EnemyAI>();
            go.Health(Damage, gameObject.tag);
        }
    }
}
