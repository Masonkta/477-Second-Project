using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class gameTimer : MonoBehaviour
{   
    private InputDevice leftController;
    private InputDevice rightController;

    public float elapsedTime = 0f;
    public float percentOfDayLeft;
    public bool leftStickPressed;
    public bool rightStickPressed;
    public TextMeshProUGUI timerCanvas;
    public GameObject player;
    public Transform playerCamera;
    public InputActionManager playerInput;
    public float playerZ;
    public bool gameOver;
    public float timeOfDeath;
    public float initialStop;
    public bool startFallingNow;
    public Image blackPic;
    public AudioSource rumblingSound;
    public AudioSource explosionSound;
    bool hasStartedRumbling = false;
    
    public bool fadeOutCompleted = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        assignPlayer();
    }

    void assignPlayer(){
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = player.transform.Find("Camera Offset").transform.Find("Main Camera");   
        playerInput = player.GetComponent<InputActionManager>();
    }

    void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        InputDevices.deviceDisconnected += OnDeviceDisconnected;
        InitializeControllers();// Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
        InputDevices.deviceDisconnected -= OnDeviceDisconnected;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This runs once after a new scene is loaded
        // Debug.Log("New scene loaded: " + scene.name);

        // Perform any first-frame actions here
        PerformFirstFrameAction();
        fadeOutCompleted = false;
        blackPic.gameObject.SetActive(true);
        Color temp = blackPic.color;
        temp.a = 1f;
        blackPic.color = temp;
    }

    void PerformFirstFrameAction()
    {
        // Any logic you want to execute on the first frame
        
        assignPlayer();
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

        if (blackPic.gameObject.activeInHierarchy && !fadeOutCompleted){
            blackPic.transform.parent.position = playerCamera.position + playerCamera.forward * 0.9f;
            blackPic.transform.parent.rotation = Quaternion.LookRotation(blackPic.transform.parent.position - playerCamera.transform.position);

            if (blackPic.color.a > 0f) {
                Color temp = blackPic.color;
                temp.a -= Time.deltaTime * 1 / 2f;
                blackPic.color = temp;
            }
            if (blackPic.color.a <= 0f) {
                blackPic.gameObject.SetActive(false);
                fadeOutCompleted = true;
            }
        }

        Time.timeScale = rightStickPressed ? 60f : 1f;
        elapsedTime += Time.deltaTime;
        percentOfDayLeft = 1f - (elapsedTime / (20f * 60f));
        percentOfDayLeft = Mathf.Clamp(percentOfDayLeft, 0f, 1f);
        gameOver = elapsedTime > 20f * 60f;
        if (gameOver)
            Time.timeScale = 1f;

        if (gameOver)
            killPlayer();
    }

    void killPlayer(){

        if (!rumblingSound.isPlaying){
            if (!hasStartedRumbling){
                hasStartedRumbling = true;
                rumblingSound.Play();
            }
            else{
                Scene activeScene = SceneManager.GetActiveScene();
                SceneManager.MoveGameObjectToScene(gameObject, activeScene);
                SceneManager.LoadScene("GameOver");
            }
        }

        if (playerInput.enabled){
            initialStop = Time.time;
            playerInput.enabled = false;
        }

        if (Time.time - initialStop > 1.2f && !startFallingNow){
            startFallingNow = true;
            timeOfDeath = Time.time;
        }

        if (startFallingNow){
            player.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, playerZ));

            float timeFalling = (Time.time - timeOfDeath) / 2f; // divided by 2 to give normalized
            
            if (timeFalling < 1f)
            {
                float speedFactor = 3f * timeFalling * timeFalling / 2f; // derivative of x^3
                playerZ -= 90f * speedFactor * Time.deltaTime;
            }
            else
            {
                if (playerZ != -90f){
                    playerZ = -90f;
                print("Player hit ground");
                blackPic.gameObject.SetActive(true);
                explosionSound.Play();
                }

                blackPic.transform.parent.position = playerCamera.position + playerCamera.forward * 0.9f;
                blackPic.transform.parent.rotation = Quaternion.LookRotation(blackPic.transform.parent.position - playerCamera.transform.position);

                if (blackPic.color.a < 1f) {
                    Color temp = blackPic.color;
                    temp.a += Time.deltaTime * 1 / 1.3f;
                    blackPic.color = temp;
                }
            }
        }
        
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
