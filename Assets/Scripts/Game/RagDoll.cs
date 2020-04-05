using UnityEngine;
using System.Collections;

public class RagDoll : MonoBehaviour
{
    private Component[] rb;
    private Animator anim;
    private static RagDoll _ragdoll;
    private Rigidbody Rigid;
    bool IsDone = false;

    public bool KnockedOut;

    // Use this for initialization
    void Start()
    {
        _ragdoll = this;

        anim = GetComponent<Animator>();

        rb = GetComponentsInChildren<Rigidbody>();

        if (KnockedOut == false)
        {
            EnableRagDoll();
        }

        //Rigid.active = false;

    }

    /*
    public static void KnockOut(Transform dir)
    {
        _ragdoll.anim.enabled = false;
        _ragdoll.KnockedOut = true;
    }

    // Update is called once per frame
      
      */

    void FixedUpdate()
    {
        if (KnockedOut == true && IsDone == false)
        {
            DisableRagDoll();
        }

        /*
       if (KnockedOut == false)
       {
           EnableRagDoll();
       }*/

     
    }

    void DisableRagDoll()
    {
        Rigidbody[] special = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody Rig in special)
            Rig.isKinematic = false;

        anim.enabled = false;
        //anim.SetBool("Death", KnockedOut);

        IsDone = true;
    }

    void EnableRagDoll()
    {
        Rigidbody[] special = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody Rig in special)
            Rig.isKinematic = true;

        anim.enabled = true;
        anim.SetBool("Death", KnockedOut);
    }
      
}
