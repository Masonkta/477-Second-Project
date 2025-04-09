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
    public XRKnob needleKnobScript;

    [Header("Conditions For Door")]
    public bool coinOn;
    public bool needleInCorrectPosition;
    public bool canFlipLever;
    [Header("Stuff")]
    
    public GameObject correctHook;
    public GameObject coin;
    Rigidbody coinRb;


    void Start()
    {
        coinRb = coin.GetComponent<Rigidbody>();
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
        // Debug.Log(Vector3.Distance(coin.transform.position, correctHook.transform.position));
        coinOn = Vector3.Distance(coin.transform.position, correctHook.transform.position) < 0.16f && coinRb.isKinematic;
        needleInCorrectPosition = needleKnobScript.value > 0f ? (needleKnobScript.value % 1f) > 0.75f : (-needleKnobScript.value % 1f) < 0.25f;

        canFlipLever = coinOn & needleInCorrectPosition;
    }

    void freezeLever(){
        if (!canFlipLever)
            doorLeverKnobScript.value = Mathf.Clamp(doorLeverKnobScript.value, 0.8f, 1f);
        
    }

    void checkOpenDoor(){
        
        if (doorLeverKnobScript.value <= 0.15f && !doorOpening)
            doorOpening = true;

        if (doorOpening)
            if (door.transform.localPosition.y > -1.95f)
                door.transform.Translate(door.transform.up * -1f * Time.deltaTime * doorSpeed);
        
                
    }

    void sceneChange(){
        Debug.Log("CHanging scenes");
    }

    public void openDoor(){
        doorOpening = true;
    }
}
