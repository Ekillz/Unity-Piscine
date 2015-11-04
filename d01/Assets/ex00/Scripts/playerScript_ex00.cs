using UnityEngine;
using System.Collections;

public class playerScript_ex00 : MonoBehaviour {

	public GameObject[] 	obj;
	private int				player;
	public 	int				startPlayer;
	public Rigidbody2D		me;
	public Vector3 			oriPos;

	void Start () {

		Physics2D.gravity = new Vector2 (0, -5);
		me = GetComponent<Rigidbody2D> ();
		this.player = 0;
		this.oriPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("1")) {
			this.player = 0;
		}
		if (Input.GetKeyDown ("2")) {
			this.player = 1;
		}
		if (Input.GetKeyDown ("3")) {
			this.player = 2;
		}
		if (Input.GetKeyDown ("r"))
			restore ();
			
	}
	void FixedUpdate() {
		if (Input.GetKey("d") && this.startPlayer == this.player) {
			this.gameObject.transform.localPosition += new Vector3(0.15f, 0, 0);
		}
		if (Input.GetKey("a") && this.startPlayer == this.player) {
			this.gameObject.transform.localPosition -= new Vector3(0.15f, 0, 0);
		}
		if (Input.GetKeyDown("space") && me.velocity == Vector2.zero && this.startPlayer == this.player) {
			me.AddForce(Vector3.up * 200);
		}

	}
	public void restore()
	{
		transform.localPosition = this.oriPos;
		this.player = 0;
	}
}