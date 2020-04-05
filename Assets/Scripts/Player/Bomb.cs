using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{
    public float MaxSize;
    public float SpeedExpolision;   

    float Range = 1;
    Vector3 RangeVector;

    // Use this for initialization
    void Start()
    {
        RangeVector = new Vector3(Range, Range, Range);
    }

    void Update()
    {
        if (transform.localScale.y < MaxSize)
        {
            transform.localScale += RangeVector * SpeedExpolision * Time.deltaTime;
        }

        if (transform.localScale.y >= MaxSize)
            Destroy(gameObject);
    }
}