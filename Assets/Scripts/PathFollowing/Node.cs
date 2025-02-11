using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public float X;
    public float Z;
    public bool IsWalkable;
    public Node Parent;

    public float GCost;
    public float HCost;
    public float FCost => GCost + HCost;

    public Node(float x, float z, bool isWalkable)
    {
        X = x;
        Z = z;
        IsWalkable = isWalkable;
    }
}

