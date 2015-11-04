using UnityEngine;
using System.Collections;

public class trigger : MonoBehaviour {

	public string type;
	static public int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (count == 3) {
			count = 2000;
			Debug.Log ("You won");
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == type)
			count++;
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == type)
			count--;
	}
}
