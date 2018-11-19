using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsCharacterController : MonoBehaviour
{
    #region Unity editor Serialized Fields
    [SerializeField]
    private float maxSpeed = 10f;

    [SerializeField]
    private float accelerationForce = 2f;

    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private PhysicsMaterial2D playerStoppingPhysicsMaterial, 
        playerMovingPhysicsMaterial, playerFallingPhysicsMaterial;

    [SerializeField]
    private Collider2D playerGroundCollider, checkForGroundTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilterNew;

    [SerializeField]
    private Animator animator;

    #endregion
    #region Private Fields
    private Rigidbody2D rigidbody2D;
    private float horizontalInput;
    private bool isOnGround, isFacingRight = true;
    private Collider2D[] groundHitDetectionArray = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    private const string horizontalInputAnimationParameter = "xInput",
        yVelocityAnimationParameter = "yVelocity", isOnGroundAnimationParameter = "isOnGround";
    #endregion
    #region Monobehaviour Functions
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        checkForGroundTrigger.isTrigger = true;
    }

    private void Update()
    {
        GetMoveInput();
        HandleJumpInput();
        UpdateAnimationParameters();
    }

    private void FixedUpdate()
    {
        UpdateIsOnGround();
        UpdatePhysicsMaterial(horizontalInput);
        Move();
        UpdateDirectionCharacterFacing();
    }
    #endregion
    #region Public Functions
    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.SetIsActivated(false);

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }

    // TODO: death animation

    public void Respawn()
    {
        if (currentCheckpoint == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
        {
            rigidbody2D.transform.position = currentCheckpoint.transform.position;
            rigidbody2D.velocity = Vector2.zero;
        }
    }
    #endregion
    #region Private Functions
    private void GetMoveInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
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
    }
    private void UpdateAnimationParameters()
    {
        animator.SetFloat(horizontalInputAnimationParameter, Mathf.Abs(horizontalInput));
        animator.SetFloat(yVelocityAnimationParameter, rigidbody2D.velocity.y);
        animator.SetBool(isOnGroundAnimationParameter, isOnGround);
    }

    private void UpdateDirectionCharacterFacing()
    {
        if (horizontalInput > 0 && !isFacingRight)
            FlipCharacter();
        else if (horizontalInput < 0 && isFacingRight)
            FlipCharacter();
    }

    private void Move()
    {
        Vector2 newVelocity = rigidbody2D.velocity;
        rigidbody2D.AddForce(horizontalInput * accelerationForce * Vector2.right, ForceMode2D.Impulse);
        newVelocity.x = Mathf.Clamp(rigidbody2D.velocity.x, -maxSpeed, maxSpeed);
        rigidbody2D.velocity = newVelocity;
    }

    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector3 horizontallyFlippedScale = transform.localScale;
        horizontallyFlippedScale.x *= -1;
        transform.localScale = horizontallyFlippedScale;
    }
    #endregion
}
