using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using State = LightbeamState;

public enum LightbeamState {
    ON,
    OFF, 
}

public class LightBeam : MonoBehaviour {
    #region publics
    public State CurState { get; private set; }
    public GameObject lightObj;
    public KeyColor color;
    #endregion

    #region privates
    private Dictionary<State, Action> stateEnterMethods;
    private Dictionary<State, Action> stateStayMethods;
    private Dictionary<State, Action> stateExitMethods;
    private bool inArea;
    #endregion

    // Start is called before the first frame update
    void Start() {
        stateEnterMethods = new() {
            [State.OFF] = StateEnterOff,
            [State.ON] = StateEnterOn,
        };
        stateStayMethods = new() {
            [State.OFF] = StateStayOff,
            [State.ON] = StateStayOn,
        };
        stateExitMethods = new() {
            [State.OFF] = StateExitOff,
            [State.ON] = StateExitOn,
        };
        CurState = State.OFF;
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

    public void turnON() {
        if (inArea) { 
            ChangeState(State.ON);
        }
    }

    public void turnOFF() {
        ChangeState(State.OFF);
    }
    
    #region enter states
    private void StateEnterOff() {
    }
    private void StateEnterOn() {
        lightObj.SetActive(true);
    }
    #endregion

    #region stay states
    private void StateStayOff() { 
    }
    private void StateStayOn() { 
    }
    #endregion

    #region exit states
    private void StateExitOff() { 
    }
    private void StateExitOn() { 
        lightObj.SetActive(false);
    }
    #endregion


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Door Key")) {
            var door = other.gameObject.GetComponentInParent<Door>();
            if (door.key == this.color) {
                door.ChangeState(DoorState.OPENING);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Light Area")) {
            inArea = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Light Area")) {
            inArea = false;
            ChangeState(State.OFF);
        }
    }
}
