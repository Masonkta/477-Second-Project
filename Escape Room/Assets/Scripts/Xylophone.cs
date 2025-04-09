using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xylophone : MonoBehaviour
{
    public AudioClip[] audioClip;
    private AudioSource audioSource;

    private void Start(){
        Notes.OnBarHit += PlaySound;
        audioSource = GetComponent<AudioSource> ();
    }

    private void OnDestroy(){
        Notes.OnBarHit -= PlaySound;
    }

    public void PlaySound(int barNumber){

        int index = barNumber - 1;

      if (index >= 0 && index < audioClip.Length)
        {
            audioSource.PlayOneShot(audioClip[index]);
        }
    }
    
}
