using UnityEngine;
using System.Collections;

public class mega_alert : MonoBehaviour {

	public GameObject[] cams;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider coll)
	{
		Debug.Log ("dude");
		cams [0].SetActive (true);
		cams [1].SetActive (true);
	}
}