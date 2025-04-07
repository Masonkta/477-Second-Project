using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGameLogic : MonoBehaviour
{
    public GameObject BK;
    public GameObject WK;
    public GameObject BP;
    public GameObject WP;
    public GameObject BR;
    public GameObject WR;
    //private Transform BK_Pos;
    public bool mistakeMade;
    public int correctCounter=0;
    public SnapStates lastSnap;
    // Start is called before the first frame update
    void Start()
    {
        print("Chess Logic Loading...");
        lastSnap = SnapStates.NONE;
        //BK_Pos = BK.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (mistakeMade == true)
        {
            //RESET POSITIONS

            correctCounter = 0;
        }
        if (correctCounter == 1)
        {
            //WIN THE GAME!
            print("YOU WIN!");
        }
    }
}
