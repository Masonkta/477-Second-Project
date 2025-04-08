using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Presets;
using UnityEngine;

public class ChessGameLogic : MonoBehaviour
{
    public GameObject BK;
    public Vector3 BKOrigin;
    public GameObject WK;
    public Vector3 WKOrigin;
    public GameObject BP;
    public Vector3 BPOrigin;
    public GameObject WP;
    public Vector3 WPOrigin;
    public GameObject BR;
    public Vector3 BROrigin;
    public GameObject WR;
    public Vector3 WROrigin;
    public GameObject Reward;
    public float resetSpeed;
    public bool mistakeMade;
    public int correctCounter=0;
    public ChessGameState currState;

    private bool reset;
    private bool bkReset;
    // Start is called before the first frame update
    void Start()
    {
        print("Chess Logic Loading...");
        currState = ChessGameState.IDLE;
        //Logging initial positions of the pieces.
        BKOrigin = BK.transform.position;
        WKOrigin = WK.transform.position;
        BPOrigin = BP.transform.position;
        WPOrigin = WP.transform.position;
        BROrigin = BR.transform.position;
        WROrigin = WR.transform.position;

        Reward.SetActive(false);
        //BK_Pos = BK.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(reset)
        {
            print("I AM RESETTING!");
            if (bkReset == false)
            {
                //Move black king towards origin
                BK.transform.position = Vector3.Lerp(BK.transform.position, BKOrigin, resetSpeed * Time.deltaTime);
                //Detect if black king is close enough to origin to simply place back.
                if (Vector3.Distance(BK.transform.position, BKOrigin) < 0.01f)
                {
                    BK.transform.position = BKOrigin;
                    bkReset = true;
                }
            }
            if (bkReset)
            {
                reset = false;
            }
        }
    }
    public void PuzzleReset()
    {
        reset = true;
        bkReset = false;
    }
}
