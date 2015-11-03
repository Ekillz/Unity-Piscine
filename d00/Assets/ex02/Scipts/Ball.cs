using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public int speed;
	public int direc;
	public bool verbose;
	public Vector3 force;
	public int score;
	public bool win;
	// Use this for initialization
	void Start () {
		this.verbose = false;
		this.speed = -1;
		this.score = -15;
		this.win = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.speed > 0)
		{
			if (transform.localPosition.y > 13.5f && transform.localPosition.y < 15f && this.speed <= 12)
			{
				transform.localPosition += new Vector3(0, 100f, 0);
				this.win = true;
			}
			this.force = new Vector3(0, this.speed * 0.1f, 0);
			if (transform.localPosition.y < 16.3f && !this.verbose)
				this.direc = 1;
			else if (transform.localPosition.y > 16.3f)
			{
				this.direc = 0;
				this.verbose = true;
			}
			else if (transform.localPosition.y <= -8.5f)
			{
				this.direc = 1;
				this.verbose = false;
			}
			if  (this.direc == 1)
				transform.localPosition += this.force;
			else
				transform.localPosition -= this.force;
			this.speed--;
		}
		if (this.speed == 0) {
			if (!this.win)
				this.score += 5;
			if (!this.win)
				Debug.Log (this.score);
			this.verbose = false;
			this.speed = -1;
		}
	}

	public void set_speed(int speed)
	{
		this.speed = speed;
	}

	public int get_speed()
	{
		return this.speed;
	}

	public Vector3 get_pos()
	{
		return transform.localPosition - new Vector3 (0.7f, 0.2f, 0);
	}
}
