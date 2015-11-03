using UnityEngine;
using System.Collections;

public class Club : MonoBehaviour {

	public Ball ball;
	public int speed;
	public Vector3 startPos;
	public float timer;
	void Start () {
		this.speed = 0;
		transform.localPosition = ball.get_pos ();
		this.startPos = transform.localPosition;
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			transform.localPosition -= new Vector3(0, 0.1f, 0);
			this.speed++;
		}
		if (!Input.GetKey(KeyCode.Space) && this.speed > 0){
			transform.localPosition = this.startPos;
			ball.set_speed(this.speed);
			this.speed = -1;

		}
		if (ball.get_speed() == -1 && this.speed == -1) {
			transform.localPosition = ball.get_pos ();
			this.startPos = transform.localPosition;
		}
	}
}