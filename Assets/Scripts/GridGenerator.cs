using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject cubePrefab;    // loading cube prefab using the inspector in Unity
    public Color cubeColor1;
    public Color cubeColor2;

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
}
