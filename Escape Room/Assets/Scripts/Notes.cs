using UnityEngine;
using System;

public class Notes : MonoBehaviour
{
    public static event Action<int> OnBarHit = delegate { };

    public int barNumber;
    public float hitCooldown = 0.2f; // Cooldown duration shared by all notes

    private static float nextGlobalValidHitTime = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mallet"))
        {
            float currentTime = Time.time;

            if (currentTime >= nextGlobalValidHitTime)
            {
                var audioSrc = GetComponent<AudioSource>();
                audioSrc?.Play();

                Debug.Log("Playing note: " + barNumber);
                OnBarHit(barNumber);

                nextGlobalValidHitTime = currentTime + hitCooldown;
            }
        }
    }

    public static void SimulateBarHit(int barNumber)
    {
        OnBarHit?.Invoke(barNumber);
    }
}
