using UnityEngine;

public class PlayerTreeRange : MonoBehaviour
{
    public bool playerInRange;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
