using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using State = ChessGameState;
public class KingB : MonoBehaviour
{
    public State State { get; private set; }
    public GameObject chessGameLogic;
    private KingBState lastSnapLocal;
    // Start is called before the first frame update
    void Start()
    {
        lastSnapLocal = KingBState.NONE;
        State = chessGameLogic.GetComponent<ChessGameLogic>().currState;
        print("STARTING STATE: " + State);
        print("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSnapLocal == KingBState.NONE)
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
        lastSnapLocal = KingBState.NONE;
    }
    private void ChangeState(State newState)
    {
        print($"Changing state to {newState}");
        if (State != newState)
        {
            State = newState;
            switch (newState)
            {
                case State.IDLE:
                    // do nothing
                    break;
                case State._1_BP:
                    //ENABLE NEXT SNAP SET
                    GameObject snaps1 = GameObject.Find("ChessTable/Chess Board and Pieces/BKSnaps1");
                    snaps1.SetActive(false);
                    GameObject snap2 = GameObject.Find("ChessTable/Chess Board and Pieces/BKSnaps2");
                    snap2.SetActive(true);
                    break;
                case State._2_Pos2Snap1_FINISH:
                    //GameObject reward = GameObject.Find("Reward");
                    //reward.SetActive(true);
                    print("FINISH ACHIEVED!");
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
        lastSnapLocal = KingBState.POS1SNAP1;
        print(lastSnapLocal);
    }
    private void Snap1INCORRECT()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = KingBState.POS1INCORRECT;
        print(lastSnapLocal);
    }
    private void Snap2Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = KingBState.POS2SNAP1;
        //lastSnap = SnapStates.POS1SNAP1;
        print("lastSnap Change");
        print(lastSnapLocal);
    }
}
