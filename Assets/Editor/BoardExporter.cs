using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(BoardDesigner))]
public class BoardExporter : Editor
{
    private string _exportDirectory = "Board";
    private string _fileName = "Level.json";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var designer = (BoardDesigner)target;
        GUILayout.Space(10);

        if (GUILayout.Button("Create Empty Tile"))
        {
            designer.CreateEmptyTile();
        }
        if (GUILayout.Button("Create Flag Quiz Tile"))
        {
            designer.CreateFlagQuizTile();
        }
        if (GUILayout.Button("Create Text Quiz Tile"))
        {
            designer.CreateTextQuizTile();
        }

        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Export Directory");
        _exportDirectory = GUILayout.TextField(_exportDirectory);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("File Name");
        _fileName = GUILayout.TextField(_fileName);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        var path = Path.GetFullPath(Path.Combine(Application.dataPath, _exportDirectory, _fileName));
        GUILayout.Label($"Destination: {path}");
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Export"))
        {
            var json = designer.Export();
            File.WriteAllText(path, json);
            AssetDatabase.Refresh();
            Debug.Log($"Created {path}");
        }
    }
}
