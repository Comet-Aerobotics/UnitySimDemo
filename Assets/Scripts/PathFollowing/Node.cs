using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int Col;
    public int Row;
    public bool IsWalkable;
    public Node Parent;

    public float GCost;
    public float HCost;
    public float FCost => GCost + HCost;
    public float cellSize;

    public Node(int col, int row, bool isWalkable)
    {
        Col = col;
        Row = row;
        IsWalkable = isWalkable;
    }
}

