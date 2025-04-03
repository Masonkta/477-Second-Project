using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using State = KingBState;
public class KingB : MonoBehaviour
{
    public State State { get; private set; }
    private SnapStates lastSnap;
    // Start is called before the first frame update
    void Start()
    {
        lastSnap = SnapStates.NONE;
        State = State.IDLE;
        print("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSnap == SnapStates.NONE)
        {
            return;
        }
        switch (State)
        {
            case State.IDLE:
                if (lastSnap == SnapStates.POS1SNAP1)
                    ChangeState(State._1_Pos1Snap1);
                else
                    ChangeState(State.ERROR);
                break;
        }
        lastSnap = SnapStates.NONE;
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
                    break;
            }
        }
    }
    private void Snapped()
    {
        print("SNAPPED");
    }
}
