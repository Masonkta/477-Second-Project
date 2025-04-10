using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TESTlighttimer : MonoBehaviour {
    public ParticleSystem lighting;

    private float timeElaps;
    private float lightingR = 11;
    private float lightingAng = 40;
    // Start is called before the first frame update
    void Start() {
        var shape = lighting.shape;
        shape.radius = lightingR;
        shape.angle = lightingAng;
        
    }

    // Update is called once per frame
    void Update() {
        timeElaps = GameObject.Find("Game Timer").GetComponent<gameTimer>().percentOfDayLeft;

        var shape = lighting.shape;

        if (shape.angle > 0) { 
            lightingAng = timeElaps * 40f;
            shape.angle = lightingAng;
        } else {
            if (lightingR <= 0.005) {
                lighting.gameObject.SetActive(false);
                print("off");
            }
            else {
                lightingR = timeElaps * 11f;
            }
            shape.radius = lightingR;
        }
    }

    public void setTimeEnd() {
        var shape = lighting.shape;
        lightingR = .5f;
        lightingAng = 0;
        shape.radius = lightingR;
        shape.angle = lightingAng;
    }
}