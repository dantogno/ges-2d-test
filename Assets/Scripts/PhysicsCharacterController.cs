using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCharacterController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float accelerationForce = 2f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody2D rb2D;
    private float horizontalInput;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void Update()
    {
        GetInput();

        if (Input.GetButtonDown("Jump"))
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        Move();

    }

    private void Move()
    {
        Vector2 newVelocity = rb2D.velocity;
        rb2D.AddForce(horizontalInput * accelerationForce * Vector2.right, ForceMode2D.Impulse);
        newVelocity.x = Mathf.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed);
        rb2D.velocity = newVelocity;
    }
}
