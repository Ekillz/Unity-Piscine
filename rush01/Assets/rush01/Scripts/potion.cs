using UnityEngine;
using System.Collections;

public class potion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider coll)
	{
		controller_maya maya = controller_maya.instance;
		if (coll.tag == "Player") {
			float tmp_hp = maya.HP + (maya.MaxHp / 100) * 30;
			if (tmp_hp > maya.MaxHp)
				maya.HP = maya.MaxHp;
			else
				maya.HP += (maya.MaxHp / 100) * 30;
			Destroy(gameObject);
		}
	}
}
