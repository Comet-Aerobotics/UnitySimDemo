using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoverMovement : MonoBehaviour
{
    public AStarPathfinding pathfinding;
    public Transform start;
    public Transform target;
    public float speed;

    private List<Node> path;
    private int currentPathIndex;
    IEnumerator Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(0.1f);

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
            Vector3 targetPosition = new Vector3(currentNode.X, 0, currentNode.Z);
            float distance = Vector3.Distance(transform.position, targetPosition);

            while (distance > 0.1f)
            {
                // Rotate to face the target node and move towards it
                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                // Rotate to face the target node
                while (Quaternion.Angle(transform.rotation, lookRotation) > 10f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, speed * Time.deltaTime);
                    yield return null;
                }

                // Move towards the target node
                while (distance > 0.1f)
                {
                    transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
                    distance = Vector3.Distance(transform.position, targetPosition);
                    yield return null;
                }
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

