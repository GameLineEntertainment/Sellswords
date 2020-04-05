using UnityEngine;


namespace Sellswords
{
    [CreateAssetMenu(fileName = "status_data", menuName = "Sellswords/Data/Spell/Status Object")]
    public class StatusObject : ScriptableObject
    {
        public StatusType StatusType;

        [ShowInInspectorByStatus(StatusType.PhysicalDamage)]
        public float Damage = 0f;

        [ShowInInspectorByStatus(StatusType.Force)]
        public Vector3 PushForce;

        [ShowInInspectorByStatus(StatusType.Slow)]
        public float SlowForce;

        [ShowInInspectorByStatus(StatusType.Slow)]
        [ShowInInspectorByStatus(StatusType.Freeze)]
        [ShowInInspectorByStatus(StatusType.PhysicalDamage)]
        public float Duration;
        
        [ShowInInspectorByStatus(StatusType.PhysicalDamage)]
        public float Interval;
    }
}