using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    public GameObject Image;  // Reference to the Canvas to be toggled

    // Method to enable the Canvas
    public void EnableCanvas()
    {
        if (Image != null)
        {
            Image.gameObject.SetActive(true);
        }
    }

    // Method to disable the Canvas
    public void DisableCanvas()
    {
        if (Image != null)
        {
            Image.gameObject.SetActive(false);
        }
    }
}
