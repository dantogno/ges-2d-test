using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsCharacterController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float accelerationForce = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private PhysicsMaterial2D playerStoppingPhysicsMaterial, 
        playerMovingPhysicsMaterial, playerFallingPhysicsMaterial;
    [SerializeField] private Collider2D playerGroundCollider, checkForGroundTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilterNew;

    private Rigidbody2D rb2D;
    private float horizontalInput;
    private bool isOnGround;
    private Collider2D[] groundHitDetectionArray = new Collider2D[16];
    private Checkpoint currentCheckpoint;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        checkForGroundTrigger.isTrigger = true;
    }

    private void GetMoveInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.SetIsActivated(false);

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }

    public void Respawn()
    {
        if (currentCheckpoint == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            rb2D.transform.position = currentCheckpoint.transform.position;
    }

    private void Update()
    {
        GetMoveInput();

        HandleJumpInput();
    }

    private void FixedUpdate()
    {
        UpdateIsOnGround();
        UpdatePhysicsMaterial(horizontalInput);
        Move();
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void UpdateIsOnGround()
    {
        int hitGround = checkForGroundTrigger.OverlapCollider(groundContactFilterNew, groundHitDetectionArray);
        isOnGround = hitGround > 0;
    }

    private void UpdatePhysicsMaterial(float xInput)
    {
        if (!isOnGround)
        {
            playerGroundCollider.sharedMaterial = playerFallingPhysicsMaterial;
        }
        else if (Mathf.Abs(xInput) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
        //TODO if not on ground, use falling phys material
    }

    private void Move()
    {
        Vector2 newVelocity = rb2D.velocity;
        rb2D.AddForce(horizontalInput * accelerationForce * Vector2.right, ForceMode2D.Impulse);
        newVelocity.x = Mathf.Clamp(rb2D.velocity.x, -maxSpeed, maxSpeed);
        rb2D.velocity = newVelocity;
    }
}
