using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float cellSize;
    public GameObject obstaclePrefab;
    public Transform bounds1;
    public Transform bounds2;
    public Transform startNode;
    public Transform targetNode;
    [HideInInspector]
    public float robotPerimeter;
    public int obstacleCount;
    [HideInInspector]
    public int gridWidth;
    [HideInInspector]
    public int gridHeight;
    public GameObject gridSquare;
    [HideInInspector]
    public GameObject nonWalkableGridSquare;
    private Node[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        gridWidth = Mathf.RoundToInt((bounds2.position.x - bounds1.position.x)/cellSize);
        gridHeight = Mathf.RoundToInt((bounds2.position.z - bounds1.position.z)/cellSize);
        Debug.Log(gridWidth + " | " + gridHeight);
        robotPerimeter = 0.767f;
        nonWalkableGridSquare = Instantiate(gridSquare);
        nonWalkableGridSquare.GetComponent<Renderer>().material.color = Color.red;
        GenerateGrid();
        PlaceObstacles();
        AccountForRobotPerimeter();
    }

    void GenerateGrid()
    {
        grid = new Node[gridWidth, gridHeight];
        gridSquare.transform.localScale = new Vector3(cellSize, 0.00001f, cellSize);
        gridSquare.GetComponent<Collider>().enabled = false;
        nonWalkableGridSquare.transform.localScale = new Vector3(cellSize, 0.00001f, cellSize);
        nonWalkableGridSquare.GetComponent<Collider>().enabled = false;
        for (int col = 0; col < gridWidth; col++)
        {
            for (int row = 0; row < gridHeight; row++)
            {
                float x = col*cellSize;
                float z = row*cellSize;
                grid[col, row] = new Node(col, row, true);
                // Debug.Log(col + " | " + row);
                if (grid[col, row].IsWalkable)
                {
                    Instantiate(gridSquare, new Vector3(x + cellSize / 2, 0.1f, z + cellSize / 2), Quaternion.identity);
                }
                else
                {
                    Instantiate(nonWalkableGridSquare, new Vector3(x + cellSize / 2, 0.1f, z + cellSize / 2), Quaternion.identity);
                }
                // Instantiate(gridSquare, new Vector3(x + cellSize/2, 0.1f, z + cellSize/2), Quaternion.identity);
                if (grid[col, row] == null) {
                    Debug.LogError($"Node at ({x}, {z}) is NULL!");
                }
            }
        }
    }

    void PlaceObstacles()
    {
        int perimeterCells = Mathf.CeilToInt(robotPerimeter / cellSize);

        for (int i = 0; i < obstacleCount; i++)
        {
            int col = UnityEngine.Random.Range(0, gridWidth);
            int row = UnityEngine.Random.Range(0, gridHeight);

            while (!IsValidObstaclePosition(col, row, perimeterCells) || IsOOB(col, row))
            {
                col = UnityEngine.Random.Range(0, gridWidth);
                row = UnityEngine.Random.Range(0, gridHeight);
            }

            if (!IsOOB(col, row))
            {
                grid[col, row].IsWalkable = false;
                Instantiate(obstaclePrefab, new Vector3(col * cellSize + cellSize / 2, 0.1f, row * cellSize + cellSize / 2), Quaternion.identity);
            }
        }
    }

    bool IsValidObstaclePosition(int col, int row, int perimeterCells)
    {
        if (!grid[col, row].IsWalkable)
        {
            return false;
        }

        Node startNodeGrid = GetNodeFromCoords(startNode.position.x, startNode.position.z);
        Node targetNodeGrid = GetNodeFromCoords(targetNode.position.x, targetNode.position.z);

        for (int i = -perimeterCells; i <= perimeterCells; i++)
        {
            for (int j = -perimeterCells; j <= perimeterCells; j++)
            {
                int checkCol = col + i;
                int checkRow = row + j;

                if (!IsOOB(checkCol, checkRow))
                {
                    if (grid[checkCol, checkRow] == startNodeGrid || grid[checkCol, checkRow] == targetNodeGrid)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    void AccountForRobotPerimeter()
    {
        int perimeterCells = Mathf.CeilToInt(robotPerimeter / cellSize);

        for (int col = 0; col < gridWidth; col++)
        {
            for (int row = 0; row < gridHeight; row++)
            {
                if (!grid[col, row].IsWalkable)
                {
                    for (int i = -perimeterCells; i <= perimeterCells; i++)
                    {
                        for (int j = -perimeterCells; j <= perimeterCells; j++)
                        {
                            int checkCol = col + i;
                            int checkRow = row + j;

                            if (!IsOOB(checkCol, checkRow))
                            {
                                grid[checkCol, checkRow].IsWalkable = false;
                            }
                        }
                    }
                }
            }
        }
    }

    bool IsOOB(int col, int row)
    {
        // this needs to be like this because the grid is 0-indexed and gridWidth and gridHeight are within bounds
        if (col < 0 || row < 0 || col > gridWidth || row > gridHeight)
        {
            Debug.LogError($"GetNode({col}, {row}) is out of bounds!");
            return true;
        }
        return false;
    }

    public float[] GetNodeCoords(Node node)
    {
        return new float[] { node.Col * cellSize, node.Row * cellSize };
    }

    public Node GetNodeFromCoords(float nodeX, float nodeZ)
    {
        int col = Mathf.RoundToInt(nodeX / cellSize);
        int row = Mathf.RoundToInt(nodeZ / cellSize);
        return IsOOB(col, row) ? null : grid[col, row];
    }

    public Node GetNodeFromGrid(int col, int row)
    {
        return IsOOB(col, row) ? null : grid[col, row];
    }

}