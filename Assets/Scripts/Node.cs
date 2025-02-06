using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int X;
    public int Y;
    public bool IsWalkable;
    public Node Parent;

    public float GCost;
    public float HCost;
    public float FCost => GCost + HCost;

    public Node(int x, int y, bool isWalkable)
    {
        X = x;
        Y = y;
        IsWalkable = isWalkable;
    }
}

