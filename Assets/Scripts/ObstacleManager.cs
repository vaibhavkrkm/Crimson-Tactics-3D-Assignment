using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;

    private void Start()
    {
        if (obstacleData == null || obstaclePrefab == null) return;    // if any variable is missing data, return

        // iterating over the grid list
        for (int i = 0; i < obstacleData.obstacleGrid.Count; i++)
        {
            if (obstacleData.obstacleGrid[i] == true)    // it's an obstacle
            {
                int x = i % 10;    // using modulo operator since columns (x axis) range from 0 to 9 in cycle
                int z = i / 10;    // using integer division to get the row (z axis) since rows change after each 10 tiles
                int y = 1;    // since cube is on 0 (in y axis), 1 will make the sphere appear on it's top

                GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(x, y, z), Quaternion.identity);
                obstacle.transform.SetParent(this.transform);
            }
        }
    }
}
