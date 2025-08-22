using TMPro;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public Camera mainCamera;
    public TMP_Text cubeInfoText;
    private int obstacleLayerMask;

    private void Start()
    {
        obstacleLayerMask = LayerMask.GetMask("Obstacle");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PerformRaycast();    // perform raycast if user presses left mouse button
        }
    }

    void PerformRaycast()
    {
        RaycastHit hit;    // for storing raycast information
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, maxDistance: 200f, layerMask: ~obstacleLayerMask))    // ignoring obstacle layer
        {
            GameObject cube = hit.collider.gameObject;    // getting the hit game object
            CubeInfo cubeInfo = cube.GetComponent<CubeInfo>();    // getting CubeInfo script

            // updating on UI
            cubeInfoText.text = $"x: {cubeInfo.gridX}, z:{cubeInfo.gridZ}";
        }
    }
}
