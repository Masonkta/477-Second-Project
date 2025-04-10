using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBounds : MonoBehaviour
{
    public GameObject BKOrigin;
    public GameObject BROrigin;
    public GameObject BPOrigin;
    public GameObject WKOrigin;
    public GameObject WROrigin;
    public GameObject WPOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {/*
        if (other.CompareTag("BlackPawn")){
            print("BLACK PAWN GONE!");
        }
        */
    }
}
