using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {


	public NavMeshAgent nav;
	public Animator anim;
	public bool is_following;
	public bool is_attacking;
	public int HP;
	// Use this for initialization
	
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		is_following = false;
		is_attacking = false;
		nav.stoppingDistance = 4.5f;
		HP = 3;
		anim.SetBool ("idle", true);
		anim.SetBool ("run", false);
		anim.SetBool ("attack", false);
	}
	
	// Update is called once per frame
	void Update () {
		if (HP > 0) {
			if (nav.remainingDistance > nav.stoppingDistance && is_following) {
				anim.SetBool ("idle", false);
				anim.SetBool ("run", true);
				anim.SetBool ("attack", false);
			} else if (nav.remainingDistance <= nav.stoppingDistance && is_following) {
				anim.SetBool ("idle", false);
				anim.SetBool ("run", false);
				if (!is_attacking)
					StartCoroutine (attack ());
			}
			changeSpeed ();
		} else
			StartCoroutine (die ());
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Player") {
			nav.SetDestination (controller_maya.instance.transform.position);
			is_following = true;
		}
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Player" && controller_maya.instance.nav.velocity.sqrMagnitude > 1) {
			is_following = true;
			nav.SetDestination(controller_maya.instance.transform.position);
		}
	}

	IEnumerator die()
	{
		anim.SetBool ("run", false);
		anim.SetBool ("idle", false);
		anim.SetBool ("attack", false);
		anim.SetBool ("death", true);
		GetComponent<CapsuleCollider> ().enabled = false;
		controller_maya.instance.ZombieDying ();
		yield return new WaitForSeconds (3f);
		Destroy (gameObject);
	}

	IEnumerator attack()
	{
		anim.SetBool ("attack", true);
		is_attacking = true;
		yield return new WaitForSeconds(0.6f);
		if (controller_maya.instance.nav.velocity.sqrMagnitude < 50 && anim.GetBool ("attack"))
			controller_maya.instance.HP -= 5; // * level
		yield return new WaitForSeconds (0.7f);
		is_attacking = false;
	}

	public void get_hit()
	{
		HP -= 1;
	}
	void changeSpeed()
	{
		float speedMultiplyer = 1.0f - 0.9f*Vector3.Angle(transform.forward, nav.steeringTarget-transform.position)/180.0f;
		nav.speed = 15 *speedMultiplyer;
	}
}