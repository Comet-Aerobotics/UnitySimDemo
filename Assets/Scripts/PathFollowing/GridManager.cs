using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float cellSize = 0.1f;
    public GameObject obstaclePrefab;
    public Transform bounds1;
    public Transform bounds2;
    public Transform startNode;
    public Transform targetNode;
    public int obstacleCount = 3;
    [HideInInspector]
    public int gridWidth;
    [HideInInspector]
    public int gridHeight;
    public GameObject gridSquare;
    private Node[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        gridWidth = Mathf.RoundToInt((bounds2.position.x - bounds1.position.x) * cellSize);
        gridHeight = Mathf.RoundToInt((bounds2.position.z - bounds1.position.z) * cellSize);
        GenerateGrid();
        PlaceObstacles(GetNode(startNode.position.x, startNode.position.z), GetNode(targetNode.position.x, targetNode.position.z));
    }

    void GenerateGrid()
    {
        grid = new Node[gridWidth, gridHeight];
        for (int x = Mathf.RoundToInt(bounds1.position.x); x < Mathf.RoundToInt(bounds2.position.x); x++)
        {
            for (int z = Mathf.RoundToInt(bounds1.position.z); z < Mathf.RoundToInt(bounds2.position.z); z++)
            {
                grid[x, z] = new Node(x, z, true);
                // Instantiate(gridSquare, new Vector3((x+0.5f) * cellSize, 0.1f, (z+0.5f) * cellSize), Quaternion.identity);
                if (grid[x, z] == null) {
                    Debug.LogError($"Node at ({x}, {z}) is NULL!");
                }
            }
        }
    }

    void PlaceObstacles(Node startNode, Node targetNode)
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            int x = UnityEngine.Random.Range(Mathf.RoundToInt(bounds1.position.x), Mathf.RoundToInt(bounds2.position.x));
            int z = UnityEngine.Random.Range(Mathf.RoundToInt(bounds1.position.z), Mathf.RoundToInt(bounds2.position.z));
            while((GetNode(x, z) == startNode) || (GetNode(x, z) == targetNode))
            {
                x = UnityEngine.Random.Range(Mathf.RoundToInt(bounds1.position.x), Mathf.RoundToInt(bounds2.position.x));
                z = UnityEngine.Random.Range(Mathf.RoundToInt(bounds1.position.z), Mathf.RoundToInt(bounds2.position.z));
            }
            grid[x, z].IsWalkable = false;
            Instantiate(obstaclePrefab, new Vector3(x * cellSize, 0.1f, z * cellSize), Quaternion.identity);
        }
    }

    public Node GetNode(float worldX, float worldZ)
    {
        int x = Mathf.RoundToInt(worldX - bounds1.position.x);
        int z = Mathf.RoundToInt(worldZ - bounds1.position.z);
        if (x < bounds1.position.x || z < bounds1.position.z || x >= bounds2.position.x || z >= bounds2.position.x)
        {
            Debug.LogError($"GetNode({x}, {z}) is out of bounds!");
            return null;
        }

        return grid[x, z];
    }

}