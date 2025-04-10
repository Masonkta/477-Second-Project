using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SceneState {
    WAITING,
    READY_TO_OPEN,
    OPENING_DOOR,
    TELEPORTING
}

public class firstSceneManager : MonoBehaviour {
    public GameObject door;
    public checkForPlayerInside playerInsideTemple;
    public GameObject player;
    public float doorSpeed = 1f;
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

    private Rigidbody coinRb;
    private bool teleported = false;

    public SceneState currentState;

    private Dictionary<SceneState, Action> stateEnterMethods;
    private Dictionary<SceneState, Action> stateUpdateMethods;
    private Dictionary<SceneState, Action> stateExitMethods;
    private float lastValue;
    public AudioClip leverDink;
    public AudioClip doorOpening;
    private bool hasHit = false;
    void Start() {
        coinRb = coin.GetComponent<Rigidbody>();

        stateEnterMethods = new() {
            [SceneState.WAITING] = EnterWaiting,
            [SceneState.READY_TO_OPEN] = EnterReadyToOpen,
            [SceneState.OPENING_DOOR] = EnterOpeningDoor,
            [SceneState.TELEPORTING] = EnterTeleporting
        };

        stateUpdateMethods = new() {
            [SceneState.WAITING] = UpdateWaiting,
            [SceneState.READY_TO_OPEN] = UpdateReadyToOpen,
            [SceneState.OPENING_DOOR] = UpdateOpeningDoor,
            [SceneState.TELEPORTING] = UpdateTeleporting
        };

        stateExitMethods = new() {
            [SceneState.WAITING] = ExitWaiting,
            [SceneState.READY_TO_OPEN] = ExitReadyToOpen,
            [SceneState.OPENING_DOOR] = ExitOpeningDoor,
            [SceneState.TELEPORTING] = ExitTeleporting
        };

        ChangeState(SceneState.WAITING);
    }

    void Update() {
        stateUpdateMethods[currentState]?.Invoke();

        audioStuff();
        
    }

    void audioStuff(){
        float currentValue = doorLeverKnobScript.value;
        
        if (!canFlipLever && !hasHit && lastValue > 0.8f && currentValue <= 0.8f)
        {
            leverDinkSound();
            hasHit = true;
        }

        if (hasHit && currentValue > 0.81f)
        {
            hasHit = false;
        }

        lastValue = currentValue;
    }


    void leverDinkSound(){
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        audioSource.clip = leverDink;
        audioSource.volume = 0.25f;
        audioSource.Play();
        Destroy(tempAudio, leverDink.length);
    }
    void doorOpeningSound(){
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        audioSource.clip = doorOpening;
        audioSource.volume = 0.4f;
        // audioSource.pitch = 0.85f;
        audioSource.Play();
        Destroy(tempAudio, leverDink.length); 
    }

    private void ChangeState(SceneState newState) {
        if (newState == currentState) return;
        stateExitMethods[currentState]?.Invoke();
        currentState = newState;
        stateEnterMethods[currentState]?.Invoke();
    }

    // === State: WAITING ===

    void EnterWaiting() { }
    void UpdateWaiting() {
        checkCondition();
        freezeLever();

        if (canFlipLever) {
            ChangeState(SceneState.READY_TO_OPEN);
        }
    }
    void ExitWaiting() { }

    // === State: READY_TO_OPEN ===

    void EnterReadyToOpen() { }
    void UpdateReadyToOpen() {
        checkCondition();
        freezeLever();

        if (!canFlipLever) {
            ChangeState(SceneState.WAITING);
            return;
        }

        if (doorLeverKnobScript.value <= 0.15f) {
            doorOpeningSound();
            ChangeState(SceneState.OPENING_DOOR);
        }
    }
    void ExitReadyToOpen() { }

    // === State: OPENING_DOOR ===

    void EnterOpeningDoor() { }
    void UpdateOpeningDoor() {
        if (door.transform.localPosition.y > -1.95f) {
            door.transform.Translate(door.transform.up * -1f * Time.deltaTime * doorSpeed);
        } else if (playerInsideTemple.playerInside) {
            ChangeState(SceneState.TELEPORTING);
        }
    }
    void ExitOpeningDoor() { }

    // === State: TELEPORTING ===

    void EnterTeleporting() { }
    void UpdateTeleporting() {
        if (fadeOutScreen.color.a < 1f) {
            Color temp = fadeOutScreen.color;
            temp.a += Time.deltaTime * 1 / 0.3f;
            fadeOutScreen.color = temp;
        }

        if (fadeOutScreen.color.a >= 1f && !teleported) {
            teleported = true;
            SceneManager.LoadScene("Logan (light puzzle)");
        }
    }
    void ExitTeleporting() { }

    // === Utility Functions ===

    void checkCondition() {
        coinOn = Vector3.Distance(coin.transform.position, correctHook.transform.position) < 0.16f && coinRb.isKinematic;

        needleInCorrectPosition = needleKnobScript.value > 0f
            ? (needleKnobScript.value % 1f) > 0.75f && (needleKnobScript.value % 1f) <= 0.875f
            : (-needleKnobScript.value % 1f) < 0.25f && (-needleKnobScript.value % 1f) >= 0.125f;

        cubeInCorrectPos = Vector3.Distance(handHeldCube.transform.position, correctSpotForCube.position) < 0.1f;

        canFlipLever = coinOn && needleInCorrectPosition && cubeInCorrectPos;
    }

    void freezeLever() {
        if (!canFlipLever)
            doorLeverKnobScript.value = Mathf.Clamp(doorLeverKnobScript.value, 0.8f, 1f);
    }
}
