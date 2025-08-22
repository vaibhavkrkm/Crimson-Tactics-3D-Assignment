using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle Data", menuName = "Create Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public List<bool> obstacleGrid = new List<bool>();    // list containing info if a cube is an obstacle or not
}
