using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {

	public GameObject[] enemy;
	public GameObject current_zombie;
	public bool is_running;
	// Use this for initialization
	void Start () {
		is_running = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!current_zombie && !is_running)
			StartCoroutine (spawn_enemy ());
	}

	IEnumerator spawn_enemy()
	{
		is_running = true;
		yield return new WaitForSeconds (3f);
		current_zombie = (GameObject)GameObject.Instantiate (enemy [Random.Range (0, 2)], transform.position, Quaternion.identity);
		is_running = false;
	}
}