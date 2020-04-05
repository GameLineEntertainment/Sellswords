using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour 
{
    public float CurHealth_Point;            // Количество здоровья
    //public bool NonDamaging = false;
    public bool IsVsCube = true;

    private EnemyAI Freez;

	// Use this for initialization
	void Start () 
    {
        Freez = GetComponent<EnemyAI>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void Die()
    {
        if(IsVsCube)
        {
        Destroy(gameObject);
        Counter.Kills++;
        }

        if (!IsVsCube)
        {
            //Counter.Kills++;
            RagOn rg = GetComponent<RagOn>();
            rg.TriggerOn();
            Counter.Kills++;
        }
    }

    public void Health(float Damage)  // отнимаем жизни
    {
        CurHealth_Point -= Damage;

        CheckDeath();
    }

    public void CheckDeath()
    {
        if (Freez.Заморожен == false)
            if (CurHealth_Point <= 0)
            {
                Die();
            }
    }
}
