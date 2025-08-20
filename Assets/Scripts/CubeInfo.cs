using UnityEngine;

public class CubeInfo : MonoBehaviour
{
    // cube properties
    [HideInInspector] public int gridX;    // x position in grid
    [HideInInspector] public int gridZ;    // y position in grid (there's no y position as y will be constant for all cubes)
}
