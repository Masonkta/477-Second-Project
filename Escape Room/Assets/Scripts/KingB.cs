using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using State = ChessGameState;
public class KingB : MonoBehaviour
{
    public State State { get; private set; }
    private KingBState lastSnap;
    // Start is called before the first frame update
    void Start()
    {
        lastSnap = KingBState.NONE;
        State = State.IDLE;
        print("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSnap == KingBState.NONE)
        {
            return;
        }
        switch (State)
        {
            case State.IDLE:
                if (lastSnap == KingBState.POS1SNAP1)
                    ChangeState(State._1_Pos1Snap1);
                else
                    ChangeState(State.ERROR);
                break;
            case State._1_Pos1Snap1:
                if (lastSnap == KingBState.POS2SNAP1)
                    ChangeState(State._2_Pos2Snap1_FINISH);
                else
                    ChangeState(State.ERROR);
                break;
            case State.INCORRECT:
                if (lastSnap == KingBState.POS1INCORRECT)
                    ChangeState(State.INCORRECT);
                else
                    ChangeState(State.ERROR);
                break;
        }
        lastSnap = KingBState.NONE;
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
        print(lastSnap);
        lastSnap = KingBState.POS1SNAP1;
        print(lastSnap);
    }
    private void Snap1INCORRECT()
    {
        print("SNAPPED");
        print(lastSnap);
        lastSnap = KingBState.POS1INCORRECT;
        print(lastSnap);
    }
    private void Snap2Pos1()
    {
        print("SNAPPED");
        print(lastSnap);
        lastSnap = KingBState.POS2SNAP1;
        //lastSnap = SnapStates.POS1SNAP1;
        print("lastSnap Change");
        print(lastSnap);
    }
}
