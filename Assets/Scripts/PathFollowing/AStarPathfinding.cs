using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    public GridManager gridManager;

    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        List<Node> openList = new List<Node> { startNode };
        HashSet<Node> closedList = new HashSet<Node>();
        
        startNode.GCost = 0;
        startNode.HCost = GetDistance(startNode, targetNode);

        while (openList.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openList);
            if (currentNode == targetNode)
                return RetracePath(startNode, targetNode);

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.IsWalkable || closedList.Contains(neighbor)) continue;

                float newMovementCost = currentNode.GCost + GetDistance(currentNode, neighbor);
                if (newMovementCost < neighbor.GCost || !openList.Contains(neighbor))
                {
                    neighbor.GCost = newMovementCost;
                    neighbor.HCost = GetDistance(neighbor, targetNode);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }

    Node GetLowestFCostNode(List<Node> nodes)
    {
        Node lowestNode = nodes[0];
        foreach (Node node in nodes)
        {
            if (node.FCost < lowestNode.FCost || (node.FCost == lowestNode.FCost && node.HCost < lowestNode.HCost))
            {
                lowestNode = node;
            }
        }
        return lowestNode;
    }

    List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int col = -1; col <= 1; col++)
        {
            for (int row = -1; row <= 1; row++)
            {
                if (col == 0 && row == 0) continue; // Skip the center node (itself)

                int neighborCol = node.Col + col;
                int neighborRow = node.Row + row;

                // Ensure the neighbor is within grid bounds
                if (neighborCol >= 0 && 
                    neighborRow >= 0 && 
                    neighborCol < gridManager.gridWidth && 
                    neighborRow < gridManager.gridHeight)
                    {
                        Node neighbor = gridManager.GetNodeFromGrid(neighborCol, neighborRow);
                        if (neighbor != null)
                        {
                            neighbors.Add(neighbor);
                        }
                    }
                
            }
        }

        return neighbors;
    }

    

    float GetDistance(Node a, Node b)
    {
        float aX = gridManager.GetNodeCoords(a)[0];
        float aZ = gridManager.GetNodeCoords(a)[1];
        float bX = gridManager.GetNodeCoords(b)[0];
        float bZ = gridManager.GetNodeCoords(b)[1];
        int dstX = (int) Mathf.Abs(aX - bX);
        int dstz = (int) Mathf.Abs(aZ - bZ);
        return dstX + dstz;
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }
}

