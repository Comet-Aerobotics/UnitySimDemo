using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        float xDir = rb.velocity.x;
        float zDir = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        if(zDir == 0) xDir = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; else xDir = rb.velocity.x;
        rb.velocity = new Vector3(xDir, rb.velocity.y, zDir);
    }
}
