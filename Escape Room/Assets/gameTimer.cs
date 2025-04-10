using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class gameTimer : MonoBehaviour
{   
    private InputDevice leftController;
    private InputDevice rightController;

    public bool gameOver;
    public GameObject player;
    public Transform playerCamera;

    public bool leftStickPressed;
    public bool rightStickPressed;
    public TextMeshProUGUI timerCanvas;
    
    public float elapsedTime = 0f;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = player.transform.Find("Camera Offset").transform.Find("Main Camera");   
    }

    void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
        InitializeControllers();
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
        InputDevices.deviceDisconnected -= OnDeviceDisconnected;
    }

    void InitializeControllers()
    {
        var leftHandedDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandedDevices);
        if (leftHandedDevices.Count > 0)
            leftController = leftHandedDevices[0];

        var rightHandedDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandedDevices);
        if (rightHandedDevices.Count > 0)
            rightController = rightHandedDevices[0];
    }

    void OnDeviceConnected(InputDevice device)
    {
        if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
            leftController = device;
        else if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
            rightController = device;
    }

    void OnDeviceDisconnected(InputDevice device)
    {
        if (device == leftController)
            leftController = default;
        if (device == rightController)
            rightController = default;
    }

    void Update()
    {
        getInput();
        timerStuff();
        timerCanvas.gameObject.SetActive(leftStickPressed || rightStickPressed);

        Time.timeScale = rightStickPressed ? 60f : 1f;
        elapsedTime += Time.deltaTime;
        gameOver = elapsedTime > 20f * 60f;
    }

    void getInput(){
        leftStickPressed = false;
        rightStickPressed = false;
        
        if (leftController.isValid && leftController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool leftClick) && leftClick)
            leftStickPressed = true;

        if (rightController.isValid && rightController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool rightClick) && rightClick)
            rightStickPressed = true;
        

    }

    void timerStuff(){
        if (timerCanvas.gameObject.activeInHierarchy){
            int totalSecondsLeft = Mathf.Max(0, 60 * 20 - Mathf.RoundToInt(elapsedTime));

            int minutes = totalSecondsLeft / 60;
            int seconds = totalSecondsLeft % 60;

            string displayTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerCanvas.text = displayTime;
        }

        timerCanvas.transform.position = playerCamera.position + playerCamera.forward * 2f;
        timerCanvas.transform.rotation = Quaternion.LookRotation(timerCanvas.transform.position - playerCamera.transform.position);
    }
}
