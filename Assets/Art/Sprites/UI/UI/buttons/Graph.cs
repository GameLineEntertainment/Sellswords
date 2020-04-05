using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Graph : MonoBehaviour 
{
	public bool sound = false;
	public Slider s;
	public GameObject sl;


	void Start () 
    {
		s = sl.GetComponent<Slider> ();
		s.value = AudioListener.volume;
	}
	public void setVolume(float v)
    {
		AudioListener.volume = s.value;
	}

	public void LowGraph () 
    {
		QualitySettings.SetQualityLevel(1, true);
	}

	public void MediumGraph () 
    {		
		QualitySettings.SetQualityLevel(3, true);
	}
	public void HihgtGraph () 
    {		
		QualitySettings.SetQualityLevel(5, true);
	}

	public void AudioToggle () 
    { 
		sound = !sound;		 
		Debug.Log ("БЛя");
        //audio.mute = sound;	       
	}
	
}


