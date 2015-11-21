using UnityEngine;
using System.Collections;

public class document : MonoBehaviour {



	public AudioSource win_music;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay(Collider coll)
	{
		if (Input.GetKeyDown ("e")) {
			StartCoroutine(win());
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		slider.obj.show_doc_text (0);
	}
	void OnTriggerExit(Collider coll)
	{
		slider.obj.show_doc_text (1);
	}

	public IEnumerator win()
	{
		slider.obj.show_doc_text (1);
		win_music.Play ();
		slider.obj.show_win_text (0);
		yield return new WaitForSeconds(4.5f);
		slider.obj.show_win_text (1);
		Application.LoadLevel(Application.loadedLevel);
	}
}
