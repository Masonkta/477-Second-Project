using System.Collections;
using System.Collections.Generic;
using System.Threading;
// using UnityEditor.Presets;
using UnityEngine;

public class ChessGameLogic : MonoBehaviour
{
    public GameObject BK;
    public GameObject BKSnaps0;
    public GameObject BKSnaps1;
    public GameObject BKSnaps2;
    public Vector3 BKOrigin;
    public GameObject WK;
    public GameObject WKSnaps0;
    public GameObject WKSnaps1;
    public GameObject WKSnaps2;
    public Vector3 WKOrigin;
    public GameObject BP;
    public GameObject BPSnaps0;
    public GameObject BPSnaps1;
    public GameObject BPSnaps2;
    public Vector3 BPOrigin;
    public GameObject WP;
    public GameObject WPSnaps0;
    public GameObject WPSnaps1;
    public GameObject WPSnaps2;
    public Vector3 WPOrigin;
    public GameObject BR;
    public GameObject BRSnaps0;
    public GameObject BRSnaps1;
    public GameObject BRSnaps2;
    public Vector3 BROrigin;
    public GameObject WR;
    public GameObject WRSnaps0;
    public GameObject WRSnaps1;
    public GameObject WRSnaps2;
    public Vector3 WROrigin;
    public GameObject Reward;
    public float resetSpeed;
    public bool mistakeMade;
    public int correctCounter=0;
    public ChessGameState currState;

    private bool reset;
    private bool bkReset;
    private bool bpReset;
    private bool brReset;
    private bool wkReset;
    private bool wpReset;
    private bool wrReset;

    public float maxPieceRange;
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
        if (Vector3.Distance(BK.transform.position, BKOrigin) > maxPieceRange)
        {
            print("PIECE TOO FAR!!!!");
            BK.transform.position = BKOrigin;
        }
        if (Vector3.Distance(BR.transform.position, BROrigin) > maxPieceRange)
        {
            print("PIECE TOO FAR!!!!");
            BR.transform.position = BROrigin;
        }
        if (Vector3.Distance(BP.transform.position, BPOrigin) > maxPieceRange)
        {
            print("PIECE TOO FAR!!!!");
            BP.transform.position = BPOrigin;
        }
        if (Vector3.Distance(WK.transform.position, WKOrigin) > maxPieceRange)
        {
            print("PIECE TOO FAR!!!!");
            WK.transform.position = WKOrigin;
        }
        if (Vector3.Distance(WR.transform.position, WROrigin) > maxPieceRange)
        {
            print("PIECE TOO FAR!!!!");
            WR.transform.position = WROrigin;
        }
        if (Vector3.Distance(WP.transform.position, WPOrigin) > maxPieceRange)
        {
            print("PIECE TOO FAR!!!!");
            WP.transform.position = WPOrigin;
        }
        if (reset)
        {

            print("I AM RESETTING!");
            //Turn off all snap points for lerp logic and turn on reset points
            if (bkReset == false)
            {
                BKSnaps0.SetActive(true);
                BKSnaps1.SetActive(false);
                BKSnaps2.SetActive(false);
                //Move black king towards origin
                BK.transform.position = Vector3.Lerp(BK.transform.position, BKOrigin, resetSpeed * Time.deltaTime);
                //Detect if black king is close enough to origin to simply place back.
                if (Vector3.Distance(BK.transform.position, BKOrigin) < 0.01f)
                {
                    BK.transform.position = BKOrigin;
                    bkReset = true;
                    //Turn back on appropriate snap points
                    BKSnaps0.SetActive(false);
                    BKSnaps1.SetActive(true);
                    BKSnaps2.SetActive(false);
                }

            }
            if (bpReset == false)
            {
                BPSnaps0.SetActive(true);
                BPSnaps1.SetActive(false);
                BPSnaps2.SetActive(false);
                //Move black pawn towards origin
                BP.transform.position = Vector3.Lerp(BP.transform.position, BPOrigin, resetSpeed * Time.deltaTime);
                //Detect if black pawn is close enough to origin to simply place back.
                if (Vector3.Distance(BP.transform.position, BPOrigin) < 0.01f)
                {
                    BP.transform.position = BPOrigin;
                    bpReset = true;
                    BPSnaps1.SetActive(true);
                    BPSnaps2.SetActive(false);
                    BPSnaps0.SetActive(false);
                }
            }
            if (brReset == false)
            {
                BRSnaps0.SetActive(true);
                BRSnaps1.SetActive(false);
                BRSnaps2.SetActive(false);
                //Move black rook towards origin
                BR.transform.position = Vector3.Lerp(BR.transform.position, BROrigin, resetSpeed * Time.deltaTime);
                //Detect if black rook is close enough to origin to simply place back.
                if (Vector3.Distance(BR.transform.position, BROrigin) < 0.01f)
                {
                    BR.transform.position = BROrigin;
                    brReset = true;
                    BRSnaps1.SetActive(true);
                    BRSnaps2.SetActive(false);
                    BRSnaps0.SetActive(false);
                }
            }
            if (wkReset == false)
            {
                WKSnaps0.SetActive(true);
                WKSnaps1.SetActive(false);
                WKSnaps2.SetActive(false);
                //Move white king towards origin
                WK.transform.position = Vector3.Lerp(WK.transform.position, WKOrigin, resetSpeed * Time.deltaTime);
                //Detect if white king is close enough to origin to simply place back.
                if (Vector3.Distance(WK.transform.position, WKOrigin) < 0.01f)
                {
                    WK.transform.position = WKOrigin;
                    wkReset = true;
                    WKSnaps1.SetActive(true);
                    WKSnaps2.SetActive(false);
                    WKSnaps0.SetActive(false);
                }
            }
            if (wpReset == false)
            {
                WPSnaps0.SetActive(true);
                WPSnaps1.SetActive(false);
                WPSnaps2.SetActive(false);
                //Move white pawn towards origin
                WP.transform.position = Vector3.Lerp(WP.transform.position, WPOrigin, resetSpeed * Time.deltaTime);
                //Detect if white pawn is close enough to origin to simply place back.
                if (Vector3.Distance(WP.transform.position, WPOrigin) < 0.01f)
                {
                    WP.transform.position = WPOrigin;
                    wpReset = true;
                    WPSnaps1.SetActive(true);
                    WPSnaps2.SetActive(false);
                    WPSnaps0.SetActive(false);
                }
            }
            if (wrReset == false)
            {
                WRSnaps0.SetActive(true);
                WRSnaps1.SetActive(false);
                WRSnaps2.SetActive(false);
                //Move white rook towards origin
                WR.transform.position = Vector3.Lerp(WR.transform.position, WROrigin, resetSpeed * Time.deltaTime);
                //Detect if white rook is close enough to origin to simply place back.
                if (Vector3.Distance(WR.transform.position, WROrigin) < 0.01f)
                {
                    WR.transform.position = WROrigin;
                    wrReset = true;
                    WRSnaps1.SetActive(true);
                    WRSnaps2.SetActive(false);
                    WRSnaps0.SetActive(false);
                }
            }
            if (wkReset == true && wpReset == true && wrReset == true && bkReset == true && bpReset == true && brReset == true)
            {
                print("RESET ENDED");
                reset = false;
            }
        }

    }
    public void PuzzleReset()
    {
        print("Resetting Chess Game...");
        //Lerp Resete Logic
        reset = true;
        bkReset = false;
        bpReset = false;
        brReset = false;
        wkReset = false;
        wpReset = false;
        wrReset = false;
        //Basic reset logic
        currState = ChessGameState.IDLE; // This works

    }
}
