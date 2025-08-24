using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cubePrefab;    // loading cube prefab using the inspector in Unity
    public Color cubeColor1;
    public Color cubeColor2;

    public int gridWidth = 10;
    public int gridHeight = 10;
    public float nodeLength = 1f;

    public ObstacleData obstacleData;

    private Node[,] grid;

    private void Awake()
    {
        CreateGrid();
    }

    void Start()
    {
        for (int z = 0; z < 10; z++)    // outer loop for handling z position of the cube generation
        {
            for (int x = 0; x < 10; x++)    // inner loop for handling x position of the cube generation
            {
                // spawning the cube
                GameObject cube = Instantiate(cubePrefab, new Vector3(x, 0, z), Quaternion.identity);
                cube.transform.SetParent(this.transform);    // setting the cube's parent to GridManager

                // getting CubeInfo script from the generated cube
                CubeInfo cubeInfo = cube.GetComponent<CubeInfo>();

                // changing the cubes color
                Renderer cubeRenderer = cube.GetComponent<Renderer>();    // getting the rendered component
                if (cubeColor1 != null && cubeColor2 != null)    // null check for avoiding errors
                {
                    if (z % 2 == 0)    // for when z is even
                    {
                        if (x % 2 == 0)
                        {
                            cubeRenderer.material.color = cubeColor1;    // if x is even, set to color 1
                        }
                        else
                        {
                            cubeRenderer.material.color = cubeColor2;    // if x is odd, set to color 2
                        }
                    }
                    else    // for when z is odd
                    {
                        if (x % 2 == 0)
                        {
                            cubeRenderer.material.color = cubeColor2;    // if x is even, set to color 2
                        }
                        else
                        {
                            cubeRenderer.material.color = cubeColor1;    // if x is odd, set to color 1
                        }
                    }
                }

                // setting the cube properties
                cubeInfo.gridX = x;
                cubeInfo.gridZ = z;
            }
        }
    }

    void CreateGrid()    // function for creating the virtual grid for pathfinding
    {
        grid = new Node[gridWidth, gridHeight];
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                Vector3 worldPosition = new Vector3(x * nodeLength, 0, z * nodeLength);    // getting world position for each node
                int index = z * gridWidth + x;    // index for accessing the obstacle data asset
                bool isObstacle = obstacleData.obstacleGrid[index];    // checking if the node is obstacle or not
                grid[x, z] = new Node(isObstacle, worldPosition, x, z);
            }
        }
    }

    public Node GetNodeFromWorldPoint(Vector3 worldPosition)    // function to get a node from a world point
    {
        int x = Mathf.RoundToInt(worldPosition.x / nodeLength);
        int z = Mathf.RoundToInt(worldPosition.z / nodeLength);

        // clamping x and z values so they are not out of bounds of the grid
        x = Mathf.Clamp(x, 0, gridWidth - 1);
        z = Mathf.Clamp(z, 0, gridHeight - 1);

        return grid[x, z];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0) continue;    // skip the current node itself

                int checkX = node.gridX + x;
                int checkZ = node.gridZ + z;

                if (checkX >= 0 && checkX < gridWidth && checkZ >= 0 && checkZ < gridHeight)
                {
                    neighbours.Add(grid[checkX, checkZ]);
                }
            }
        }

        return neighbours;
    }
}
