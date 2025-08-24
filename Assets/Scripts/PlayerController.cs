using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public float moveSpeed = 5f;
    private Pathfinding pathfinder;
    private bool isMoving = false;    // variable to maintain if the player is moving
    private int gridLayerMask;

    private void Start()
    {
        gridLayerMask = LayerMask.GetMask("Grid");    // getting grid layer mask so that raycast can only hit this layer
        pathfinder = FindAnyObjectByType<Pathfinding>();    // getting pathfinding reference
        mainCamera = FindAnyObjectByType<Camera>();
    }

    private void Update()
    {
        if (isMoving) return;    // disable input if already moving

        if (Input.GetMouseButtonDown(0))    // when left mouse button is pressed
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, maxDistance: 200f, layerMask: gridLayerMask))
            {
                List<Node> path = pathfinder.FindPath(transform.position, hit.point);    // finding the path using FindPath method

                if (path != null && path.Count > 0)    // if there are nodes to move
                {
                    StartCoroutine(MoveAlongPath(path));    // start the movement coroutine
                }
            }
        }
    }

    // movement coroutine
    private IEnumerator MoveAlongPath(List<Node> path)
    {
        isMoving = true;

        // iterating through every node in the path
        foreach (Node node in path)
        {
            Vector3 targetPosition = new Vector3(node.worldPosition.x, 1.5f, node.worldPosition.z);    // world position of the node in the path
            while (transform.position != targetPosition)    // runnimg the loop till the player reaches the node's world position
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);    // moving towards the target position (current node in the path)
                yield return null;    // wait for next frame
            }
        }

        isMoving = false;
    }
}
