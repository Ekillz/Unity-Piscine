using UnityEngine;
using System.Collections;

public class camera_follow : MonoBehaviour {

	public GameObject maya;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (maya.transform.position.x, maya.transform.position.y + 50, maya.transform.position.z + 50);
		transform.LookAt (maya.transform);
	}
}
