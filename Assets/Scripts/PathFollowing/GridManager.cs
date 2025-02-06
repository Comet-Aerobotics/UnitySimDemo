using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 20;
    public int gridHeight = 20;
    public float cellSize = 1f;
    public GameObject obstaclePrefab;

    private Node[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        PlaceObstacles();
    }

    void GenerateGrid()
    {
        grid = new Node[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y] = new Node(x, y, true);
            }
        }
    }

    void PlaceObstacles()
    {
        for (int i = 0; i < 30; i++) // Random obstacles
        {
            int x = Random.Range(0, gridWidth);
            int y = Random.Range(0, gridHeight);
            grid[x, y].IsWalkable = false;
            Instantiate(obstaclePrefab, new Vector3(x * cellSize, 0, y * cellSize), Quaternion.identity);
        }
    }

    public Node GetNode(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < gridWidth && y < gridHeight)
            return grid[x, y];
        return null;
    }
}