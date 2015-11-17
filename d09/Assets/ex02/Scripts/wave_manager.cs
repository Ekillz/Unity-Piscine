using UnityEngine;
using System.Collections;

public class wave_manager : MonoBehaviour {


	public static wave_manager instance {get; set;}
	public int level;
	public int dead_zombies;
	public int current_zombies;
	public	bool wave_start;
	public bool is_waiting;
	public bool boss_alive;
	// Use this for initialzation
	void Start () {
		instance = this;
		level = 1	; // ?? MODFIIT A 1
		dead_zombies = 0;
		current_zombies = 0;
		wave_start = true;
		is_waiting = false;
		boss_alive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (current_zombies >= 20 && !is_waiting) {
			StartCoroutine (wait_wave ());
		} else if (current_zombies == 1 && level % 3 == 0) {
			StartCoroutine(wait_wave());
		}
	}

	IEnumerator wait_wave()
	{
		is_waiting = true;
		wave_start = false;
		int i = 0;
		if (level % 3 == 0) {
			while (boss_alive) {
				yield return 0;
			}
		} 
		else {
			while (i < 35) {
				yield return new WaitForSeconds (1f);
				i++;
			}
		}
		level++;
		current_zombies = 0;
		wave_start = true;
		is_waiting = false;
	}
}