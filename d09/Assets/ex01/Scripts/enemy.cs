using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	public int HP;
	public Animator anim;
	public bool dead;
	public GameObject way;
	public NavMeshAgent nav;
	public bool target;
	public bool can_get_hit;
	// Use this for initialization
	void Start () {
		HP = wave_manager.instance.level * 5;
		anim = GetComponent<Animator> ();
		dead = false;
		nav = GetComponent<NavMeshAgent>();
		nav.SetDestination(way.transform.position);
		nav.stoppingDistance = 2f;
		anim.SetBool ("idle", false);
		anim.SetBool ("walk", true);
		target = false;
		can_get_hit = true;
	}
	// Update is called once per frame
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !dead && anim.GetBool ("attack")) {
			shoot.instance.HP -= 5 + wave_manager.instance.level;
		}
		if (anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !dead)
			anim.Play (0);
		if (Vector3.Distance(transform.position, nav.destination) <= nav.stoppingDistance && target) {
			anim.SetBool ("idle", false);
			anim.SetBool("walk", false);
			anim.SetBool("run", false);
			anim.SetBool("attack", true);
		}
		if (Vector3.Distance (transform.position, nav.destination) <= nav.stoppingDistance && !target) {
			anim.SetBool ("idle", true);
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