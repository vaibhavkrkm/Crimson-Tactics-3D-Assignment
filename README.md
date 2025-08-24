
# 📜 Crimson Tactics 3D Assignment in Unity

<img src="https://github.com/vaibhavkrkm/Crimson-Tactics-3D-Assignment/blob/main/assignment_overview.gif?raw=true" width=380 height=auto>

A small **grid-based implementation** for Crimson Tactics 3D game where the player and the enemy move using **A\* Pathfinding** algorithm avoiding obstacles in their path, featuring a **custom Unity editor** for level design.

## 🖇️ Implemented Features
- **Run-time grid generation** - A 10x10 grid of cubes is generated at run-time with provided obstacles in the path.
- **Custom Unity tool for placing obstacles** - Developed a custom Unity tool for placing obstacles on the grid.
- **A\* Pathfinding algorithm for player and enemy movement** - Used A* Pathfinding algorithm to correctly find the best possible path (if available) avoiding all obstacles between two tiles.
- **ScriptableObjects for obstacle data** - Obstacles data is stored within a scriptable object asset, seperating level design and game code.

## ▶️ How to Run

1. Open the project in Unity (preferably version 6000.0.36f1).
2. Open **SampleScene.unity** from the scenes folder.
3. Press **Play** button to run.
4. To place obstacles on the scene, stop the simulation and go to **Tools (in the menu bar)>> ObstaclePlacer** and attach the **ObstacleData1.asset** file, or you can create your own obstacle data asset (**Right-click on a folder >> Create >> Create Obstacle Data**) and use that as well. Then toggle all the cells you want to place an obstacle on and play the game again!
