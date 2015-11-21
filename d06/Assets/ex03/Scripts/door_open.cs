using UnityEngine;
using System.Collections;

public class door_open : MonoBehaviour {

	public Animator anim;
	public AudioSource access_denied;
	public AudioSource access_granted;
	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay(Collider coll)
	{
		if (Input.GetKeyDown ("e")) {
			if (slider.obj.key)
			{
				access_granted.Play ();
				anim.Play("Open");
			}
			else
				access_denied.Play();
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		slider.obj.show_door_text (0);
	}
	void OnTriggerExit(Collider coll)
	{
		slider.obj.show_door_text (1);
	}
}