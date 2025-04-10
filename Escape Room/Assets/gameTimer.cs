using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class gameTimer : MonoBehaviour
{   
    private InputDevice leftController;
    private InputDevice rightController;

    
    public bool rightStickPressed;

    public float elapsedTime = 0f;



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
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

        Time.timeScale = rightStickPressed ? 60f : 1f;
        elapsedTime += Time.deltaTime;
    }

    void getInput(){
        rightStickPressed = false;
        
        if (rightController.isValid && rightController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool rightClick) && rightClick)
            rightStickPressed = true;

    }
}
