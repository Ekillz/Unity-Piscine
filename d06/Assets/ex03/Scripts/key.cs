using UnityEngine;
using System.Collections;

public class key : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay(Collider coll)
	{
		if (Input.GetKeyDown ("e")) {
			slider.obj.key = true;
			gameObject.SetActive(false);
			slider.obj.show_key_text (1);
		}
	}
	void OnTriggerEnter(Collider coll)
	{
		slider.obj.show_key_text (0);
	}
	void OnTriggerExit(Collider coll)
	{
		slider.obj.show_key_text (1);
	}
}