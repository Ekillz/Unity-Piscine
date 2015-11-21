using UnityEngine;
using System.Collections;

public class camera_light : MonoBehaviour {

	public float camera_big;
	public static camera_light instance {get; set;}
	// Use this for initialization
	void Start () {
		instance = this;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider coll)
	{
		slider.obj.detect = true;
		slider.obj.progress.value += 0.02f - camera_big;
	}
	
	void OnTriggerExit(Collider coll)
	{
		slider.obj.detect = false;
	}
}
