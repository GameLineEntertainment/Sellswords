using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spears : Progectile
{

    protected override void Update()
    {
        if (!Killed)
        {
            transform.position += transform.forward * Settings.Speed * Time.deltaTime;

            Vector3 lol = Target.position - MyTransform.position;
            MyTransform.rotation = Quaternion.Slerp(MyTransform.rotation,
                                    Quaternion.LookRotation(lol),
                                    Settings.RotationSpeed * Time.deltaTime);
        }
    }
}
