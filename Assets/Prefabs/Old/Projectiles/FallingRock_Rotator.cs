using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock_Rotator : Progectile
{
    [SerializeField]
    Transform Portal, Rock;

    protected override void Update()
    {
        if (!Killed && Target != null)
        {
            Rock.Translate(Vector3.forward * Settings.Speed * Time.deltaTime);
            rotateGO(Portal);
            rotateGO(Rock);
        }
    }

    void rotateGO(Transform GO)
    {
        Vector3 lol = Target.position - GO.position;
        GO.rotation = Quaternion.Slerp(GO.rotation,
                                Quaternion.LookRotation(lol),
                                Settings.RotationSpeed * Time.deltaTime);
    }
}
