using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = ChessGameState;
public class RookB : MonoBehaviour
{
    public State State { get; private set; }
    public GameObject chessGameLogic;
    public RookBState lastSnapLocal;
    // Start is called before the first frame update
    void Start()
    {
        lastSnapLocal = RookBState.NONE;
        State = chessGameLogic.GetComponent<ChessGameLogic>().currState;
        print("STARTING STATE: " + State);
        print("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSnapLocal == RookBState.NONE)
        {
            return;
        }
        switch (State = chessGameLogic.GetComponent<ChessGameLogic>().currState)
        {
            case State.IDLE:
                if (lastSnapLocal == RookBState.POS1SNAP1)// Add a bunch of OR statments
                {
                    print("STATE IDLE: " + State);
                    ChangeState(State.INCORRECT);
                }
                else
                    ChangeState(State.ERROR);
                break;
            case State._1_BP:
                if (lastSnapLocal == RookBState.POS1SNAP1)
                    ChangeState(State.INCORRECT);
                break;
            case State._2_WK:
                if (lastSnapLocal == RookBState.POS1SNAP1)
                    ChangeState(State._3_BR);
                else
                    ChangeState(State.ERROR);
                break;
            case State._3_BR:
                if (lastSnapLocal == RookBState.POS2SNAP1)
                    ChangeState(State.INCORRECT);
                break;
            case State._4_WR:
                if (lastSnapLocal == RookBState.POS2SNAP1)
                    ChangeState(State.INCORRECT);
                break;
            case State._5_BP:
                if (lastSnapLocal == RookBState.POS2SNAP1)
                    ChangeState(State.INCORRECT);
                break;
            case State.INCORRECT:
                ChangeState(State.IDLE);
                break;
        }
        lastSnapLocal = RookBState.NONE;
    }
    private void ChangeState(State newState)
    {
        print($"Changing state to {newState}");
        
        if (State != newState)
        {
            chessGameLogic.GetComponent<ChessGameLogic>().currState = newState;
            State = chessGameLogic.GetComponent<ChessGameLogic>().currState;
            print("GLOBAL STATE CHECK: " + chessGameLogic.GetComponent<ChessGameLogic>().currState);
            switch (newState)
            {
                case State.IDLE:
                    // do nothing
                    break;
                case State._3_BR:
                    //ENABLE NEXT SNAP SET
                    GameObject snaps1 = GameObject.Find("ChessTable/Chess Board and Pieces/BRSnaps1");
                    snaps1.SetActive(false);
                    GameObject snap2 = GameObject.Find("ChessTable/Chess Board and Pieces/BRSnaps2");
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
        lastSnapLocal = RookBState.POS1SNAP1;
        print(lastSnapLocal);
    }
    private void Snap1INCORRECT()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = RookBState.POS1INCORRECT;
        print(lastSnapLocal);
    }
    private void Snap2Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = RookBState.POS2SNAP1;
        //lastSnap = SnapStates.POS1SNAP1;
        print("lastSnap Change");
        print(lastSnapLocal);
    }
}
