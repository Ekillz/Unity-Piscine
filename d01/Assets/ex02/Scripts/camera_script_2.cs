using UnityEngine;
using System.Collections;

public class camera_script_2: MonoBehaviour {


	public GameObject[] players;
	public int			cameraPos;
	void Start () {
		this.cameraPos = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1"))
			this.cameraPos = 0;
		if (Input.GetKeyDown ("2"))
			this.cameraPos = 1;
		if (Input.GetKeyDown ("3"))
			this.cameraPos = 2;
		if (Input.GetKeyDown ("r"))
			this.cameraPos = 0;
	}

	void LateUpdate() {
		if (this.cameraPos == 0)
			this.transform.position = new Vector3(players[0].transform.position.x,
			                                      players[0].transform.position.y + 2,
			                                      players[0].transform.position.z - 1);
		else if (this.cameraPos == 1)
			this.transform.position = new Vector3(players[1].transform.position.x,
			                                      players[1].transform.position.y + 2,
			                                      players[1].transform.position.z - 1);
		else if (this.cameraPos == 2)
			this.transform.position = new Vector3(players[2].transform.position.x,
			                                      players[2].transform.position.y + 2,
			                                      players[2].transform.position.z - 1);
	}
}
