using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour {

	public static string playerPrefName = "soundMute";

	public AudioClip damage;
	public AudioClip fire;
	public AudioClip gameover;
	public AudioClip jump;
	public AudioClip pickup;

	public AudioSource theSource;

	public static SoundController theController;

	public bool muted;
	public Sprite muteSprite;
	public Sprite nonMutedSprite;

	void Awake() {
		DontDestroyOnLoad(gameObject);
		SoundController.theController = this;
		theSource = gameObject.GetComponent<AudioSource>();

		if(PlayerPrefs.HasKey(playerPrefName)) {
			muted = PlayerPrefs.GetInt(playerPrefName) != 0;
		} else {
			muted = false;
			PlayerPrefs.SetInt(playerPrefName, 0);
		}
	}

	public void playSound(AudioClip theSound) {
		if(!muted) {
			theSource.clip = theSound;
			theSource.Play();
		}
	}

	public void toggleMute(GameObject imageToUpdate) {
		muted = !muted;
		PlayerPrefs.SetInt(playerPrefName, muted ? 1 : 0);

		if(imageToUpdate != null) {
			updateSprite(imageToUpdate);
		}

		if(muted) {
			theSource.Stop();
		}
	}

	public void updateSprite(GameObject who) {
		who.GetComponent<SpriteRenderer>().sprite = muted ? muteSprite : nonMutedSprite;
	}
}