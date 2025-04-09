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

   public void PlaySound(int barNumber)
{
    switch (barNumber)
    {
        case 1:
            SoundManager.Instance.Play(SoundType.NOTE_C);
            break;
        case 2:
            SoundManager.Instance.Play(SoundType.NOTE_D);
            break;
        case 3:
            SoundManager.Instance.Play(SoundType.NOTE_E);
            break;
        case 4:
            SoundManager.Instance.Play(SoundType.NOTE_F);
            break;
        case 5:
            SoundManager.Instance.Play(SoundType.NOTE_G);
            break;
        default:
            Debug.LogWarning("Unknown bar number: " + barNumber);
            break;
    }
}

    
}
