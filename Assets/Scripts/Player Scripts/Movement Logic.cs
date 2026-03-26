using System;
using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    Player mainScript;
    public Rigidbody2D rigidBody;
    public LayerMask blocksLayer;

    public float speed = 5f;
    private float hMovement = 0f;
    private float vMovement = 0f;
    private bool spaceMovement;

    // Player Collisions
    private bool topSideCollision;
    private bool bottomSideCollision;
    private bool rightSideCollision;
    private bool leftSideCollision;

    private bool topSideCollisionOverride = false;
    private bool bottomSideCollisionOverride = false;
    private bool rightSideCollisionOverride = false;
    private bool leftSideCollisionOverride = false;

    Transform topSideCollider;
    Transform bottomSideCollider;
    Transform rightSideCollider;
    Transform leftSideCollider;

    private float colliderCheckRadius = 0.1f;

    void Start()
    {
        mainScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();

        // Colliders and RB
        topSideCollider = transform.Find("Top");
        bottomSideCollider = transform.Find("Bottom");
        rightSideCollider = transform.Find("Right");
        leftSideCollider = transform.Find("Left");
    }

    private void FixedUpdate()
    {
        // If overrided, will be true when collide with objects regardless if collide with blocks Layer
        topSideCollision = Physics2D.OverlapCircle(topSideCollider.position, colliderCheckRadius, blocksLayer) || topSideCollisionOverride;
        bottomSideCollision = Physics2D.OverlapCircle(bottomSideCollider.position, colliderCheckRadius, blocksLayer) || bottomSideCollisionOverride;
        rightSideCollision = Physics2D.OverlapCircle(rightSideCollider.position, colliderCheckRadius, blocksLayer) || rightSideCollisionOverride;
        leftSideCollision = Physics2D.OverlapCircle(leftSideCollider.position, colliderCheckRadius, blocksLayer) || leftSideCollisionOverride;
    }

    public void ResetMovement(Rigidbody2D rigidBody)
    {
        rigidBody.linearVelocity = new Vector2(0, 0);
    }

    public void Movement()
    {
        hMovement = Input.GetAxis("Horizontal");
        vMovement = Input.GetAxis("Vertical");

        // Remove diagonal speed buff
        Vector2 movement = Vector2.ClampMagnitude(new Vector2(hMovement, vMovement), 1f);

        bool move = movement != Vector2.zero;

        rigidBody.linearVelocity = movement * speed;

        // If running into a wall, disable X/Y axis movement in that direction
        if (move && ((topSideCollision && DirectionCheck("Top")) ||
        (bottomSideCollision && DirectionCheck("Bottom"))))
        {
            rigidBody.linearVelocity = new Vector2(speed * hMovement, 0);
        }
        else if (move && ((rightSideCollision && DirectionCheck("Right")) ||
        (leftSideCollision && DirectionCheck("Left"))))
        {
            rigidBody.linearVelocity = new Vector2(0, speed * vMovement);
        }
    }

    private bool DirectionCheck(string direction)
    { // Move Movement if statement check to here for cleanliness, else is so program doesnt crash
        if (direction == "Top")
        {
            return vMovement > 0;
        }
        if (direction == "Bottom")
        {
            return vMovement < 0;
        }
        if (direction == "Right")
        {
            return hMovement > 0;
        }
        if (direction == "Left")
        {
            return hMovement < 0;
        }
        else { throw new ArgumentException(); }
    }
}

/*
public void ConfigureMovementCollision(string direction, bool disable)
{ // if disable = true, override movement to disable in Movement()
    if (direction == "Top"){
        topSideCollisionOverride = disable;
    }
    if (direction == "Bottom"){
        bottomSideCollisionOverride = disable;
    }
    if (direction == "Right"){
        rightSideCollisionOverride = disable;
    }
    if (direction == "Left"){
        leftSideCollisionOverride = true;
    }
}*/