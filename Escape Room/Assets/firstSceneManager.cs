using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.SceneManagement;

public class firstSceneManager : MonoBehaviour
{
    public GameObject door;
    public GameObject player;
    public bool doorOpening;
    public float doorSpeed = 1f;
    public XRKnob doorLeverKnobScript;

    [Header("Conditions For Door")]
    public bool ringOn;
    public bool canFlipLever;
    [Header("Stuff")]
    
    public GameObject correctCylinder;
    public GameObject ring;
    Rigidbody ringRb;


    void Start()
    {
        ringRb = ring.GetComponent<Rigidbody>();
    }
    

    void Update()
    {
        checkCondition();
        freezeLever();
        checkOpenDoor();
        
        if (player.transform.position.x < -2.1f)
            sceneChange();
    }

    void checkCondition(){
        ringOn = Vector3.Distance(ring.transform.position, correctCylinder.transform.position) < 0.15f && ringRb.isKinematic;

        canFlipLever = ringOn;
    }

    void freezeLever(){
        if (!canFlipLever)
            doorLeverKnobScript.value = Mathf.Clamp(doorLeverKnobScript.value, 0.8f, 1f);
        
    }

    void checkOpenDoor(){
        
        if (doorLeverKnobScript.value <= 0.15f && !doorOpening)
            doorOpening = true;

        if (doorOpening)
            if (door.transform.localPosition.y < 3.31f)
                door.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed);
                
    }

    void sceneChange(){
        Debug.Log("CHanging scenes");
    }

    public void openDoor(){
        doorOpening = true;
    }
}
