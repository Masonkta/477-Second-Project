using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = PawnBState;
public class PawnB : MonoBehaviour
{
    public State State { get; private set; }
    public GameObject chessGameLogic;
    public SnapStates lastSnapLocal;
    // Start is called before the first frame update
    void Start()
    {
        lastSnapLocal = chessGameLogic.GetComponent<ChessGameLogic>().lastSnap;
        State = State.IDLE;
        print("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSnapLocal == SnapStates.NONE)
        {
            return;
        }
        switch (State)
        {
            case State.IDLE:
                if (lastSnapLocal == SnapStates.POS1SNAP1)
                    ChangeState(State._1_Pos1Snap1);
                else
                    ChangeState(State.ERROR);
                break;
            case State._1_Pos1Snap1:
                if (lastSnapLocal == SnapStates.POS2SNAP1)
                    ChangeState(State._2_Pos2Snap1_FINISH);
                else
                    ChangeState(State.ERROR);
                break;
            case State.INCORRECT:
                if (lastSnapLocal == SnapStates.POS1INCORRECT)
                    ChangeState(State.INCORRECT);
                else
                    ChangeState(State.ERROR);
                break;
        }
        lastSnapLocal = SnapStates.NONE;
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
                case State._1_Pos1Snap1:
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
                    print("INCORRECT MOVE!");
                    break;
            }
        }
    }
    private void Snap1Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = SnapStates.POS1SNAP1;
        print(lastSnapLocal);
    }
    private void Snap1INCORRECT()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = SnapStates.POS1INCORRECT;
        print(lastSnapLocal);
    }
    private void Snap2Pos1()
    {
        print("SNAPPED");
        print(lastSnapLocal);
        lastSnapLocal = SnapStates.POS2SNAP1;
        //lastSnap = SnapStates.POS1SNAP1;
        print("lastSnap Change");
        print(lastSnapLocal);
    }
}

