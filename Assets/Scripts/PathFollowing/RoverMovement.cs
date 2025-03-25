using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoverMovement : MonoBehaviour
{
    public AStarPathfinding pathfinding;
    [HideInInspector]
    public GridManager gridManager;
    public Transform start;
    public Transform target;
    public float moveSpeed;
    public float turnSpeed;

    private List<Node> path;
    private int currentPathIndex;
    IEnumerator Start()
    {
        gridManager = pathfinding.gridManager;
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(0.1f);

        Node startNode = pathfinding.gridManager.GetNodeFromCoords(Mathf.RoundToInt(start.position.x), Mathf.RoundToInt(start.position.z));
        Node targetNode = pathfinding.gridManager.GetNodeFromCoords(Mathf.RoundToInt(target.position.x), Mathf.RoundToInt(target.position.z));

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
            Vector3 targetPosition = new Vector3(gridManager.GetNodeCoords(currentNode)[0], transform.position.y, gridManager.GetNodeCoords(currentNode)[1]);
            
            while (Vector3.Angle(transform.forward, targetPosition - transform.position) > 1f)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
                transform.Rotate(Vector3.up, angle * Time.deltaTime * turnSpeed);
                yield return null;
            }

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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

