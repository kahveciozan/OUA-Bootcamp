using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Third person character controller like Fall Guys
/// </summary>
public class ThirdPersonCharacterController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true; // Prevents the Rigidbody from rotating
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter");
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Collision Ground Enter");
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision exit");
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Collision Ground Exit");

            isGrounded = false;
        }
    }

}
