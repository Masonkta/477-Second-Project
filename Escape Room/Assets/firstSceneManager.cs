using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class firstSceneManager : MonoBehaviour
{
    public GameObject door;
    public checkForPlayerInside playerInsideTemple;
    public GameObject player;
    public bool doorOpening;
    public float doorSpeed = 1f;
    public bool teleporting = false;
    bool teleported = false;
    public Image fadeOutScreen;

    [Header("Conditions For Door")]
    public bool coinOn;
    public bool needleInCorrectPosition;
    public bool cubeInCorrectPos;
    public bool canFlipLever;
    [Header("Stuff")]
    
    public GameObject correctHook;
    public GameObject coin;
    public XRKnob doorLeverKnobScript;
    public XRKnob needleKnobScript;
    public Transform correctSpotForCube;
    public GameObject handHeldCube;
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
        
        if (teleporting)
            sceneChange();
    }

    void checkCondition(){
        // Debug.Log();
        coinOn = Vector3.Distance(coin.transform.position, correctHook.transform.position) < 0.16f && coinRb.isKinematic;
        needleInCorrectPosition = needleKnobScript.value > 0f ? (needleKnobScript.value % 1f) > 0.75f && (needleKnobScript.value % 1f) <= 0.875f : (-needleKnobScript.value % 1f) < 0.25f && (-needleKnobScript.value % 1f) >= 0.125f;
        cubeInCorrectPos = Vector3.Distance(handHeldCube.transform.position, correctSpotForCube.position) < 0.1f;

        canFlipLever = coinOn & needleInCorrectPosition && cubeInCorrectPos;

        teleporting = playerInsideTemple.playerInside;
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
        
        if (fadeOutScreen.color.a < 1f){
            Color temp = fadeOutScreen.color;
            temp.a += Time.deltaTime * 1 / 0.3f;
            fadeOutScreen.color = temp;
        }

        if (fadeOutScreen.color.a >= 1f && !teleported){
            teleported = true;
            SceneManager.LoadScene("Logan (light puzzle)");
        }

    }


}
