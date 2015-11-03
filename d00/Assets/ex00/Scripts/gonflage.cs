using UnityEngine;
using System.Collections;

public class gonflage : MonoBehaviour {

	public float breath;
	public float time;
	public bool verbose;
	public int	breathMax;
	// Use this for initialization
	void Start () {
		this.breath = 0;
		this.time = -10;
		this.verbose = false;
		this.breathMax = 50;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown("space") && breath <= this.breathMax) {
			this.breath += 3;
			transform.localScale += new Vector3 (0.3f, 0, 0);
			transform.localScale += new Vector3 (0, 0.3f, 0);
		}
		if (Time.unscaledTime >= 20 && transform.localScale.x < 8) {
			string str = "Balloon life time: " + Mathf.RoundToInt(Time.unscaledTime) + "s";
			Debug.Log(str);
			GameObject.Destroy (this.gameObject);
		}
		if (this.breath >= this.breathMax && !verbose) {
			this.verbose = true;
			this.time = Time.unscaledTime;
		}
		if (Time.unscaledTime - this.time > 2 && Time.unscaledTime - this.time < 3) {
			this.breath = 0;
			this.verbose = false;
			this.time = 0;
		}

		if (!this.verbose && this.breath > 0)
			this.breath -= 0.5f;
		if (transform.localScale.x > 1 && transform.localScale.y > 1) {
			transform.localScale -= new Vector3 (0.05f, 0, 0);
			transform.localScale -= new Vector3 (0, 0.05f, 0);
		}
		if (transform.localScale.x > 9 && transform.localScale.y > 9) {
			GameObject.Destroy (this.gameObject);
			string str = "Balloon life time: " + Mathf.RoundToInt(Time.unscaledTime) + "s";
			Debug.Log(str);
		}


	}
}