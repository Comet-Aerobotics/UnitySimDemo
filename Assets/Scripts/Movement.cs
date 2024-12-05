using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    public float turnSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        // float xDir = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(transform.forward * move * Time.deltaTime, Space.World);


        // rotation stuff (play with this later when u code tank drive)
        float rotation = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        Vector3 m_EulerAngleVelocity = new Vector3(0, rotation, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
