using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float cellSize = 1f;
    public GameObject obstaclePrefab;
    public GameObject bounds1;
    public GameObject bounds2;
    public int obstacleCount = 10;

    [HideInInspector]
    public float bounds1X;
    [HideInInspector]
    public float bounds1Z;
    [HideInInspector]
    public float bounds2X;
    [HideInInspector]
    public float bounds2Z;
    [HideInInspector]
    public float gridWidth;
    [HideInInspector]
    public float gridHeight;
    

    private Node[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        bounds1X = bounds1.transform.position.x;
        bounds1Z = bounds1.transform.position.z;
        bounds2X = bounds2.transform.position.x;
        bounds2Z = bounds2.transform.position.z;
        gridWidth = (bounds2X - bounds1X);
        gridHeight = (bounds2Z - bounds1Z);
        GenerateGrid();
        PlaceObstacles();
    }

    void GenerateGrid()
    {
        grid = new Node[(int) gridWidth, (int) gridHeight];
        for (int x = 0; x < (int) gridWidth; x++)
        {
            for (int z = 0; z < (int) gridHeight; z++)
            {
                grid[x, z] = new Node(x, z, true);
            }
        }
    }

    void PlaceObstacles()
    {

        for (int i = 0; i < obstacleCount; i++)
        {
            int x = (int) Random.Range(bounds1X, bounds2X);
            int z = (int) Random.Range(bounds1Z, bounds2Z);
            //grid[x, z].IsWalkable = false;
            Instantiate(obstaclePrefab, new Vector3(x * cellSize, 1.668f, z * cellSize), Quaternion.identity);
        }
    }

    public Node GetNode(int x, int z)
    {
        if (x >= bounds1X && z >= bounds1Z && x < bounds2X && z < bounds2Z)
            return grid[x, z];
        return null;
    }
}