using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {


	public NavMeshAgent nav;
	public Animator anim;
	public bool is_following;
	public bool is_attacking;

	// stats //
	public float HP;
	public static float MaxHp;
	public float STR;
	public float AGI;
	public float CON;
	public float minDamage;
	public float maxDamage;
	public int   level;
	public float XP;
	public float money;
	public float HitChance;
	public float BaseDmg;
	public float FinalDmg;
	public static float my_agi;
	public static float my_xp;
	public static float my_money;
	public bool calculating_nav;
	public bool is_dying;
	public GameObject healthPotion;
	// stats //

	void Start () {
		recalculate_stats ();

		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		is_following = false;
		is_attacking = false;
		calculating_nav = false;
		is_dying = false;
		nav.stoppingDistance = 4.5f;
		anim.SetBool ("idle", true);
		anim.SetBool ("run", false);
		anim.SetBool ("attack", false);
	}

	void recalculate_stats()
	{
		level = controller_maya.instance.level;
		STR = 8 + level * 2;
		AGI = 8 + level * 2;
		CON = 8 + level * 2;
		my_agi = AGI;
		XP = 1 + level / 2;
		money = 10 * level;
		my_xp = XP;
		my_money = money;
		MaxHp = 5 * CON;
		HP = MaxHp;
		minDamage = STR / 2;
		maxDamage = minDamage + 4;
		HitChance = 75 + AGI - controller_maya.instance.AGI;
		BaseDmg = Random.Range (minDamage, maxDamage + 1);
		FinalDmg = BaseDmg;
		// recalcul need //
	}

	public float get_dmg()
	{
		BaseDmg = Random.Range (minDamage, maxDamage + 1);
		FinalDmg = BaseDmg;
		return FinalDmg;
	}

	void Update () {
		if (HP > 0) {
			if (nav.remainingDistance > nav.stoppingDistance && is_following) {
				anim.SetBool ("idle", false);
				anim.SetBool ("run", true);
				anim.SetBool ("attack", false);
			} else if (nav.remainingDistance <= nav.stoppingDistance && is_following && Vector3.Distance (transform.position, controller_maya.instance.transform.position) <= nav.stoppingDistance) {
				anim.SetBool ("idle", false);
				anim.SetBool ("run", false);
				if (!is_attacking)
					StartCoroutine (attack ());
			} else if (Vector3.Distance (transform.position, controller_maya.instance.transform.position) >= nav.stoppingDistance && is_following)
				nav.SetDestination (controller_maya.instance.transform.position);
			changeSpeed ();
		} 
		else {
			if (!is_dying)
				StartCoroutine (die ());
		}
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
		if (coll.tag == "Player" && controller_maya.instance.nav.velocity.sqrMagnitude > 1 && !calculating_nav) {
			if (!calculating_nav)
				StartCoroutine(calc_nav_wait());
			is_following = true;
			nav.SetDestination(controller_maya.instance.transform.position);
		}
	}

	IEnumerator calc_nav_wait()
	{
		calculating_nav = true;
		yield return new WaitForSeconds (1.2f);
		calculating_nav = false;
	}

	IEnumerator die()
	{
		is_dying = true;
		GetComponent<SphereCollider> ().enabled = false;
		GetComponent<CapsuleCollider> ().enabled = false;
		anim.SetBool ("run", false);
		anim.SetBool ("idle", false);
		anim.SetBool ("attack", false);
		anim.SetBool ("death", true);
		nav.enabled = false;
		controller_maya.instance.ZombieDying ();
		yield return new WaitForSeconds (2f);
		int i = 0;
		Vector3 savePos = transform.position + new Vector3(-10, 0, -10);
		while (i < 5) {
			yield return new WaitForSeconds(0.5f);
			transform.position -= new Vector3(0, 0.05f, 0);
			i++;
		}
		if (Random.Range (0, 100) <= 30) {
			Instantiate(healthPotion, savePos, Quaternion.identity);
		}
		Destroy (gameObject);
		is_dying = false;
	}

	IEnumerator attack()
	{
		anim.SetBool ("attack", true);
		is_attacking = true;
		yield return new WaitForSeconds(0.6f);
		if (controller_maya.instance.nav.velocity.sqrMagnitude < 30 && anim.GetBool ("attack")) {
			if (Random.Range(0, 100f) <= HitChance)
				controller_maya.instance.HP -= get_dmg ();
		}
		yield return new WaitForSeconds (0.7f);
		is_attacking = false;
	}

	public void get_hit(float damage)
	{
		HP -= damage;
	}
	void changeSpeed()
	{
		float speedMultiplyer = 1.0f - 0.9f*Vector3.Angle(transform.forward, nav.steeringTarget-transform.position)/180.0f;
		nav.speed = 15 *speedMultiplyer;
	}
}