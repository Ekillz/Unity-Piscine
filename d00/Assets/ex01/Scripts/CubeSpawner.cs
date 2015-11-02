using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour {

	public GameObject a;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


		int chooseLine = Random.Range (0, 3);
		Vector3 line = new Vector3 (0, 0, 0);
		Debug.Log (chooseLine);
		if (chooseLine == 0)
			line = new Vector3(-3.8f, 5.53f, 0);
		else if (chooseLine == 1)
			line = new Vector3(0.48f, 5.53f, 0);
		else if (chooseLine == 2)
			line = new Vector3(4.7f, 5.53f, 0);
		GameObject.Instantiate (a, line, Quaternion.identity);
	}
}
