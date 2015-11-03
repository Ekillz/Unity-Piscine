using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {

	// Use this for initialization
	public bool loose;
	public float speed;
	public int score;
	void Start () {
		this.score = 0;
		this.speed = 0;
		this.loose = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.loose) {
			transform.localPosition -= new Vector3 (0.2f + this.speed, 0, 0);
			if (transform.localPosition.x < -7) {
				this.speed += 0.1f;
				this.score += 5;
				float y = transform.localPosition.y;
				transform.localPosition = new Vector3 (7.5f, y, 0);
			}
		}
	}
	public void set_loose(bool loose)
	{
		this.loose = true;
	}
}
