using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	// Use this for initialization
	public Pipe pipe;
	public bool loose;

	void Start () {
		this.loose = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.loose) {
			if (transform.localPosition.y > -3.3)
				transform.localPosition -= new Vector3 (0, 0.15f, 0);
			else {
				this.loose = true;
				pipe.set_loose (true);
			}
			if (Input.GetKeyDown ("space"))
				transform.localPosition += new Vector3 (0, 1.0f, 0);
			if (check_hit()) {
				this.loose = true;
				pipe.set_loose (true);
				Debug.Log ("Score: " + pipe.score + "\n" + "Time: " +  Mathf.RoundToInt(Time.unscaledTime) + "s");
			}
		}
	}

	public bool check_hit()
	{
		if ((transform.localPosition.x + (transform.localScale.x / 2) > (this.pipe.transform.localPosition.x - (this.pipe.transform.localScale.x / 2)) && transform.localPosition.x - (transform.localScale.x / 2) < (this.pipe.transform.localPosition.x + (this.pipe.transform.localScale.x / 2))) && 
			(transform.localPosition.y > 1.2f || transform.localPosition.y < -0.1f))
			return true;
		return false;
	}
}