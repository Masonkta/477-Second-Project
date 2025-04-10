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

    #region publics
    public State CurState { get; private set; }
    public static LightRoom Lightroom { get; private set; }
    public GameObject blueLens;
    public GameObject blueDoor;
    public GameObject redLens;
    public GameObject redDoor;
    public GameObject yellowLens;
    public GameObject yellowDoor;
    public GameObject lensSpawn;
    public bool musicPuz;
    public bool chessPuz;
    #endregion

    #region privates
    private Dictionary<State, Action> stateEnterMethods;
    private Dictionary<State, Action> stateStayMethods;
    private Dictionary<State, Action> stateExitMethods;
    #endregion

    private void Awake() {

        GameObject a = GameObject.Find(gameObject.name);

        if (a == gameObject && !musicPuz)
            DontDestroyOnLoad(gameObject);
        else if (!a == gameObject && musicPuz)
            Destroy(a);
        
    }
    

    void Start() {
        GameObject a = GameObject.Find(gameObject.name);
        if (!a == gameObject && musicPuz)
            Destroy(a);

        Lightroom = this;
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
        redLens.transform.position = lensSpawn.transform.position;
        // redLens.SetActive(false);
        yellowLens.transform.position = lensSpawn.transform.position;
        // yellowLens.SetActive(false);
    }

    void Update() {
        if (stateStayMethods.ContainsKey(CurState)) {
            stateStayMethods[CurState]();
        }
        if (musicPuz) {
            ChangeState(State.FIRST);
        }
        if (chessPuz) { 
            ChangeState(State.SECOND);
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
        if (blueDoor) blueDoor.transform.position = new Vector3(4, -7.66f, -6);
        if (blueLens) blueLens.SetActive(false);
        if (redLens) redLens.SetActive(true);
    }
    private void StateEnterSECOND() {
        if (blueDoor) blueDoor.transform.position = new Vector3(4, -7.66f, -6);
        if (redDoor) redDoor.transform.position = new Vector3(-6, -7.66f, 4);
        if (redLens) redLens.SetActive(false);
        if (blueLens) blueLens.SetActive(false);
        if (yellowLens) yellowLens.SetActive(true);
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
