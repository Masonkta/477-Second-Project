using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = LightRoomState;

public enum LightRoomState {
    INIT,
    FIRST,
    SECOND,
}

public class LightRoom : MonoBehaviour {

    public State CurState { get; private set; }
    public GameObject blueLens;
    public GameObject blueDoor;
    public GameObject redLens;
    public GameObject redDoor;
    public GameObject yellowLens;
    public GameObject yellowDoor;
    public GameObject lensSpawn;
    public GameObject outside;
    public bool chessFinished;
    public bool numFinished;

    private Dictionary<State, Action> stateEnterMethods;
    private Dictionary<State, Action> stateStayMethods;
    private Dictionary<State, Action> stateExitMethods;

    void Start() {
        stateEnterMethods = new() {
            [State.INIT] = StateEnterINIT,
            [State.FIRST] = StateEnterFIRST,
            [State.SECOND] = StateEnterSECOND,
        };
        stateStayMethods = new() {
            [State.INIT] = StateStayINIT,
            [State.FIRST] = StateStayFIRST,
            [State.SECOND] = StateStaySECOND,
        };
        stateExitMethods = new() {
            [State.INIT] = StateExitINIT,
            [State.FIRST] = StateExitFIRST,
            [State.SECOND] = StateExitSECOND,
        };
        CurState = State.INIT;

        blueLens.transform.position = lensSpawn.transform.position;
        redLens.transform.position = outside.transform.position;
        yellowDoor.transform.position = outside.transform.position;
    }

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
    private void StateEnterINIT() {
    }
    private void StateEnterFIRST() {
        blueDoor.transform.position = new Vector3(4, -7.66f, -6);
        blueLens.transform.position = outside.transform.position;
        redLens.transform.position = lensSpawn.transform.position;
    }
    private void StateEnterSECOND() {
        blueDoor.transform.position = new Vector3(4, -7.66f, -6);
        redDoor.transform.position = new Vector3(-6, -7.66f, 4);
        blueLens.transform.position = outside.transform.position;
        redLens.transform.position = outside.transform.position;
        yellowLens.transform.position = lensSpawn.transform.position;
    }
    #endregion

    #region stay states
    private void StateStayINIT() {
    }
    private void StateStayFIRST() {
    }
    private void StateStaySECOND() {
    }
    #endregion

    #region exit states
    private void StateExitINIT() {
    }
    private void StateExitFIRST() {
    }
    private void StateExitSECOND() {
    }
    #endregion
}
