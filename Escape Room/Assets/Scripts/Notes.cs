using UnityEngine;
using System;


public class Notes : MonoBehaviour
{
    public static event Action<int> OnBarHit = delegate {};
    public int barNumber;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Mallet"))
        {
           Debug.Log("Playing note: " + barNumber);
            OnBarHit(barNumber);
        }
    }
    
}
