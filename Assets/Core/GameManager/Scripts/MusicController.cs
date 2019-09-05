using UnityEngine;
using System.Collections;

//Handle music
public class MusicController : MonoBehaviour 
{
	public static string playerPrefName = "musicMute";

	public AudioClip[] genericMusic;

	public AudioSource theSource;

	public static MusicController theController;

	public bool muted;
	public Sprite muteSprite;
	public Sprite nonMutedSprite;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		MusicController.theController = this;
		theSource = gameObject.GetComponent<AudioSource>();

		if(PlayerPrefs.HasKey(playerPrefName)) {
			muted = PlayerPrefs.GetInt(playerPrefName) != 0;
		} else {
			muted = false;
			PlayerPrefs.SetInt(playerPrefName, 0);
		}

		PlayRandomMusic();
	}

	public void PlayMusic(AudioClip music, float volume = 1.0f)
	{
		if(!muted) {
			theSource.Stop();
			theSource.clip = music;
			theSource.Play();
		}
	}

	public void PlayRandomMusic(float volume = 1.0f) {
		int randomNumber = Random.Range(0, genericMusic.Length);
		Debug.Log(genericMusic.Length + " - " + randomNumber);
		PlayMusic(genericMusic[randomNumber], volume);
	}

	public void toggleMute(GameObject imageToUpdate) {
		muted = !muted;
		PlayerPrefs.SetInt(playerPrefName, muted ? 1 : 0);

		if(imageToUpdate != null) {
			updateSprite(imageToUpdate);
		}

		if(muted) {
			theSource.Stop();
		} else {
			PlayRandomMusic();
		}
	}

	public void updateSprite(GameObject who) {
		who.GetComponent<SpriteRenderer>().sprite = muted ? muteSprite : nonMutedSprite;
	}
}