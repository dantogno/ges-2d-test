using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField]
    private float gravityModifier = 1f;

    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected Vector2 previousPosition;
    protected Vector2 currentPosition;
    protected Vector2 nextMovement;


    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        previousPosition = rb2d.position;
        Move(Physics2D.gravity * gravityModifier * Time.deltaTime);
        currentPosition = previousPosition + nextMovement;
        velocity = (currentPosition - previousPosition) / Time.deltaTime;

        rb2d.MovePosition(currentPosition);
        nextMovement = Vector2.zero;
    }

    private void Move(Vector2 moveAmount)
    {
        nextMovement += moveAmount;
    }
}
