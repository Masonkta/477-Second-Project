using System.Collections.Generic;
using UnityEngine;

public class SequenceChecker : MonoBehaviour
{
    // correct sequence
    private readonly List<int> correctSequence = new List<int> { 5, 3, 2, 1, 4 };
    private List<int> playerInput = new List<int>();

    private void OnEnable()
    {
        Notes.OnBarHit += CheckInput;
    }

    private void OnDisable()
    {
        Notes.OnBarHit -= CheckInput;
    }

    private void CheckInput(int barNumber)
    {
        playerInput.Add(barNumber);

       
        for (int i = 0; i < playerInput.Count; i++)
        {
            if (playerInput[i] != correctSequence[i])
            {
                Debug.Log("Wrong sequence");
                playerInput.Clear();
                return;
            }
        }

       // check if sequence is correct
    
        if (playerInput.Count == correctSequence.Count)
        {
            Debug.Log("Testing sequence");
            playerInput.Clear(); 
        }
    }
}
