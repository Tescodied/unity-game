using UnityEngine;
using System;

public class FishPlayer : MonoBehaviour
{
    public float speed = 1000;
    public Rigidbody2D rigidBody;

    public float durationMovement = 0.1f;
    private float movementCounter = 0f;
    private float frame = 0f;
    private bool move = false;
    public bool start = false;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        frame += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            move = true;
            start = true;
            movementCounter = 0f;
        }

        if (move)
        {
            rigidBody.linearVelocity = new Vector2(0, speed);
            movementCounter += Time.deltaTime;
            if (movementCounter >= durationMovement)
            {
                move = false;
            }
        }
    }
}
