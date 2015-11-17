using UnityEngine;
using System.Collections;

public class controller_maya : MonoBehaviour {

	public Animator anim;
	public NavMeshAgent nav;
	public static controller_maya instance;
	public int HP;
	public int level;
	public bool is_attacking;
	public bool is_attacking_anim;
	// Use this for initialization
	void Start () {
		HP = 100;
		level = 1;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		nav.stoppingDistance = 1f;
		instance = this;
		is_attacking = false;
		is_attacking_anim = false;
		Physics.queriesHitTriggers = false;
	}

	// Update is called once per frame
	void Update () {
		if (HP > 0) {
			if (Input.GetMouseButton (0)) {
				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit)) {
					nav.SetDestination (hit.point);
					anim.SetBool ("run", true);
					anim.SetBool ("idle", false);
				}
			}
			if (nav.remainingDistance <= nav.stoppingDistance && nav.velocity.sqrMagnitude < 50 && !is_attacking) {
				anim.SetBool ("run", false);
				anim.SetBool ("idle", true);
			}
			if (nav.velocity.sqrMagnitude > 50 && nav.destination != Vector3.zero) {
				anim.SetBool ("run", true);
				anim.SetBool ("idle", false);
			}
			changeSpeed ();
		} else
			StartCoroutine (die ());
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Enemy" && !coll.isTrigger) {
			is_attacking = true;
			anim.SetBool ("run", false);
			anim.SetBool ("idle", false);
			if (!is_attacking_anim)
				StartCoroutine (attack (coll));
			gameObject.transform.eulerAngles = -coll.gameObject.transform.eulerAngles;
		}
	}

	IEnumerator attack(Collider coll)
	{
		anim.SetBool ("attack", true);
		is_attacking_anim = true;
		yield return new WaitForSeconds(0.4f);
		if (controller_maya.instance.nav.velocity.sqrMagnitude < 50 && anim.GetBool ("attack"))
			coll.SendMessage ("get_hit");
		yield return new WaitForSeconds (0.3f);
		is_attacking_anim = false;
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "Enemy" && !coll.isTrigger) {
			is_attacking = false;
			anim.SetBool("attack", false);
		}
	}

	IEnumerator die()
	{
		anim.SetBool ("run", false);
		anim.SetBool ("idle", false);
		anim.SetBool ("attack", false);
		anim.SetBool ("death", true);
		yield return new WaitForSeconds (5f);
		Application.LoadLevel (Application.loadedLevel);
	}

	void changeSpeed()
	{
		float speedMultiplyer = 1.0f - 0.9f*Vector3.Angle(transform.forward, nav.steeringTarget-transform.position)/180.0f;
		nav.speed = 20 *speedMultiplyer;
	}

	public void ZombieDying()
	{
		is_attacking = false;
		anim.SetBool("attack", false);
	}
}