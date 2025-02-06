using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverMovement : MonoBehaviour
{
    public AStarPathfinding pathfinding;
    public Transform start;
    public Transform target;
    public float speed = 2f;

    private List<Node> path;
    private int currentPathIndex;

    void Start()
    {
        Node startNode = pathfinding.gridManager.GetNode(Mathf.RoundToInt(start.position.x), Mathf.RoundToInt(start.position.z));
        Node targetNode = pathfinding.gridManager.GetNode(Mathf.RoundToInt(target.position.x), Mathf.RoundToInt(target.position.z));

        path = pathfinding.FindPath(startNode, targetNode);

        if (path != null)
        {
            currentPathIndex = 0;
            StartCoroutine(FollowPath());
        }
        else
        {
            Debug.LogError("No path found");
        }
    }

    IEnumerator FollowPath()
    {
        while (currentPathIndex < path.Count)
        {
            Node currentNode = path[currentPathIndex];
            Vector3 targetPosition = new Vector3(currentNode.X, 0, currentNode.Y);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            currentPathIndex++;
        }

        // Simulate excavation and return to start
        Debug.Log("Excavating...");
        yield return new WaitForSeconds(2);
        Debug.Log("Returning to start...");
        Start();
    }
}

