using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ChessSoundType
{
    CLICK,
    SUCCESS,
    HOVER,
}

public class ChessSoundCollection
{
    private AudioClip[] clips;
    private int lastClipIndex;

    public ChessSoundCollection(params string[] clipNames)
    {
        this.clips = new AudioClip[clipNames.Length];
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i] = Resources.Load<AudioClip>(clipNames[i]);
            if (clips[i] == null)
            {
                Debug.Log($"can't find audio clip {clipNames[i]}");
            }
        }
        lastClipIndex = -1;
    }

    public AudioClip GetRandClip()
    {
        if (clips.Length == 0)
        {
            Debug.Log("No clips to give");
            return null;
        }
        else if (clips.Length == 1)
        {
            return clips[0];
        }
        else
        {
            int index = lastClipIndex;
            while (index == lastClipIndex)
            {
                index = Random.Range(0, clips.Length);
            }
            lastClipIndex = index;
            return clips[index];
        }
    }

}

public class ChessSoundManager : MonoBehaviour
{
    public float mainVolume = 1.0f;
    private Dictionary<ChessSoundType, ChessSoundCollection> sounds;
    private AudioSource audioSrc;

    public static ChessSoundManager Instance { get; private set; }

    // unity life cycle
    private void Awake()
    {
        Instance = this;
        audioSrc = GetComponent<AudioSource>();
        sounds = new Dictionary<ChessSoundType, ChessSoundCollection> {
    { ChessSoundType.CLICK, new ChessSoundCollection("audio/Button_22_click", "audio/Button_14_hover") },
    { ChessSoundType.SUCCESS, new ChessSoundCollection("audio/solved") },
    

    // xylophone notes
        };

    }


    public void Play(ChessSoundType type, AudioSource audioSrc = null)
    {
        if (sounds.ContainsKey(type))
        {
            audioSrc ??= this.audioSrc;
            audioSrc.volume = Random.Range(0.70f, 1.0f) * mainVolume;
            audioSrc.pitch = Random.Range(0.75f, 1.25f);
            audioSrc.clip = sounds[type].GetRandClip();
            audioSrc.Play();
        }
    }
}
