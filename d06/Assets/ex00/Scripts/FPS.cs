using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour {


	public float speedH = 2.0f;
	public float speedV = 2.0f;
	public float speed = 0.1f;
	private float yaw = 0.0f;
	private float pitch = 0.0f;


	public AudioSource step;
	public GameObject audio;
	public CharacterController character;
	// Use this for initialization
	void Start () {
		character = GetComponent<CharacterController> ();
		step = audio.GetComponent<AudioSource> ();
		step.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);	
		yaw += speedH * Input.GetAxis ("Mouse X");
		pitch -= speedV * Input.GetAxis ("Mouse Y");
		transform.eulerAngles = new Vector3 (pitch, yaw, 0.0f);
		if (Input.GetKey ("w") || Input.GetKey ("s") || Input.GetKey ("a") || Input.GetKey ("d")) {
			step.enabled = true;
			if (!step.isPlaying)
				step.Play();
		}
		if (Input.GetKey ("w"))
			character.Move (transform.rotation * Vector3.forward * speed);
		if (Input.GetKey ("s"))
			character.Move (transform.rotation * Vector3.back * speed);
		if (Input.GetKey ("a"))
			character.Move (transform.rotation * Vector3.left * speed);
		if (Input.GetKey ("d"))
			character.Move (transform.rotation * Vector3.right * speed);
		if (Input.GetKeyDown (KeyCode.LeftShift))
			speed = 0.15f;
		if (Input.GetKeyUp (KeyCode.LeftShift))
			speed = 0.1f;
	}
}