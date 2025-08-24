using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    private LevelManager levelManager;
    private GridManager gridManager;
    private Pathfinding pathfinder;

    private bool isMoving = false;
    public float moveSpeed = 5f;

    private void Awake()
    {
        // getting references
        levelManager = FindAnyObjectByType<LevelManager>();
        gridManager = FindAnyObjectByType<GridManager>();
        pathfinder = FindAnyObjectByType<Pathfinding>();
    }

    public void MoveOnGrid(Vector3 playerPos)
    {
        if (isMoving) return;    // return if enemy is already moving

        Node enemyNode = gridManager.GetNodeFromWorldPoint(transform.position);    // getting enemy node
        Node playerNode = gridManager.GetNodeFromWorldPoint(playerPos);    // getting node where player stands
        List<Node> validAdjacentNodes = new List<Node>();    // all valid adjacent nodes list

        // using for loop to get adjacent nodes of the player
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (Mathf.Abs(x - z) == 1)    // difference 1 means adjacent sides of the node
                {
                    int checkX = playerNode.gridX + x;
                    int checkZ = playerNode.gridZ + z;
                    
                    // checking if the adjacent nodes are not out of the grid
                    if (checkX >= 0 && checkX < gridManager.gridWidth && checkZ >= 0 && checkZ < gridManager.gridHeight)
                    {
                        // checking if the adjacent nodes are not obstacles
                        if (!gridManager.grid[checkX, checkZ].isObstacle)
                        {
                            validAdjacentNodes.Add(gridManager.grid[checkX, checkZ]);    // adding this node to valid adjacent nodes list
                        }
                    }
                }
            }
        }

        Node bestTarget = null;    // best target variable
        List<Node> bestPath = new List<Node>();    // path to best target list

        // iterating over all valid adjacent nodes in the list
        foreach (Node node in validAdjacentNodes)
        {
            // finding path for the current node
            List<Node> currentPath = pathfinder.FindPath(enemyNode.worldPosition, node.worldPosition);

            if (currentPath != null)    // if a path is found, i.e., not null
            {
                if (bestPath.Count == 0 || currentPath.Count < bestPath.Count)    // if the path is better than the previous one (or it's the first path we found)
                {
                    // setting best path and best target variables
                    bestPath = currentPath;
                    bestTarget = node;
                }
            }
        }

        // now moving the player using coroutine
        StartCoroutine(MoveAlongPath(bestPath));
    }

    // movement coroutine
    private IEnumerator MoveAlongPath(List<Node> path)
    {
        isMoving = true;

        // iterating through every node in the path
        foreach (Node node in path)
        {
            Vector3 targetPosition = new Vector3(node.worldPosition.x, 1.5f, node.worldPosition.z);    // world position of the node in the path
            while (transform.position != targetPosition)    // runnimg the loop till the enemy reaches the node's world position
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);    // moving towards the target position (current node in the path)
                yield return null;    // wait for next frame
            }
        }

        levelManager.currentTurn = "player";    // changing the turn to player after enemy movement is complete
        isMoving = false;
    }
}
