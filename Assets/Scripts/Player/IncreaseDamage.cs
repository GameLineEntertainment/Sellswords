using UnityEngine;
using System.Collections;

public class IncreaseDamage : MonoBehaviour 
{
    public float Damage_Bomb;

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
        if (other.gameObject.tag == "AoE" && (other.gameObject.tag != "Lighting" && other.gameObject.name != "Lighting(Clone)"))
        {
            Arrow Дамаг = other.gameObject.GetComponent<Arrow>();
            Дамаг.Damage++;
        }
         * */

        if (other.gameObject.name == "Arrow(New)" || other.gameObject.name == "Arrow(New)(Clone)")
        {
            Arrow Дамаг = other.gameObject.GetComponent<Arrow>();
            Дамаг.Damage++;
        }

        if (other.gameObject.name == "Lighting" || other.gameObject.name ==  "Lighting(Clone)")
        {
            Arrow Дамаг = other.gameObject.GetComponent<Arrow>();
            Дамаг.Damage--;
        }

        if (other.gameObject.name == "BombWave2(Clone)" || other.gameObject.name == "BombWave1(Clone)")
        {
            Bomb Бомба = other.gameObject.GetComponent<Bomb>();
            //Бомба.Damage = Damage_Bomb;
        }
    }
}
