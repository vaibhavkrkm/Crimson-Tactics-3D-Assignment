using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private Vector3 playerPos = new Vector3(0, 1.5f, 0);    // gridX = 0 and gridZ = 0 means bottom-left corner of the grid
    private Vector3 enemyPos = new Vector3(9, 1.5f, 9);    // gridX = 9 and gridZ = 9 means top-right corner of the grid
    private EnemyAI enemyAI;

    [HideInInspector] public string currentTurn;    // variable to maintain current turn ("player" or "enemy")

    void Start()
    {
        currentTurn = "player";
        player = Instantiate(player, playerPos, Quaternion.identity);    // spawning the player on the grid
        enemy = Instantiate(enemy, enemyPos, Quaternion.identity);    // spawning the enemy on the grid
        enemyAI = enemy.GetComponent<EnemyAI>();
    }

    private void Update()
    {
        if (currentTurn == "enemy")    // handling enemy turn (player turn is being handled in PlayerController.cs script)
        {
            enemyAI.MoveOnGrid(player.transform.position);
        }
    }
}
