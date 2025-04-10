using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;
using State = ChessGameState;
public class PawnB : MonoBehaviour
{
    public GameObject Door;
    public LightRoom lightscript;
    public State State { get; private set; }
    public GameObject chessGameLogic;
    public PawnBState lastSnapLocal;
    // Start is called before the first frame update

    void Start()
    {
        Door = GameObject.Find("DoorLogic");
        lightscript = Door.GetComponent<LightRoom>();
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
        switch (State = chessGameLogic.GetComponent<ChessGameLogic>().currState)
        {
            case State.IDLE:
                if (lastSnapLocal == PawnBState.POS1SNAP1)
                    ChangeState(State._1_BP);
                else
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
                if (lastSnapLocal == PawnBState.POS2SNAP1)
                {
                    ChangeState(State._5_BP);
                }
                else
                {
                    ChangeState(State.INCORRECT);
                }
                break;
            case State._5_BP:
                if (lastSnapLocal == PawnBState.POS2SNAP1)
                    ChangeState(State._5_BP);
                else
                    ChangeState(State.INCORRECT);
                break;
            case State.INCORRECT:
                ChangeState(State.IDLE);
                break;
        }
        lastSnapLocal = PawnBState.NONE;
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
                    ChessSoundManager.Instance.Play(ChessSoundType.SUCCESS);
                    print("FINISH ACHIEVED!");
                    //GameObject reward = GameObject.Find("ChessTable/LensReward");
                    chessGameLogic.GetComponent<ChessGameLogic>().Reward.SetActive(true);
                    print("REWARD UNLOCKED!!");
                    break;
                case State.ERROR:
                    print("ERROR");
                    break;
                case State.INCORRECT:
                    ChessSoundManager.Instance.Play(ChessSoundType.CLICK);
                    lightscript.chessPuz = true;
                    chessGameLogic.GetComponent<ChessGameLogic>().PuzzleReset();
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

