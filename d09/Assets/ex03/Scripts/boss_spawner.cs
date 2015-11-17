using UnityEngine;
using System.Collections;

public class boss_spawner : MonoBehaviour {

	public GameObject enemy;
	public bool spawn_start;
	// Use this for initialization
	void Start () {
		spawn_start = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (wave_manager.instance.wave_start && !spawn_start && wave_manager.instance.level % 3 == 0)
			StartCoroutine (spawn ());
	}
	
	IEnumerator spawn()
	{
		spawn_start = true;
		Instantiate (enemy, transform.position, Quaternion.identity);
		wave_manager.instance.boss_alive = true;
		wave_manager.instance.current_zombies++;
		spawn_start = false;
		yield return 0;
	}
}
