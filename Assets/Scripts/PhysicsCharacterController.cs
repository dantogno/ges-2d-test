using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCharacterController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float accelerationForce = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private PhysicsMaterial2D playerStoppingPhysicsMaterial, 
        playerMovingPhysicsMaterial, playerFallingPhysicsMaterial;
    [SerializeField] private Collider2D playerGroundCollider, checkForGroundTrigger;
    [SerializeField] private LayerMask whatIsGround;


    private Rigidbody2D rb2D;
    private float horizontalInput;
    private bool isOnGround;
    private ContactFilter2D groundContactFilter;
    private Collider2D[] groundHitDetectionArray = new Collider2D[16];

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        checkForGroundTrigger.isTrigger = true;
        groundContactFilter.useTriggers = false;
        groundContactFilter.SetLayerMask(whatIsGround);
        groundContactFilter.useLayerMask = true;
    }

    private void GetMoveInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void Update()
    {
        GetMoveInput();

        if (Input.GetButtonDown("Jump") && isOnGround)
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        UpdateIsOnGround();
        UpdateFriction(horizontalInput);
        Move();
     
    }

    private void UpdateIsOnGround()
    {
        // TODO: OverlapCircleNonAlloc
        int hitGround = checkForGroundTrigger.OverlapCollider(groundContactFilter, groundHitDetectionArray);
        isOnGround = hitGround > 0;
        Debug.Log($"on ground: {isOnGround}");
    }

    private void UpdateFriction(float xInput)
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
