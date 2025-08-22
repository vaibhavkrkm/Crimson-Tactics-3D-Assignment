using UnityEngine;
using UnityEditor;

public class ObstaclePlacerTool : EditorWindow
{
    public ObstacleData obstacleData;

    [MenuItem("Tools/Obstacle Placer")]    // for making it appear in the toolbar
    public static void ShowWindow()
    {
        // Get existing open window or if none, make a new one.
        GetWindow<ObstaclePlacerTool>("Obstacle Placer");
    }

    void OnGUI()
    {
        GUILayout.Label("My Obstacle Grid", EditorStyles.boldLabel);    // title of the editor

        // accepting the actual obstacle data asset
        obstacleData = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data Asset", obstacleData, typeof(ObstacleData), false);

        if (obstacleData == null) return;    // return if the asset is not linked or null for some reason

        // initialize with false in the beginning
        if (obstacleData.obstacleGrid.Count != 100)
        {
            for (int i = 0; i < 100; i++)
            {
                obstacleData.obstacleGrid.Add(false);
            }
        }

        // drawing the toggles
        EditorGUILayout.LabelField("Obstacle Grid:");
        for (int y = 0; y < 10; y++)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < 10; x++)
            {
                int index = y * 10 + x;
                bool currentState = obstacleData.obstacleGrid[index];    // current state of a given tile
                bool newState = GUILayout.Toggle(currentState, "");    // new state after the user clicks the checkbox

                if (newState != currentState)    // if state changed by user
                {
                    obstacleData.obstacleGrid[index] = newState;    // modifying the actual asset as per the new state

                    // making sure Unity saves this asset when modified
                    EditorUtility.SetDirty(obstacleData);
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
