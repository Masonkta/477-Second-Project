using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorScript : MonoBehaviour
{
    public float totalXRotation = 0f;
    public float currAngle;
    private float lastFrameXRotation = 0f;
    public Transform camTransform;
    public Transform plane;
    public float planeDOT;

    public float initialScaleX;
    public float initialScaleY;
    public float initialScaleZ;

    void Start()
    {
        // // Initialize the lastFrameXRotation with the current local X rotation
        // lastFrameXRotation = transform.localEulerAngles.x;
        initialScaleX = plane.localScale.x;
        initialScaleY = plane.localScale.y;
        initialScaleZ = plane.localScale.z;
    }

    void Update()
    {

        float currentXRotation = transform.localEulerAngles.x;

        // Calculate delta rotation considering wraparound from 360 to 0
        float deltaRotation = Mathf.DeltaAngle(lastFrameXRotation, currentXRotation);
        totalXRotation += deltaRotation;

        lastFrameXRotation = currentXRotation;

        float rawX = transform.localEulerAngles.x;
        currAngle = 1;

        planeDOT = Vector3.Dot(transform.right, Vector3.up);
        

        if (planeDOT <= 0f){
            // PROBLEM
            plane.localEulerAngles = new Vector3(0f, -90f - totalXRotation, 0f);
            plane.localScale = new Vector3(-initialScaleX, initialScaleY, -initialScaleZ);
        }
        else {
            plane.localEulerAngles = new Vector3(0f, -90f + totalXRotation, 0f);
            plane.localScale = new Vector3(initialScaleX, initialScaleY, initialScaleZ);
        }

        camTransform.position = plane.position;

        // Set the camera's rotation to look in the forward direction with the world's up direction.
        camTransform.rotation = Quaternion.LookRotation(plane.up, Vector3.up);

    }
    
}