using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable{
    void Activate(bool isPressed);
}

public class PressurePlate : MonoBehaviour
{
public GameObject[] targets;
public bool _isPressed;


    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Pad")){
        _isPressed = true;
        foreach(GameObject target in targets){
            if (target.TryGetComponent<IInteractable>(out IInteractable interactable)){
                interactable.Activate(_isPressed);
            }
            Debug.Log("Pad Enter");
            }
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Pad")){
        _isPressed = false;
        foreach(GameObject target in targets){
            if (target.TryGetComponent<IInteractable>(out IInteractable interactable)){
                interactable.Activate(_isPressed);
            }
            Debug.Log("Pad Exit");
            }
        }
    }
}
