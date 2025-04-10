
using UnityEngine;

public class makeNoiseOnImpact : MonoBehaviour
{   
    
    public AudioClip Sound;
    public float VolumeScale = 1f;
    public float PitchScale = 1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime / rb.mass;
        playSound();
    }


    void playSound(){
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        audioSource.clip = Sound;
        audioSource.volume = VolumeScale;
        audioSource.pitch = PitchScale; // Adjusts speed and pitch
        audioSource.Play();
        Destroy(tempAudio, Sound.length / PitchScale); // Adjust cleanup time based on speed
    }

}