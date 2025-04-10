using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = DoorState;

public enum DoorState {
    CLOSED,
    OPENING,
    OPEN,
}

public enum KeyColor {
    RED,
    YELLOW, 
    BLUE,
}

public class Door : MonoBehaviour {
    #region publics
    public State CurState { get; private set; }
    public KeyColor key;
    #endregion

    #region privates
    private Dictionary<State, Action> stateEnterMethods;
    private Dictionary<State, Action> stateStayMethods;
    private Dictionary<State, Action> stateExitMethods;
    private Transform fullDoor;
    #endregion

    // Start is called before the first frame update
    void Start() {
        stateEnterMethods = new() {
            [State.CLOSED] = StateEnterClosed,
            [State.OPENING] = StateEnterOpening,
            [State.OPEN] = StateEnterOpen,
        };
        stateStayMethods = new() {
            [State.CLOSED] = StateStayClosed,
            [State.OPENING] = StateStayOpening,
            [State.OPEN] = StateStayOpen,
        };
        stateExitMethods = new() {
            [State.CLOSED] = StateExitClosed,
            [State.OPENING] = StateExitOpening,
            [State.OPEN] = StateExitOpen,
        };
        CurState = State.CLOSED;
        fullDoor = transform;
    }

    // Update is called once per frame
    void Update() {
        if (stateStayMethods.ContainsKey(CurState)) {
            stateStayMethods[CurState]();
        }
    }

    public void ChangeState(State NewState) {
        if (CurState != NewState) {
            if (stateExitMethods.ContainsKey(CurState)) {
                stateExitMethods[CurState]();
            }
            CurState = NewState;
            if (stateEnterMethods.ContainsKey(CurState)) {
                stateEnterMethods[CurState]();
            }
        }
    }

    #region enter states
    private void StateEnterClosed() {
    }
    private void StateEnterOpening() {
        print("opening");
    }
    private void StateEnterOpen() {
    }
    #endregion

    #region stay states
    private void StateStayClosed() {
    }
    private void StateStayOpening() {
        fullDoor.Translate(Vector3.down * Time.deltaTime);
        if (fullDoor.position.y < -5) {
            ChangeState(State.OPEN);
        }
    }
    private void StateStayOpen() {
        print("open");
    }
    #endregion

    #region exit states
    private void StateExitClosed() {
    }
    private void StateExitOpening() {
    }
    private void StateExitOpen() {
    }
    #endregion
}
