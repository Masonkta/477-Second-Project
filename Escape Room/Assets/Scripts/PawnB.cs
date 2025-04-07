using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = ChessGameState;
public class PawnB : MonoBehaviour
{
    public State State { get; private set; }
    public GameObject chessGameLogic;
    public PawnBState lastSnapLocal;
    // Start is called before the first frame update
    void Start()
    {
        lastSnapLocal = PawnBState.NONE;
        State = chessGameLogic.GetComponent<ChessGameLogic>().currState;
        print("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSnapLocal == PawnBState.NONE)
        {
            return;
        }
        switch (State)
        {
            case State.IDLE:
                if (lastSnapLocal == PawnBState.POS1SNAP1)
                    ChangeState(State._1_Pos1Snap1);
                else
                    ChangeState(State.ERROR);
                break;
            case State._1_Pos1Snap1:
                if (lastSnapLocal == PawnBState.POS2SNAP1)
                    ChangeState(State._2_Pos2Snap1_FINISH);
                else
                    ChangeState(State.ERROR);
                break;
            case State.INCORRECT:
                if (lastSnapLocal == PawnBState.POS1INCORRECT)
                    ChangeState(State.INCORRECT);
                else
                    ChangeState(State.ERROR);
                break;
        }
        lastSnapLocal = PawnBState.NONE;
    }
    private void ChangeState(State newState)
    {
        print($"Changing state to {newState}");
        print("GLOBAL STATE CHECK: " +  chessGameLogic.GetComponent<ChessGameLogic>().currState);
        if (State != newState)
        {
            State = newState;
            chessGameLogic.GetComponent<ChessGameLogic>().currState = newState;
            switch (newState)
            {
                case State.IDLE:
                    // do nothing
                    break;
                case State._1_Pos1Snap1:
                    //ENABLE NEXT SNAP SET
                    GameObject snaps1 = GameObject.Find("ChessTable/Chess Board and Pieces/BPSnaps1");
                    snaps1.SetActive(false);
                    GameObject snap2 = GameObject.Find("ChessTable/Chess Board and Pieces/BPSnaps2");
                    snap2.SetActive(true);
                    break;
                case State._2_Pos2Snap1_FINISH:
                    //GameObject reward = GameObject.Find("Reward");
                    //reward.SetActive(true);
                    print("FINISH ACHIEVED!");
                    break;
                case State.INCORRECT:
                    print("INCORRECT MOVE!");
                    break;
            }
        }
    }
    private void Snap1Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = PawnBState.POS1SNAP1;
        print(lastSnapLocal);
    }
    private void Snap1INCORRECT()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = PawnBState.POS1INCORRECT;
        print(lastSnapLocal);
    }
    private void Snap2Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = PawnBState.POS2SNAP1;
        //lastSnap = SnapStates.POS1SNAP1;
        print("lastSnap Change");
        print(lastSnapLocal);
    }
}

