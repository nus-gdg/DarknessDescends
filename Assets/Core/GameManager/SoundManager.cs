using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDG
{
    // Credit: Jeremy Choo
    public class SoundManager : Manager<SoundManager>
    {
        public static string playerPrefName = "soundMute";
        [SerializeField]
        private AudioClip damage;
        [SerializeField]
        private AudioClip fire;
        [SerializeField]
        private AudioClip gameover;
        [SerializeField]
        private AudioClip jump;
        [SerializeField]
        private AudioClip pickup;
        [SerializeField]
        private AudioClip bolt;
        [SerializeField]
        private AudioClip shield;

        public void PlayDamage() { PlaySound(damage); }
        public void PlayFire() { PlaySound(fire); }
        public void PlayGameOver() { PlaySound(gameover); }
        public void PlayJump() { PlaySound(jump); }
        public void PlayPickup() { PlaySound(pickup); }
        public void PlayBolt() { PlaySound(bolt); }
        public void PlayShield() { PlaySound(shield); }

        [SerializeField]
        private AudioSource theSource;

        [SerializeField]
        private bool muted;
        [SerializeField]
        private Sprite muteSprite;
        [SerializeField]
        private Sprite nonMutedSprite;

        void Start()
        {
            theSource = gameObject.GetComponent<AudioSource>();
            if (PlayerPrefs.HasKey(playerPrefName))
            {
                muted = PlayerPrefs.GetInt(playerPrefName) != 0;
            }
            else
            {
                muted = false;
                PlayerPrefs.SetInt(playerPrefName, 0);
            }
        }

        public void PlaySound(AudioClip theSound)
        {
            if (!muted)
            {
                theSource.clip = theSound;
                theSource.Play();
            }
        }

        public void PlayUninterruptedSound(AudioClip theSound)
        {
            if (!muted)
            {
                theSource.PlayOneShot(theSound, 1);
            }
        }

        public void ToggleMute(GameObject imageToUpdate)
        {
            muted = !muted;
            PlayerPrefs.SetInt(playerPrefName, muted ? 1 : 0);

            if (imageToUpdate != null)
            {
                UpdateSprite(imageToUpdate);
            }

            if (muted)
            {
                theSource.Stop();
            }
        }

        private void UpdateSprite(GameObject who)
        {
            who.GetComponent<SpriteRenderer>().sprite = muted ? muteSprite : nonMutedSprite;
        }
    }
}