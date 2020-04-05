using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Вздрочнул");
        if (other.gameObject.tag == "Char")
        {
            Debug.Log("Проебал");
            Counter.Money++;
            Destroy(gameObject);
        }
    }
}
