using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour {

	public GameObject[] obj;
	public float timer;
	// Use this for initialization
	void Start () {
		this.timer = Time.unscaledTime;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.unscaledTime - this.timer >= 0.83) {
			int chooseLine = Random.Range (0, 3);
			Vector3 line = new Vector3 (0, 0, 0);
			if (chooseLine == 0)
				line = new Vector3 (-4.2f, 8.6f, 0);
			else if (chooseLine == 1)
				line = new Vector3 (0.07f, 8.6f, 0);
			else if (chooseLine == 2)
				line = new Vector3 (4.29f, 8.6f, 0);
			GameObject.Instantiate (obj[chooseLine], line, Quaternion.identity);
			this.timer = Time.unscaledTime;
		}
	}
}
