using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkForPlayerInside : MonoBehaviour
{
    public bool playerInside;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerInside = false;
    }
}
