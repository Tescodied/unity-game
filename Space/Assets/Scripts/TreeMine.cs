using System.Collections;
using UnityEngine;

public class TreeMine : MonoBehaviour
{
    SpriteRenderer treeRenderer;
    PlayerTreeRange playerRangeScript;
    TreeSpawn parentControllerScript;
    Player playerScript;

    // Hex color: #956634
    private readonly Color damageColor = new Color32(0x95, 0x66, 0x34, 0xE6);
    private float durationDamageColor = 0.25f;
    private const float playerMineCooldown = 0.3f;
    private float playerMineCounter = 0f;
    private bool mineCounterEnabled = false;

    private int hitsNecessary = 3;
    private int hitsTaken = 0;

    public bool playerInRange;

    void Start()
    {
        treeRenderer = GetComponent<SpriteRenderer>();
        playerRangeScript = GetComponentInChildren<PlayerTreeRange>();
        parentControllerScript = GameObject.FindGameObjectWithTag("Tree Spawner").GetComponent<TreeSpawn>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (mineCounterEnabled)
        {
            playerMineCounter += Time.deltaTime;
            if (playerMineCounter >= playerMineCooldown)
            {
                mineCounterEnabled = false;
                playerMineCounter = 0;
            }
        }

        playerInRange = playerRangeScript.playerInRange;
        bool selfClosest = parentControllerScript.CalculateClosestTree() == gameObject;

        if (!mineCounterEnabled && playerInRange && Input.GetKeyDown(KeyCode.Space) && selfClosest && playerScript.axeOnPlayerHandBool)
        {
            StartCoroutine(MineSelf());
            mineCounterEnabled = true;
        }
    }

    private IEnumerator MineSelf()
    {
        if (hitsTaken == hitsNecessary - 1)
        {
            hitsTaken++;
            StartCoroutine(ShakeTree());
            yield return StartCoroutine(FlashDamage());
            parentControllerScript.DestroyTree(transform.position);
        }
        else if (hitsTaken < hitsNecessary - 1)
        {
            hitsTaken++;
            StartCoroutine(ShakeTree());
            yield return StartCoroutine(FlashDamage());
        }
    }

    private IEnumerator FlashDamage()
    {
        treeRenderer.color = damageColor;
        yield return new WaitForSeconds(durationDamageColor);
        // Set back to default
        treeRenderer.color = Color.white;
    }

    private IEnumerator ShakeTree()
    {
        Vector3 originalPosition = transform.position;

        float shakeDuration = 0.1f;
        float shakeAmount = 0.1f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offset = Random.Range(-shakeAmount, shakeAmount);
            transform.position = originalPosition + new Vector3(offset, 0f, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
}
