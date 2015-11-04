using UnityEngine;
using System.Collections;

public class playerScript_ex01 : MonoBehaviour {
	
	public GameObject[] 	obj;
	private int				player;
	public 	int				startPlayer;
	public Rigidbody2D		me;
	public Vector3 			oriPos;
	public float			speed;
	public int				jump;
	void Start () {
		Physics2D.gravity = new Vector2 (0, -5);
		me = GetComponent<Rigidbody2D> ();
		this.player = 0;
		set_stats ();
		this.oriPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("1")) {
			this.player = 0;
			set_stats();
		}
		if (Input.GetKeyDown ("2")) {
			this.player = 1;
			set_stats();
		}
		if (Input.GetKeyDown ("3")) {
			this.player = 2;
			set_stats();
		}
		if (Input.GetKeyDown ("r"))
			restore ();
		
	}
	void FixedUpdate() {
		if (Input.GetKey("d") && this.startPlayer == this.player) {
			this.gameObject.transform.localPosition += new Vector3(0.15f - this.speed , 0, 0);
		}
		if (Input.GetKey("a") && this.startPlayer == this.player) {
			this.gameObject.transform.localPosition -= new Vector3(0.15f - this.speed , 0, 0);
		}
		if (Input.GetKeyDown("space") && me.velocity == Vector2.zero && this.startPlayer == this.player) {
			me.AddForce(Vector3.up * (200 - this.jump));
		}
		
	}
	public void restore()
	{
		transform.localPosition = this.oriPos;
		this.player = 0;
	}

	public void set_stats()
	{
		if (this.player == 0) {
			this.speed = 0.05f;
			this.jump = 50;
		}
		if (this.player == 1) {
			this.speed = -0.05f;
			this.jump = -50;
		}
		if (this.player == 2) {
			this.speed = 0;
			this.jump = 0;
		}
	}
}