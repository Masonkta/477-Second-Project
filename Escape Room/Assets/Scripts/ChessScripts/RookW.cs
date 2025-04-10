using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = ChessGameState;
public class RookW : MonoBehaviour
{
    public State State { get; private set; }
    public GameObject chessGameLogic;
    public RookWState lastSnapLocal;
    // Start is called before the first frame update
    void Start()
    {
        lastSnapLocal = RookWState.NONE;
        State = chessGameLogic.GetComponent<ChessGameLogic>().currState;
        print("STARTING STATE: " + State);
        print("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSnapLocal == RookWState.NONE)
        {
            return;
        }
        switch (State = chessGameLogic.GetComponent<ChessGameLogic>().currState)
        {
            case State.IDLE:
                ChangeState(State.INCORRECT);
                break;
            case State._1_BP:
                ChangeState(State.INCORRECT);
                break;
            case State._2_WK:
                //NOTE: Might not need if statments for completly incorrect states? Only fires off once the piece is snapped, not constantly
                ChangeState(State.INCORRECT);
                break;
            case State._3_BR:
                if (lastSnapLocal == RookWState.POS1SNAP1)
                    ChangeState(State._4_WR);
                else
                    ChangeState(State.INCORRECT);
                break;
            case State._4_WR:
                ChangeState(State.INCORRECT);
                break;
            case State._5_BP:
                ChangeState(State.INCORRECT);
                break;
            case State.INCORRECT:
                ChangeState(State.IDLE);
                break;
        }
        lastSnapLocal = RookWState.NONE;
    }
    private void ChangeState(State newState)
    {
        print($"Changing state to {newState}");
        if (State != newState)
        {
            chessGameLogic.GetComponent<ChessGameLogic>().currState = newState;
            print("GLOBAL STATE CHECK: " + chessGameLogic.GetComponent<ChessGameLogic>().currState);
            State = chessGameLogic.GetComponent<ChessGameLogic>().currState;
            switch (newState)
            {
                case State.IDLE:
                    // do nothing
                    break;
                case State._4_WR:
                    //ENABLE NEXT SNAP SET
                    GameObject snaps1 = GameObject.Find("ChessTable/Chess Board and Pieces/WRSnaps1");
                    snaps1.SetActive(false);
                    GameObject snap2 = GameObject.Find("ChessTable/Chess Board and Pieces/WRSnaps2");
                    snap2.SetActive(true);
                    break;
                case State.ERROR:
                    print("ERROR");
                    break;
                case State.INCORRECT:
                    chessGameLogic.GetComponent<ChessGameLogic>().PuzzleReset();

                    print("WRONG MOVE!!!!");
                    break;
            }
        }
    }
    private void Snap1Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = RookWState.POS1SNAP1;
        print(lastSnapLocal);
    }
    private void Snap1INCORRECT()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = RookWState.POS1INCORRECT;
        print(lastSnapLocal);
    }
    private void Snap2Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = RookWState.POS2SNAP1;
        //lastSnap = SnapStates.POS1SNAP1;
        print("lastSnap Change");
        print(lastSnapLocal);
    }
}
