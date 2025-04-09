using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum SoundType {
    CLICK,
    SOLVED,
    NOTE_C,
    NOTE_D,
    NOTE_E,
    NOTE_F,
    NOTE_G,
}

public class SoundCollection {
    private AudioClip[] clips;
    private int lastClipIndex;

    public SoundCollection(params string[] clipNames) {
        this.clips = new AudioClip[clipNames.Length];
        for (int i = 0; i < clips.Length; i++) {
            clips[i] = Resources.Load<AudioClip>(clipNames[i]);
            if (clips[i] == null) {
                Debug.Log($"can't find audio clip {clipNames[i]}");
            }
        }
        lastClipIndex = -1;
    }

    public AudioClip GetRandClip() {
        if (clips.Length == 0) {
            Debug.Log("No clips to give");
            return null;
        }
        else if (clips.Length == 1) {
            return clips[0];
        }
        else {
            int index = lastClipIndex;
            while (index == lastClipIndex) {
                index = Random.Range(0, clips.Length);
            }
            lastClipIndex = index;
            return clips[index];
        }
    }

}

public class SoundManager : MonoBehaviour {
    public float mainVolume = 1.0f;
    private Dictionary<SoundType, SoundCollection> sounds;
    private AudioSource audioSrc;

    public static SoundManager Instance { get; private set; }

    // unity life cycle
    private void Awake() {
        Instance = this;
        audioSrc = GetComponent<AudioSource>();
       sounds = new Dictionary<SoundType, SoundCollection> {
    { SoundType.CLICK, new SoundCollection("clicks/click_1", "clicks/click_2", "clicks/click_3", "clicks/click_4") },
    { SoundType.SOLVED, new SoundCollection("solved") },

    // xylophone notes
    { SoundType.NOTE_C, new SoundCollection("notes/C") },
    { SoundType.NOTE_D, new SoundCollection("notes/D") },
    { SoundType.NOTE_E, new SoundCollection("notes/E") },
    { SoundType.NOTE_F, new SoundCollection("notes/F") },
    { SoundType.NOTE_G, new SoundCollection("notes/G") }, 
        };

     }
    

    public void Play(SoundType type, AudioSource audioSrc = null) {
        if (sounds.ContainsKey(type)) {
            audioSrc ??= this.audioSrc;
            audioSrc.volume = Random.Range(0.70f, 1.0f) * mainVolume;
            audioSrc.pitch = Random.Range(0.75f, 1.25f);
            audioSrc.clip = sounds[type].GetRandClip();
            audioSrc.Play();
        }
    }
}
