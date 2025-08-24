using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GridManager gridManager;

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        // getting start and target nodes using their world positions
        Node startNode = gridManager.GetNodeFromWorldPoint(startPos);
        Node targetNode = gridManager.GetNodeFromWorldPoint(targetPos);

        // initializing open and closed lists
        List<Node> openList = new List<Node>() { startNode };
        List<Node> closedList = new List<Node>();

        // running the loop till open list exhausts
        while (openList.Count > 0)
        {
            Node currentNode = openList[0];    // current node in the open list

            for (int i = 1; i < openList.Count; i++)
            {
                // checking if the node in the open list is better than the current node, if yes, set that as current node
                if (openList[i].fCost < currentNode.fCost
                    || (openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost))
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetNode)    // if current node == target node -> retrace path and return it
            {
                return RetracePath(startNode, targetNode);
            }
            // handling all neighbors for the current node
            foreach (Node neighbor in gridManager.GetNeighbours(currentNode))
            {
                if (neighbor.isObstacle || closedList.Contains(neighbor)) continue;    //skip the neighbor if obstacle or already used

                int newCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);    // cost from the current node to neighbor
                if (!openList.Contains(neighbor) || newCostToNeighbor < neighbor.gCost)
                {
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.prevNode = currentNode;
                }
            }
        }

        return null;
    }

    public int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);    // x distance between them
        int distanceZ = Mathf.Abs(nodeA.gridZ - nodeB.gridZ);    // z distance between them

        if (distanceX < distanceZ)
        {
            return distanceX * 14 + (distanceZ - distanceX) * 10;
        }
        else
        {
            return distanceZ * 14 + (distanceX - distanceZ) * 10;
        }
    }

    // method for returning the actual path between two nodes using prevNode property
    private List<Node> RetracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();    // empty path list
        Node currentNode = targetNode;    // start retracing from target node

        while (currentNode != startNode)
        {
            path.Add(currentNode);    // add current node in the list
            currentNode = currentNode.prevNode;    // make the previous node as current node
        }

        path.Reverse();    // reverse the path list
        return path;
    }
}
