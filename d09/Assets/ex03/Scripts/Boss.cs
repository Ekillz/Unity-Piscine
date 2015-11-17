using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
	
	public int HP;
	public Animator anim;
	public bool dead;
	public NavMeshAgent nav;
	public bool target;
	public bool can_get_hit;
	public GameObject particle1;
	// Use this for initialization
	void Start () {
		HP = wave_manager.instance.level * 100;
		anim = GetComponent<Animator> ();
		dead = false;
		nav = GetComponent<NavMeshAgent>();
		nav.stoppingDistance = 2f;
		anim.SetBool ("idle", false);
		anim.SetBool ("walk", false);
		anim.SetBool ("run", true);
		target = false;
		can_get_hit = true;
		StartCoroutine(shoot_laser());
		
	}
	// Update is called once per frame
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !dead && anim.GetBool ("attack")) {
			shoot.instance.HP -= 10 * wave_manager.instance.level;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !dead)
			anim.Play (0);
		if (Vector3.Distance(transform.position, nav.destination) <= nav.stoppingDistance && target) {
			anim.SetBool ("idle", false);
			anim.SetBool("walk", false);
			anim.SetBool("run", false);
			anim.SetBool("attack", true);
		}
		if (Vector3.Distance (transform.position, nav.destination) > nav.stoppingDistance)
			anim.SetBool ("attack", false);
		if (target)
			nav.SetDestination (shoot.instance.transform.position);
	}
	
	public void weapon0_dmg()
	{
		if (!target) {
			target = true;
			anim.SetBool ("walk", false);
			anim.SetBool ("run", true);
		}
		HP -= 4;
		if (HP <= 0) {
			nav.Stop();
			anim.SetBool ("dead", true);
			dead = true;
			StartCoroutine(die());
		}
		else
			StartCoroutine (get_hit ());
	}
	
	public void weapon1_dmg()
	{
		if (!target) {
			target = true;
			anim.SetBool ("walk", false);	
			anim.SetBool ("run", true);
		}
		HP -= 10;
		if (HP <= 0) {
			nav.Stop();
			anim.SetBool ("dead", true);
			dead = true;
			StartCoroutine (die ());
		}
		else
			StartCoroutine (get_hit ());
	}
	
	IEnumerator die()
	{
		yield return new WaitForSeconds (3f);
		wave_manager.instance.dead_zombies++;
		wave_manager.instance.boss_alive = false;
		Destroy (gameObject);
	}
	
	IEnumerator get_hit()
	{
		anim.SetBool ("walk", false);
		anim.SetBool ("run", false);
		anim.SetBool("hit", true);
		yield return new WaitForSeconds (0.3f);
		anim.SetBool("hit", false);
		anim.SetBool ("run", true);
	}
	
	IEnumerator wait_dmg()
	{
		can_get_hit = false;
		yield return new WaitForSeconds (0.5f);
		can_get_hit = true;
	}

	IEnumerator shoot_laser()
	{

		while (wave_manager.instance.boss_alive) {
			anim.SetBool ("run", false);
			anim.SetBool ("idle", true);
//			nav.Stop();
			GameObject fire = (GameObject)GameObject.Instantiate (particle1, transform.position + transform.forward * 4, Quaternion.identity);
			Collider[] coll = Physics.OverlapSphere(fire.transform.position, 5f);
			StartCoroutine(destroy_particle(fire));
			int i = 0;
			while (i < coll.Length) {
				if (coll [i].tag == "Player")
					shoot.instance.get_hit();
				i++;
			}
			yield return new WaitForSeconds(1.0f);
			nav.SetDestination(shoot.instance.gameObject.transform.position);
			anim.SetBool ("idle", false);
			anim.SetBool ("run", true);
			yield return new WaitForSeconds(2.5f);
		}
	}

	IEnumerator destroy_particle(GameObject fire)
	{
		yield return new WaitForSeconds (2f);
		Destroy (fire);
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Player") {
			target = true;
			anim.SetBool ("walk", false);
			anim.SetBool ("run", true);
		}
	}
	void OnTriggerExit(Collider coll)
	{
		anim.SetBool("walk", true);
		anim.SetBool("run", false);
		anim.SetBool("attack", false);
		nav.SetDestination (coll.transform.position);
		target = false;
	}
}