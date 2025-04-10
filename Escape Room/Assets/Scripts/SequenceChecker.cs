using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class SequenceChecker : MonoBehaviour
{
    private enum State
    {
        WaitingForInput,
        Success, // if player does the correct sequence
        Failure // checks if player makes mistake
    }

    // starts in waiting for input
    private State currentState = State.WaitingForInput;

    // sequence that player must enter
    private readonly List<int> correctSequence = new List<int> { 5, 3, 2, 1, 4 };
    // tracks player input
    private List<int> playerInput = new List<int>();

    public GameObject Door;
    public LightRoom lightscript;

    private void Start()
    {
        Notes.OnBarHit += CheckInput;
        Door = GameObject.Find("DoorLogic");
        lightscript = Door.GetComponent<LightRoom>();
    }
    private void OnDestroy()
    {
        Notes.OnBarHit -= CheckInput;
    }

    // for xylo. bar hits
    private void CheckInput(int barNumber)
    {
        switch (currentState)
        {
            // handles input if waiting state
            case State.WaitingForInput:
                HandleInput(barNumber);
                break;

            case State.Success:
            case State.Failure:
                ResetFSM(); // clears input and resets state
                HandleInput(barNumber); 
                break;
        }
    }

    private void HandleInput(int barNumber)
    {
        playerInput.Add(barNumber);

        // check if sequence is correct so far
        for (int i = 0; i < playerInput.Count; i++)
        {
            if (playerInput[i] != correctSequence[i])
            {
                Debug.Log("Wrong sequence.");
                currentState = State.Failure;
                ResetFSM(); // clears input
                return;
            }
        }

        // sequence is correct
        if (playerInput.Count == correctSequence.Count)
        {
            Debug.Log("Correct sequence.");
            currentState = State.Success; // successful

        
            // plays beat back after completing
            SoundManager.Instance.Play(SoundType.SOLVED);
            lightscript.musicPuz = true;
        }

    }

    // resets state and clears input
    private void ResetFSM()
    {
        playerInput.Clear();
        currentState = State.WaitingForInput;
    }
}
