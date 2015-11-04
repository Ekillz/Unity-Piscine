using UnityEngine;
using System.Collections;

public class ascenseur : MonoBehaviour {

	public Vector3 starPos;

	public Vector3 endPos;
	public bool verbose;
	public int direc;
	// Use this for initialization
	void Start () {
		this.starPos = transform.localPosition;
		this.endPos = this.starPos + new Vector3 (0, 8f, 0);
	}
	
	// Update is called once per frame
	void Update () {
//		if (transform.localPosition.y < this.endPos.y)
//			transform.localPosition += new Vector3 (0, 0.1f, 0);
//		if (transform.localPosition.y == this.starPos.y)

		if (transform.localPosition.y < this.endPos.y && !this.verbose)
			this.direc = 1;
		else if (transform.localPosition.y > this.endPos.y)
		{
			this.direc = 0;
			this.verbose = true;
		}
		else if (transform.localPosition.y <= this.starPos.y)
		{
			this.direc = 1;
			this.verbose = false;
		}
		if  (this.direc == 1)
			transform.localPosition += new Vector3(0, 0.1f, 0);
		else
			transform.localPosition -= new Vector3(0, 0.1f, 0);

	}
}
