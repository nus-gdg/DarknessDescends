using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
// Credit: Jeremy Choo
public class SoundManager : Manager<SoundManager>
{
    public static string playerPrefName = "soundMute";

    public AudioClip damage;
    public AudioClip fire;
    public AudioClip gameover;
    public AudioClip jump;
    public AudioClip pickup;
    public AudioClip bolt;
    public AudioClip shield;

    public AudioSource theSource;

    public bool muted;
    public Sprite muteSprite;
    public Sprite nonMutedSprite;

    void Start()
    {
        theSource = gameObject.GetComponent<AudioSource>();
        if(PlayerPrefs.HasKey(playerPrefName))
        {
            muted = PlayerPrefs.GetInt(playerPrefName) != 0;
        } else
        {
            muted = false;
            PlayerPrefs.SetInt(playerPrefName, 0);
        }
    }

    public void PlaySound(AudioClip theSound)
    {
        if(!muted)
        {
            theSource.clip = theSound;
            theSource.Play();
        }
    }

    public void playUninterruptedSound(AudioClip theSound)
    {
        if(!muted)
        {
            theSource.PlayOneShot(theSound, 1);
        }
    }

    public void toggleMute(GameObject imageToUpdate)
    {
        muted = !muted;
        PlayerPrefs.SetInt(playerPrefName, muted ? 1 : 0);

        if(imageToUpdate != null)
        {
            updateSprite(imageToUpdate);
        }

        if(muted)
        {
            theSource.Stop();
        }
    }

    public void updateSprite(GameObject who)
    {
        who.GetComponent<SpriteRenderer>().sprite = muted ? muteSprite : nonMutedSprite;
    }
}
}