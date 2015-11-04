using UnityEngine;
using System.Collections;

public class tp : MonoBehaviour {

	// Use this for initialization

	public GameObject tepe;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Claire")
			coll.transform.localPosition = tepe.gameObject.transform.localPosition;
	}

}