using UnityEngine;
using System.Collections;

public class MyAnim : MonoBehaviour 
{
    private Animator anim;

	// Use this for initialization
	void Start () 
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        //ГАВНОКОД!!!!!!!
        if (GameLevel.Death_Over == true)
            Death(true);
	}

    public void Attack(bool Атака)
    {
        anim.SetBool("Attack", Атака);
        StartCoroutine(Откат("Attack"));
    }

    public void Damage(bool Урон)
    {
        anim.SetBool("Damage", Урон);
        StartCoroutine(Откат("Damage"));
    }

    public void Run(float moveSpeed)
    {
        if(anim != null)
        anim.SetFloat("Speed", moveSpeed);
    }

    public void Death(bool Смерть)
    {
        if (GameLevel.GodMode == false)
        {
            anim.SetBool("Death", Смерть);
            anim.applyRootMotion = true;
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            StartCoroutine(Откат("Реабилитация"));      // Какая-то фигня на блин исправить !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            print("Эта штука вызывает проседание FPS");
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
    }

    IEnumerator Откат(string Анимация)
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool(Анимация, false);
    }

    IEnumerator Реабилитация()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Death", false);
    }
}
