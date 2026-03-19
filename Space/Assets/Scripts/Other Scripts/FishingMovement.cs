using UnityEngine;

public class FishingMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Player playerScript;
    public GameObject playerFishing;
    FishPlayer playerFishingScript;
    MovementLogic playerMovementScript;

    public Transform highestPoint;
    public Transform lowestPoint;

    private float highestCor;
    private float lowestCor;

    public float maxSpeed = 200f;
    public float minSpeed = 100f;
    public float speed = 0f;
    private int direction = 0;
    private float speedResetCooldown = 2f;

    private float frame = 0f;
    private bool firstMovement = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovementScript = playerScript.GetComponent<MovementLogic>();
        playerFishingScript = GameObject.FindGameObjectWithTag("Player (Fishing)").GetComponent<FishPlayer>();

        Collider2D fishCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = playerFishing.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, fishCollider);

        highestCor = highestPoint.position.y;
        lowestCor = lowestPoint.position.y;
        float startingPosition = highestCor - 1f;
        transform.position = new Vector3(transform.position.x, startingPosition, transform.position.z);
    }

    void Update()
    {
        if (playerFishingScript.start)
        {
            frame += Time.deltaTime;
            UpdateSpeed();
            Move();
        }
    }

    private void UpdateSpeed()
    {
        if (frame >= speedResetCooldown || firstMovement)
        {
            frame = 0f;
            direction = Random.Range(0, 2) == 0 ? -1 : 1;
            speed = Random.Range(minSpeed, maxSpeed);
            firstMovement = false;
        }
    }

    private void Move()
    {   
        float movement = speed * Time.deltaTime * direction;
        float yCor = transform.position.y + movement;

        if (lowestCor < yCor && yCor < highestCor)
        {
            rigidBody.linearVelocity = new Vector2(0, movement);
        } else
        {
            playerMovementScript.ResetMovement(rigidBody);
        }
    }
}
