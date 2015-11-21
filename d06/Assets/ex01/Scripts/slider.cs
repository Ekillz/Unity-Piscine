using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class slider : MonoBehaviour {

	public Slider progress;
	public bool verbose = false;
	public int direc = 1;
	public static slider obj { get; set;}
	public bool detect = false;
	public bool key = false;

	public GameObject audio0;
	public GameObject audio1;
	public GameObject audio2;
	public AudioSource alert_sound;
	public AudioSource normal_music;	
	public AudioSource alert_music;	

	void Start () {

		progress = GetComponent<Slider> ();
		obj = this;
		alert_sound = audio0.GetComponent<AudioSource> ();
		alert_sound.enabled = false;
		normal_music = audio1.GetComponent<AudioSource> ();
		normal_music.enabled = false;
		alert_music = audio2.GetComponent<AudioSource> ();
		alert_music.enabled = false;
		StartCoroutine (start ());
	}

	public IEnumerator start()
	{
		Text[] txt = GetComponentsInChildren<Text>();
		txt[5].enabled = true;
		yield return new WaitForSeconds (3);
		txt[5].enabled = false;

	}

	public IEnumerator loose()
	{
		Text[] txt = GetComponentsInChildren<Text>();
		txt[6].enabled = true;
		yield return new WaitForSeconds (1);
		txt[6].enabled = false;
		Application.LoadLevel (Application.loadedLevel);
	}

	// Update is called once per frame
	void Update () {
		if (progress.value >= 0.75f && progress.value < 1f) {
			if (!alert_sound.isActiveAndEnabled) {
				normal_music.enabled = false;
				alert_sound.enabled = true;
				alert_music.enabled = true;
				alert_sound.Play ();
				alert_music.Play ();
			}
		} else if (progress.value >= 1f)
			StartCoroutine (loose ());
		else {
			alert_sound.enabled = false;
			alert_music.enabled = false;
			if (!normal_music.isActiveAndEnabled)
			{
				normal_music.enabled = true;
				normal_music.Play();
			}
		}
		if (!detect)
			progress.value -= 0.0015f;
	}

	public void show_key_text(int which)
	{
		Text[] txt = GetComponentsInChildren<Text>();
		if (which == 0) {
			txt[0].enabled = true;
		}
		else
			txt[0].enabled = false;
	}

	public void show_door_text(int which)
	{
		Text[] txt = GetComponentsInChildren<Text>();
		if (key) {
			if (which == 0) {
				txt [1].enabled = true;
			} else
				txt [1].enabled = false;
		}
		else {
			if (which == 0) {
				txt [2].enabled = true;
			} else
				txt [2].enabled = false;
		}
	}

	public void show_doc_text(int which)
	{
		Text[] txt = GetComponentsInChildren<Text>();
		if (which == 0) {
			txt[3].enabled = true;
		} else
			txt[3].enabled = false;
	}

	public void show_win_text(int which)
	{
		Text[] txt = GetComponentsInChildren<Text>();
		if (which == 0) {
			txt[4].enabled = true;
		} else
			txt[4].enabled = false;
	}
}