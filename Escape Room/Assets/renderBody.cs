using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderBody : MonoBehaviour
{

    public Transform leftHand;
    public Transform rightHand;
    public Transform head;
    public Vector3 chest;
    public LineRenderer LR;

    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        locateChest();
        LR.SetPosition(0, head.position + head.up * -0.16f);
        LR.SetPosition(1, chest);
    }

    void locateChest(){
        chest = head.position + head.up * -0.3f;
    }
}
