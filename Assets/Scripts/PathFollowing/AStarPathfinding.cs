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

        int nodeX = Mathf.RoundToInt(node.X - gridManager.bounds1.position.x);
        int nodeZ = Mathf.RoundToInt(node.Z - gridManager.bounds1.position.z);

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0) continue; // Skip the center node (itself)

                int neighborX = nodeX + x;
                int neighborZ = nodeZ + z;

                // Ensure the neighbor is within grid bounds
                if (neighborX >= gridManager.bounds1.position.x && neighborZ >= gridManager.bounds1.position.z && 
                    neighborX < gridManager.bounds2.position.x && 
                    neighborZ < gridManager.bounds2.position.z){
                        Node neighbor = gridManager.GetNode(node.X + x, node.Z + z);
                        if (neighbor != null)
                            neighbors.Add(neighbor);
                    }
            }
        }

        return neighbors;
    }

    

    float GetDistance(Node a, Node b)
    {
        int dstX = (int) Mathf.Abs(a.X - b.X);
        int dstz = (int) Mathf.Abs(a.Z - b.Z);
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

