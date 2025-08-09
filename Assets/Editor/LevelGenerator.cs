using UnityEditor;
using UnityEngine;

public class LevelGenerator : EditorWindow
{
    private int gridWidth;
    private int gridLength;
    private GameObject tilePrefab;
    private int space = 10;
    private Transform gridParent;
    private GameObject[,] gridTile;

    [MenuItem("Tool/Level Generator")]

    public static void ShowWindow()
    {
        GetWindow<LevelGenerator>("Level Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
        gridWidth = EditorGUILayout.IntField("Grid Size X", gridWidth);
        gridLength = EditorGUILayout.IntField("Grid Size Z", gridLength);
        GUILayout.Space(space);

        GUILayout.Label("Title Prefab", EditorStyles.boldLabel);
        tilePrefab = (GameObject)EditorGUILayout.ObjectField("Tile Prefabe", tilePrefab, typeof(GameObject), false);
        GUILayout.Space(space);

        GUILayout.Label("Grid Parent", EditorStyles.boldLabel);
        gridParent = (Transform)EditorGUILayout.ObjectField("Grid Parent", gridParent, typeof(Transform), true);
        GUILayout.Space(space);

        if (GUILayout.Button("Generat Grid"))
        {
            GenerateGride();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            ClearGrid();
        }
    }

    private void GenerateGride()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("No Tile Dumb Dumb");
            return;
        }

        gridTile = new GameObject[gridWidth, gridWidth];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridLength; z++)
            {
                Vector3 position = new Vector3(x, 0, z);
                gridTile[x, z] = (GameObject)PrefabUtility.InstantiatePrefab(tilePrefab, gridParent);
                gridTile[x, z].transform.position = position;
                Debug.Log(position);
            }
        }

    }

    private void ClearGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridLength; z++)
            {

                DestroyImmediate(gridTile[x, z]);
            }
        }
    }
}
