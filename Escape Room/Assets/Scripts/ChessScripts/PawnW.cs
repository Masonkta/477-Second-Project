using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = ChessGameState;
public class PawnW : MonoBehaviour
{
    public State State { get; private set; }
    public GameObject chessGameLogic;
    public PawnWState lastSnapLocal;
    // Start is called before the first frame update
    void Start()
    {
        lastSnapLocal = PawnWState.NONE;
        State = chessGameLogic.GetComponent<ChessGameLogic>().currState;
        print("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSnapLocal == PawnWState.NONE)
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
                ChangeState(State.INCORRECT);
                break;
            case State._3_BR:
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
        lastSnapLocal = PawnWState.NONE;
    }
    private void ChangeState(State newState)
    {
        print($"Changing state to {newState}");
        if (State != newState)
        {
            State = newState;
            chessGameLogic.GetComponent<ChessGameLogic>().currState = newState;
            print("GLOBAL STATE CHECK: " + chessGameLogic.GetComponent<ChessGameLogic>().currState);
            switch (newState)
            {
                case State.IDLE:
                    // do nothing
                    break;
                case State._1_BP:
                    ChessSoundManager.Instance.Play(ChessSoundType.CLICK);
                    //ENABLE NEXT SNAP SET
                    GameObject snaps1 = GameObject.Find("ChessTable/Chess Board and Pieces/BPSnaps1");
                    snaps1.SetActive(false);
                    GameObject snap2 = GameObject.Find("ChessTable/Chess Board and Pieces/BPSnaps2");
                    snap2.SetActive(true);
                    break;
                case State._5_BP:
                    ChessSoundManager.Instance.Play(ChessSoundType.CLICK);
                    break;
                case State.ERROR:
                    print("ERROR");
                    break;
                case State.INCORRECT:
                    ChessSoundManager.Instance.Play(ChessSoundType.CLICK);
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
        lastSnapLocal = PawnWState.POS1SNAP1;
        print(lastSnapLocal);
    }
    private void Snap1INCORRECT()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = PawnWState.POS1INCORRECT;
        print(lastSnapLocal);
    }
    private void Snap2Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = PawnWState.POS2SNAP1;
        //lastSnap = SnapStates.POS1SNAP1;
        print("lastSnap Change");
        print(lastSnapLocal);
    }
}
