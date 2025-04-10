using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attatchToPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = player.transform.Find("Camera Offset").transform.Find("Main Camera");   
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerCamera.position + playerCamera.forward * 12f;
        transform.rotation = Quaternion.LookRotation(transform.position - playerCamera.transform.position);
    }
}
