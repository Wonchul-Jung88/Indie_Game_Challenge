using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MazeGeneratorScript))]
public class MazeGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MazeGeneratorScript myScript = (MazeGeneratorScript)target;
        if (GUILayout.Button("Build Maze"))
        {
            myScript.BuildMaze();
        }

        // Add a new button for clearing the maze
        if (GUILayout.Button("Clear Maze"))
        {
            myScript.ClearMaze();
        }
    }
}
