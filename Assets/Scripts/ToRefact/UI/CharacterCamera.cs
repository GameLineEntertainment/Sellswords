using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public float ChangeTime = 200;
    public float DeltaPosition = 0.05f;
    public Transform CharacterPosition, UpgradePosition;
    private GameObject Instance;
    private Transform thisPosition;

    protected virtual void Awake()
    {
        Instance = this.gameObject;
        thisPosition = Instance.GetComponent<Transform>();
    }

    public virtual IEnumerator Move(Transform finalPosition)
    {
        float position = 0;
        while (position != 1)
        {
            var _distance = (finalPosition.position - thisPosition.position).magnitude;
            position += 1 / (ChangeTime);
            if (_distance < DeltaPosition) position = 1;
            position = Mathf.Clamp(position, 0, 1);
            thisPosition.position = Vector3.Slerp(thisPosition.position, finalPosition.position, position);
            thisPosition.rotation = Quaternion.Slerp(thisPosition.rotation, finalPosition.rotation, position);
            yield return null;
        }
    }

    public virtual void CharacterPos()
    {
        StartCoroutine(Move(CharacterPosition));
    }

    public virtual void UpgradePos()
    {
        StartCoroutine(Move(UpgradePosition));
    }

}