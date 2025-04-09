using UnityEngine;
using System;

public class Notes : MonoBehaviour
{
    public static event Action<int> OnBarHit = delegate {};
    public int barNumber;

    private void OnTriggerEnter(Collider other)
    {
        Transform root = other.transform.root;
        if (root.CompareTag("Mallet"))
        {
            OnBarHit(barNumber);
        }
    }
    
}
