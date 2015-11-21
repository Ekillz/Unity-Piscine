using UnityEngine;
using System.Collections;

public class camera_laser : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider coll)
	{
		camera_light.instance.camera_big = 0.008f;
	}
	
	void OnTriggerExit(Collider coll)
	{
		camera_light.instance.camera_big = 0;
	}

//	void OnParticleCollision(GameObject go)
//	{
////		if (go.tag == "player")
//			slider.obj.progress.value -= 0.05f;
//	}


}