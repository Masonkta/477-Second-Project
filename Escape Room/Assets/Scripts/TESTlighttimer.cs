using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TESTlighttimer : MonoBehaviour {
    public ParticleSystem lighting;

    private float maxTime = 100f;
    private float lightingR = 5;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        var shape = lighting.shape;
        if (lightingR <= 0.005) {
            lighting.gameObject.SetActive(false);
            print("off");
        } else {
            lightingR -= ((maxTime - Time.deltaTime)/(maxTime*1000));
        }
        shape.radius = lightingR;
    }
}