using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    public GameObject Image;  // Reference to the Canvas to be toggled
    public GameObject player;
    public Transform playerCam;
    MeshRenderer renderer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = player.transform.Find("Camera Offset").Find("Main Camera");
        renderer = GetComponent<MeshRenderer>();

    }

    private void Update()
    {
        if (Image.activeInHierarchy) {
            Image.transform.position = playerCam.position + playerCam.forward * 1f;
            Image.transform.rotation = Quaternion.LookRotation(Image.transform.position - playerCam.position);
        }
        renderer.enabled = !Image.activeInHierarchy;
    }

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
