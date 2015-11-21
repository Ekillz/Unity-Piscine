using UnityEngine;
using System.Collections;

public class light : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider coll)
	{
		slider.obj.detect = true;
		slider.obj.progress.value += 0.007f;
	}

	void OnTriggerExit(Collider coll)
	{
		slider.obj.detect = false;
	}
}