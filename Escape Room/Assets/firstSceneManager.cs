using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class firstSceneManager : MonoBehaviour
{
    public GameObject door;
    public bool doorOpening;
    public float doorSpeed = 1f;
    public XRKnob doorLeverKnobScript;

    [Header("Conditions For Door")]
    public bool ringOn;
    public bool canFlipLever;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkOpenDoor();
    }

    void checkOpenDoor(){
        
        if (doorLeverKnobScript.value <= 0.15f && !doorOpening)
            doorOpening = true;

        if (doorOpening)
            if (door.transform.localPosition.y < 3.31f)
                door.transform.Translate(Vector3.up * Time.deltaTime * doorSpeed);
                
    }

    public void openDoor(){
        doorOpening = true;
    }
}
