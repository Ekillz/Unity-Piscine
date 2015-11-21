using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class controller_maya : MonoBehaviour {

	public Animator anim;
	public NavMeshAgent nav;
	public static controller_maya instance;
	public bool is_attacking;
	public bool is_attacking_anim;
	public GameObject current_target;
	// STATS //
	public float HP;
	public float MaxHp;
	public float STR;
	public float AGI;
	public float CON;
	public float minDamage;
	public float maxDamage;
	public int   level;
	public float XP;
	public float money;
	public float XpToNextLevel;
	public float HitChance;
	public float BaseDmg;
	public float FinalDmg;
	public int   stat_points;
	public Text hero_hp;
	public Text target_hp;
	public Text hero_lvl;
	public Text target_lvl;
	public Image target_img;
	public Slider xp_bar;
	public GameObject Panel;
	// GUI //


	// GUI //

	void Start () {

		// main stats //
		STR = 20;
		AGI = 20;
		CON = 20;
		level = 1;
		stat_points = 0;
		// main stats //

		// after enemy kill recalculate //
		XP = 0;
		XpToNextLevel = 5 * level - XP;
		money = 0;
		MaxHp = 5 * CON;
		HP = MaxHp;
		StartCoroutine (heal ());
		// after enemy kill recalculate //

		recalculate_stats ();

		current_target = null;
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		nav.stoppingDistance = 1f;
		instance = this;
		is_attacking = false;
		is_attacking_anim = false;
		Physics.queriesHitTriggers = false;
	}

	// COMBAT_STAT METHODS //

	void update_xp_bar()
	{
		xp_bar.minValue = 0;
		xp_bar.maxValue = 5 * level;
		xp_bar.value = XP;
	}

	void calculate_xp_money (float add_xp, float add_money)
	{
		XP += add_xp;
		money += add_money;
		XpToNextLevel = 5 * level - XP;
		if (XpToNextLevel <= 0) {
			XP -= 5 * level;
			level++;
			stat_points += 5;
		}
	}

	void recalculate_stats()
	{
		// recalcul needed //
		MaxHp = 5 * CON;
		minDamage = STR / 2;
		maxDamage = minDamage + 4;
		HitChance = 75 + AGI - enemy.my_agi;
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

	public void update_HP()
	{
		float tmp_hp = HP;
		hero_hp.text = Mathf.Round (tmp_hp).ToString () + "/" + Mathf.Round (MaxHp);
	}

	public void show_hud(int which)
	{
		float tmp_hero_lvl = level;
		hero_lvl.text = tmp_hero_lvl.ToString ();

		if (which == 0) {
			if (!target_hp.enabled)
				target_hp.enabled = true;
			if (!target_img.enabled)
				target_img.enabled = true;
			if (!target_lvl.enabled)
				target_lvl.enabled = true;

			float tmp_target_hp = current_target.GetComponent<enemy>().HP;
			target_hp.text = Mathf.Round(tmp_target_hp).ToString() + "/" + Mathf.Round(enemy.MaxHp);
			float tmp_target_lvl = level;
			target_lvl.text = tmp_target_lvl.ToString();
		}
		else {
			target_hp.enabled = false;
			target_img.enabled = false;
			target_lvl.enabled = false;
		}
	}



	// COMBAT_STAT METHODS //

	void Update () {
		if (HP > 0) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hit;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit) && GUIUtility.hotControl == 0) {
					if (hit.collider.tag == "Enemy" && !hit.collider.isTrigger)
					{
						current_target = hit.collider.gameObject;
						Vector3 hit_point = new Vector3(hit.point.x, 0, hit.point.z);
						nav.SetDestination (hit_point);
					}
					else
						nav.SetDestination (hit.point);
					is_attacking = false;
					anim.SetBool ("attack", false);
					anim.SetBool ("run", true);
					anim.SetBool ("idle", false);
				}
			}
			if (nav.remainingDistance <= nav.stoppingDistance && nav.velocity.sqrMagnitude < 30 && !is_attacking) {
				anim.SetBool ("run", false);
				anim.SetBool ("idle", true);
			}
			if (nav.velocity.sqrMagnitude > 30 && nav.destination != Vector3.zero) {
				anim.SetBool ("run", true);
				anim.SetBool ("idle", false);
			}
			changeSpeed ();
		} else
			StartCoroutine (die ());
		update_HP ();
		update_xp_bar ();
		if (current_target)
			show_hud (0);
		else
			show_hud (1);
	}


	void OnGUI()
	{
		GUI.contentColor = Color.white;
		string stats = "Stats";
		if (stat_points > 0) {
			stats = "Stats(" + stat_points + ")";
		}
		if (GUI.Button (new Rect (Screen.width * 0.026f, Screen.height * 0.8f, 100, 70), stats)) {
			if (!Panel.activeSelf)
				Panel.SetActive(true);
			else if (Panel.activeSelf)
				Panel.SetActive(false);
		}
		if (Panel.activeSelf) {
			if (stat_points > 0) {
				if (GUI.Button (new Rect (Screen.width * 0.1f, Screen.height * 0.46f, 30, 30), "+")) {
					STR++;
					recalculate_stats();
					stat_points--;
				}
				if (GUI.Button (new Rect (Screen.width * 0.2f, Screen.height * 0.46f, 30, 30), "+")) {
					AGI++;
					recalculate_stats();
					stat_points--;
				}
				if (GUI.Button (new Rect (Screen.width * 0.3f, Screen.height * 0.46f, 30, 30), "+")) {
					CON++;
					recalculate_stats();
					stat_points--;
				}
			}
			GUIStyle myStyle = new GUIStyle();
			myStyle.fontSize = 22;
			myStyle.normal.textColor = Color.green;
			GUI.Label(new Rect (Screen.width * 0.03f, Screen.height * 0.46f, 30, 30), "STR: " + STR, myStyle);
			GUI.Label(new Rect (Screen.width * 0.135f, Screen.height * 0.46f, 30, 30), "AGI: " + AGI, myStyle);
			GUI.Label(new Rect (Screen.width * 0.230f, Screen.height * 0.46f, 30, 30), "CON: " + CON, myStyle);
			GUI.Label(new Rect (Screen.width * 0.03f, Screen.height * 0.50f, 30, 30), "HP: " + Mathf.Round(HP), myStyle);
			GUI.Label(new Rect (Screen.width * 0.135f, Screen.height * 0.50f, 30, 30), "MaxHp: " + MaxHp, myStyle);
			GUI.Label(new Rect (Screen.width * 0.230f, Screen.height * 0.50f, 30, 30), "minDmg: " + Mathf.Round(minDamage), myStyle);
			GUI.Label(new Rect (Screen.width * 0.03f, Screen.height * 0.54f, 30, 30), "maxDmg: " + Mathf.Round(maxDamage), myStyle);
			GUI.Label(new Rect (Screen.width * 0.135f, Screen.height * 0.54f, 30, 30), "Level: " + level, myStyle);
			GUI.Label(new Rect (Screen.width * 0.230f, Screen.height * 0.54f, 30, 30), "XP: " + Mathf.Round(XP), myStyle);
			GUI.Label(new Rect (Screen.width * 0.03f, Screen.height * 0.58f, 30, 30), "next_lvl: " + Mathf.Round(XpToNextLevel), myStyle);
			GUI.Label(new Rect (Screen.width * 0.135f, Screen.height * 0.58f, 30, 30), "money: " + money, myStyle);
			GUI.Label(new Rect (Screen.width * 0.230f, Screen.height * 0.58f, 30, 30), "HitChance: " + Mathf.Round(HitChance), myStyle);
		}
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "Enemy" && !coll.isTrigger && nav.velocity.sqrMagnitude < 30) {
			if (!current_target)
				current_target = coll.gameObject;
			is_attacking = true;
			anim.SetBool ("run", false);
			anim.SetBool ("idle", false);
			if (!is_attacking_anim)
				StartCoroutine (attack (coll));
			transform.eulerAngles = -coll.gameObject.transform.eulerAngles;
		}
	}

	IEnumerator attack(Collider coll)
	{
		anim.SetBool ("attack", true);
		is_attacking_anim = true;
		yield return new WaitForSeconds(0.4f);
		if (nav.velocity.sqrMagnitude < 30 && anim.GetBool ("attack")) {
			if (Random.Range (0, 100f) <= HitChance)
				current_target.SendMessage ("get_hit", get_dmg());
		}
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

	IEnumerator heal()
	{
		while (HP > 0) {
			yield return new WaitForSeconds(1f);
			if (!is_attacking && HP < MaxHp)
				HP++;
		}
	}

	IEnumerator die()
	{
		anim.SetBool ("run", false);
		anim.SetBool ("idle", false);
		anim.SetBool ("attack", false);
		anim.SetBool ("dead", true);
		yield return new WaitForSeconds (5f);
		Application.LoadLevel (Application.loadedLevel);
	}


	// NavMeshAgent methods //
	void changeSpeed()
	{
		float speedMultiplyer = 1.0f - 0.9f*Vector3.Angle(transform.forward, nav.steeringTarget-transform.position)/180.0f;
		nav.speed = 20 *speedMultiplyer;
	}
	// NavMeshAgent methods //


	// MESSAGE METHODS //
	public void ZombieDying()
	{
		calculate_xp_money (enemy.my_xp, enemy.my_money);
		Collider[] coll = Physics.OverlapSphere(transform.position, 5f);
		int i = 0;
		while (i < coll.Length) {
			if (coll [i].tag == "Enemy")
			{
				current_target = coll[i].gameObject;
				break;
			}
			i++;
		}
		if (i >= coll.Length) {
			current_target = null;
			is_attacking = false;
			anim.SetBool ("attack", false);
		}
	}
}