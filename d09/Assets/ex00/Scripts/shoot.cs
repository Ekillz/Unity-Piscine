using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shoot : MonoBehaviour {


	public int		  HP;
	public GameObject particle0;
	public GameObject particle1;
	public Camera main_camera;
	public GameObject[] weapons;
	public Material material0;
	public Material material1;
	public GameObject bout0;
	public GameObject bout1;
	public GameObject bout;
	public bool		can_shoot;
	public Animator anim0;
	public Animator anim1;
	public AudioSource audio0;
	public AudioSource audio1;
	public AudioSource audio_ambiance;
	public LineRenderer line;
	public static shoot instance { get; set;}
	public Text loose_msg;
	public Text health;
	public int HP_str;
	public Text next_wave_timer;
	public Text next_wave_msg;
	public Text zombies_killed;
	public bool showing_time;
	public bool is_frozen;
	public Vector3 frozen_pos;
	// Use this for initialization
	void Start () {
		HP = 100;
		is_frozen = false;
		showing_time = false;
		weapons [0].SetActive (true);
		weapons [1].SetActive (false);
		line = GetComponent<LineRenderer>();
		can_shoot = true;
		line.material = material0;
		bout = bout0;
		Physics.queriesHitTriggers = false; // DISABLE RAYCAST HITTING TRIGGERS //
		instance = this;
		anim0 = weapons [0].GetComponent<Animator> ();
		anim1 = weapons [1].GetComponent<Animator> ();
		audio_ambiance.Play ();
		zombies_killed.enabled = true;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (is_frozen);
		if (!wave_manager.instance.wave_start && !showing_time) {
			StartCoroutine(show_wave_time());
		}
		if (is_frozen)
			transform.position = frozen_pos;
		HP_str = HP;
		health.text = HP_str.ToString (); 
		zombies_killed.text = "Zombies killed: " + wave_manager.instance.dead_zombies;
		if (HP > 0) {
			if (!is_frozen) {
				if (!audio_ambiance.isPlaying)
					audio_ambiance.Play ();
				if (Input.GetMouseButton (0)) {
					if (can_shoot) {
						if (weapons [0].activeSelf == true) {
							audio0.Play ();
							StartCoroutine (animate (0));
							StartCoroutine (wait_fire (0.75f));
						} else {
							audio1.Play ();
							StartCoroutine (animate (1));
							StartCoroutine (wait_fire (0.3f));
						}

						RaycastHit hit;
						GameObject fire;
						Physics.Raycast (bout.transform.position, Camera.main.transform.forward, out hit, 10000f);
						Collider[] coll = Physics.OverlapSphere (hit.point, 2f);
						if (weapons [0].activeSelf == true) {
							fire = (GameObject)GameObject.Instantiate (particle0, hit.point, Quaternion.identity);
							int i = 0;
							while (i < coll.Length) {
								if (coll [i].tag == "Enemy")
									coll [i].gameObject.SendMessage ("weapon0_dmg");
								i++;
							}
						} else {
							fire = (GameObject)GameObject.Instantiate (particle1, hit.point, Quaternion.identity);
							if (hit.collider.tag == "Enemy" && !hit.collider.isTrigger)
								hit.collider.gameObject.SendMessage ("weapon1_dmg");
						}
						LineRenderer line = GetComponent<LineRenderer> ();
						line.enabled = true;
						line.SetWidth (0.15f, 0.15f);

						Ray r = new Ray (bout.transform.position, transform.forward);
						line.SetPosition (0, r.origin);
						line.SetPosition (1, hit.point);
						StartCoroutine (destroy_line ());
						StartCoroutine (destroy_particle (fire));
					}
				}
				if (Input.GetKeyDown ("1")) {
					bout = bout0;
					line.material = material0;
					weapons [0].SetActive (true);
					weapons [1].SetActive (false);
				}
				if (Input.GetKeyDown ("2")) {
					bout = bout1;
					line.material = material1;
					weapons [0].SetActive (false);
					weapons [1].SetActive (true);
				}
			}
		}
		else
			StartCoroutine (die ());
	}
	IEnumerator destroy_particle(GameObject fire)
	{
		yield return new WaitForSeconds (1f);
		Destroy (fire);
	}

	IEnumerator destroy_line()
	{
		yield return new WaitForSeconds (0.03f);
		line.enabled = false;
	}

	IEnumerator wait_fire(float time)
	{
		can_shoot = false;
		yield return new WaitForSeconds (time);
		can_shoot = true;
	}
	IEnumerator animate(int which)
	{
		if (which == 0) {
			anim0.SetBool ("fire", true);
		} else {
			anim1.SetBool ("fire", true);
		}
		while (!Input.GetMouseButtonUp (0)) {
			yield return null;
		}
		if (which == 0)
			anim0.SetBool("fire", false);
		else
			anim1.SetBool("fire", false);
	}

	IEnumerator die()
	{
		loose_msg.text = "You are Dead You survived " + wave_manager.instance.level + " rounds. Respawning in 5 seconds";
		loose_msg.enabled = true;
		yield return new WaitForSeconds (5f);
		loose_msg.enabled = false;
		Application.LoadLevel (Application.loadedLevel);
	}

	IEnumerator show_wave_time()
	{
		showing_time = true;
		int wave_time = 35;
		next_wave_timer.enabled = true;
		while (wave_time > 0) {
			next_wave_timer.text = "Next  wave in: " + wave_time;
			wave_time--;
			yield return new WaitForSeconds(1f);
		}
		next_wave_timer.enabled = false;
		showing_time = false;
		next_wave_msg.enabled = true;
		next_wave_msg.text = "Wave " + wave_manager.instance.level + " is spawning!";
		yield return new WaitForSeconds (2.5f);
		next_wave_msg.enabled = false;
	}

	IEnumerator frozen()
	{
		frozen_pos = transform.position;
		is_frozen = true;
		yield return new WaitForSeconds (2f);
		is_frozen = false;
	}
	
	public void get_hit()
	{
		StartCoroutine (frozen ());
	}
}