using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTimerBox : MonoBehaviour
{
    public ParticleSystem lighting;

    private float maxTime = 1200f; // 20 minutes in seconds
    private Vector3 lightingSize = new Vector3(31.7f, 39.4f, 1f); // The starting size of the box

    // Start is called before the first frame update
    void Start()
    {
        var shape = lighting.shape;
        shape.shapeType = ParticleSystemShapeType.Box; // Set the shape to Box
        shape.scale = lightingSize; // Set the box size
    }

    // Update is called once per frame
    void Update()
    {
        var shape = lighting.shape;

        // Ensure the box has positive size for x and y, but z remains constant at 1
        if (lightingSize.x > 0 && lightingSize.y > 0)
        {
            // Reduce the size over time, for 20 minutes (1200 seconds)
            float reductionRate = Time.deltaTime / maxTime; // Amount to reduce the size each frame

            lightingSize.x -= reductionRate; // Only reduce x
            lightingSize.y -= reductionRate; // Only reduce y
            // z remains constant at 1
            shape.scale = lightingSize;
        }
        else
        {
            // Ensure that the x and y values don't go below a minimum threshold
            if (lightingSize.x <= 0.005f && lightingSize.y <= 0.005f)
            {
                lighting.gameObject.SetActive(false);
                print("off");
            }
        }
    }

    public void setTimeEnd()
    {
        var shape = lighting.shape;
        lightingSize = new Vector3(0.5f, 0.5f, 1f); // Set a new small box size but keep z as 1
        shape.scale = lightingSize; // Apply the new box size
    }
}
