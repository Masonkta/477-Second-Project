using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bruteForce : MonoBehaviour
{
    public GameObject blueLens;
    
    public GameObject redLens;
    public GameObject yellow;
    public LightRoom mainScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool music = false;
        bool chess = false;
        foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>()){
            if (g.GetComponent<LightRoom>()){
                if (g.GetComponent<LightRoom>().musicPuz)
                    music = true;
                if (g.GetComponent<LightRoom>().chessPuz)
                    chess = true;
            }
        }
        blueLens.SetActive(!redLens.activeInHierarchy);
        redLens.SetActive(music && !yellow.activeInHierarchy);
        yellow.SetActive(chess);
    }
}
