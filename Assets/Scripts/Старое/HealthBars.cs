using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBars : MonoBehaviour {
	public Scrollbar HealthBar;
	public float Health = 100;
	public GameObject Text;
	public int num;

	void Start ()
	{
	}

	void Update () 
	{
		Text.GetComponent<Text>().text = ("") + num;
	}

	public void Damage(float value)
	{
		Health -= value;
		HealthBar.size = Health / 100f;
		Text.GetComponent<Text>().text = ("") + value;
	}
}