using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {
 
	public Vector3 speed;
	public float distY;
	public Vector3 startPos;
	void Start () {
		this.speed = new Vector3(0, Random.Range(0.35f, 0.5f), 0);
		this.distY = -3;
		this.startPos = transform.localPosition;
		this.startPos[1] -= this.distY;
	}
	
	void Update () {
		transform.localPosition -= this.speed;
		if (transform.localPosition.y <= -6.5f)
			GameObject.Destroy (this.gameObject);

		if (Input.GetKeyDown ("a") && gameObject.name == "keyA(Clone)" && transform.localPosition.y >= -1.5) {
			Debug.Log (transform.localPosition.y);
			GameObject.Destroy(this.gameObject);
		}
		if (Input.GetKeyDown ("s") && gameObject.name == "keyS(Clone)" && transform.localPosition.y >= -1.5) {
			Debug.Log (transform.localPosition.y);
			GameObject.Destroy (this.gameObject);
		}
			
		if (Input.GetKeyDown ("d") && gameObject.name == "keyD(Clone)" && transform.localPosition.y >= -1.5) {
			Debug.Log (transform.localPosition.y);
			GameObject.Destroy (this.gameObject);
		}
	}	
}
