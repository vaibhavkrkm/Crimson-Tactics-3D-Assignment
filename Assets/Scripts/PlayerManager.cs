using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPos = new Vector3(0, 1.5f, 0);    // gridX = 0 and gridZ = 0 means bottom-left corner of the grid

    void Start()
    {
        Instantiate(player, playerPos, Quaternion.identity);
    }
}
