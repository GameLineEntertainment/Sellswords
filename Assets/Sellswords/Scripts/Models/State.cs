using RootMotion.Dynamics;
using UnityEngine;


namespace Sellswords
{
    public abstract class State
    {
        public float Hp;
        public float MoveSpeed;
        public Rigidbody Rigidbody;
        public bool IsDead;
    }
}