using UnityEngine;

public class Node
{
    public int gridX;    // x coordinate of node in the grid
    public int gridZ;    // z coordinate of node in the grid
    public Vector3 worldPosition;    // actual world position of the node
    public bool isObstacle;    // bool to store if the node is an obstacle or not

    public int gCost;    // gCost means the actual cost from the initial node to current node
    public int hCost;    // hCost means an estimated cost from the current node to the final node
    public Node prevNode;    // previous node in the path

    // constructor to initialize all the required parameters of a node
    public Node(bool isObstacle, Vector3 worldPosition, int gridX, int gridZ)
    {
        this.isObstacle = isObstacle;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridZ = gridZ;
    }

    public int fCost => this.gCost + this.hCost;

}
