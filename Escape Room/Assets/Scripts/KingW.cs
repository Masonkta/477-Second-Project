using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = ChessGameState;
public class KingW : MonoBehaviour
{
    public State State { get; private set; }
    public GameObject chessGameLogic;
    public KingWState lastSnapLocal;
    // Start is called before the first frame update
    void Start()
    {
        lastSnapLocal = KingWState.NONE;
        State = chessGameLogic.GetComponent<ChessGameLogic>().currState;
        print("STARTING STATE: " + State);
        print("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSnapLocal == KingWState.NONE)
        {
            return;
        }
        switch (State = chessGameLogic.GetComponent<ChessGameLogic>().currState)
        {
            case State.IDLE:
                if (lastSnapLocal == KingWState.POS1SNAP1)// Add a bunch of OR statments
                {
                    print("STATE IDLE: " + State);
                    ChangeState(State.INCORRECT);
                }
                else
                    ChangeState(State.ERROR);
                break;
            case State._1_BP:
                if (lastSnapLocal == KingWState.POS1SNAP1)
                    ChangeState(State._2_WK);
                break;
            case State._2_WK:
                if (lastSnapLocal == KingWState.POS1SNAP1)
                    ChangeState(State.INCORRECT);
                else
                    ChangeState(State.ERROR);
                break;
            case State._3_BR:
                if (lastSnapLocal == KingWState.POS2SNAP1)
                    ChangeState(State.INCORRECT);
                break;
            case State._4_WR:
                if (lastSnapLocal == KingWState.POS2SNAP1)
                    ChangeState(State.INCORRECT);
                break;
            case State._5_BP:
                if (lastSnapLocal == KingWState.POS2SNAP1)
                    ChangeState(State.INCORRECT);
                break;
            case State.INCORRECT:
                ChangeState(State.IDLE);
                break;
        }
        lastSnapLocal = KingWState.NONE;
    }
    private void ChangeState(State newState)
    {
        print($"Changing state to {newState}");
        print("GLOBAL STATE CHECK: " + chessGameLogic.GetComponent<ChessGameLogic>().currState);
        if (State != newState)
        {
            chessGameLogic.GetComponent<ChessGameLogic>().currState = newState;
            State = chessGameLogic.GetComponent<ChessGameLogic>().currState;
            switch (newState)
            {
                case State.IDLE:
                    // do nothing
                    break;
                case State._2_WK:
                    //ENABLE NEXT SNAP SET
                    GameObject snaps1 = GameObject.Find("ChessTable/Chess Board and Pieces/WKSnaps1");
                    snaps1.SetActive(false);
                    GameObject snap2 = GameObject.Find("ChessTable/Chess Board and Pieces/WKSnaps2");
                    snap2.SetActive(true);
                    break;
                case State._5_BP:
                    //GameObject reward = GameObject.Find("Reward");
                    //reward.SetActive(true);
                    print("FINISH ACHIEVED!");
                    break;
                case State.ERROR:
                    print("ERROR");
                    break;
                case State.INCORRECT:
                    print("WRONG MOVE!!!!");
                    break;
            }
        }
    }
    private void Snap1Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = KingWState.POS1SNAP1;
        print(lastSnapLocal);
    }
    private void Snap1INCORRECT()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = KingWState.POS1INCORRECT;
        print(lastSnapLocal);
    }
    private void Snap2Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = KingWState.POS2SNAP1;
        //lastSnap = SnapStates.POS1SNAP1;
        print("lastSnap Change");
        print(lastSnapLocal);
    }
}