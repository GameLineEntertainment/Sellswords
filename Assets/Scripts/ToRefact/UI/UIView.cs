using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIView : MonoBehaviour
{
    public float ChangeTime = 200;
    public float DeltaPosition = 0.05f;
    protected GameObject Instance;
    protected RectTransform thisPosition;
    protected Vector2 callPosition, removePosition;

    protected virtual void Awake()
    {
        Instance = this.gameObject;
        thisPosition = Instance.GetComponent<RectTransform>();
    }

    public virtual IEnumerator Move(Vector2 finalPosition)
    {
        float position = 0;
        while (position != 1)
        {
            var _distance = (finalPosition - thisPosition.anchoredPosition).magnitude;
            position += 1 / (ChangeTime);
            if (_distance < DeltaPosition) position = 1;
            position = Mathf.Clamp(position, 0, 1);
            thisPosition.anchoredPosition = Vector3.Lerp(thisPosition.anchoredPosition, finalPosition, position);
            yield return null;
        }
    }

    public virtual void Call()
    {
        StartCoroutine(Move(callPosition));
    }

    public virtual void Remove()
    {
        StartCoroutine(Move(removePosition));
    }
}

