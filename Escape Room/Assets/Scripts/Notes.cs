using UnityEngine;
using System;
using System.Collections;


public class Notes : MonoBehaviour
{
    public static event Action<int> OnBarHit = delegate {};
    public int barNumber;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Mallet"))
        {
        var audioSrc = GetComponent<AudioSource>();
        audioSrc?.Play(); 
           Debug.Log("Playing note: " + barNumber);
           OnBarHit(barNumber);
        }
    }
    public static void SimulateBarHit(int barNumber)
{
    OnBarHit?.Invoke(barNumber);
}

    
}
