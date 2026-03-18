using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    private int spawnCount = 40;
    private float treeDistancing = 2f;

    PolygonCollider2D polygonCollider;
    public GameObject tree;
    GameObject player;

    public Sprite fullTree;
    public Sprite choppedTree;

    private List<Vector2> spawnedTreeCors = new List<Vector2>();

    private readonly List<GameObject> spawnedTrees = new List<GameObject>();

    Player playerScript;

    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        // Spawn Trees when loaded.
        // If we fail to place all trees (e.g. max attempts exceeded), destroy all spawned
        // trees, clear tracked positions, and retry from the beginning.
        const int maxRetries = 100;
        int retry = 0;
        bool succeeded = false;

        while (!succeeded && retry < maxRetries)
        {
            try
            {
                spawnedTreeCors.Clear();
                foreach (var tree in spawnedTrees)
                {
                    if (tree != null)
                        Destroy(tree);
                }
                spawnedTrees.Clear();

                for (int i = 0; i < spawnCount; i++)
                {
                    Spawn();
                }

                succeeded = true;
            }
            catch (System.InvalidOperationException)
            {
                retry++;
                Debug.LogWarning($"TreeSpawn: failed to place all trees on attempt {retry}/{maxRetries}; retrying...");

                // Loop will retry; any trees created this iteration were destroyed above.
            }
        }

        if (!succeeded)
        {
            Debug.LogError($"TreeSpawn: failed to place trees after {maxRetries} retries.");
        }
    }

    void Spawn()
    {
        Bounds bounds = polygonCollider.bounds;
        Vector2 newTreeCors;
        bool validPos;

        const int maxAttempts = 1000;
        int attempt = 0;

        do
        {
            validPos = true;

            if (++attempt > maxAttempts)
            {
                throw new System.InvalidOperationException(
                    $"Unable to place tree after {maxAttempts} attempts. ");
            }
            newTreeCors = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            // Check if point is inside polygon collider
            if (!polygonCollider.OverlapPoint(newTreeCors))
            {
                validPos = false;
            }

            foreach (Vector2 spawnedTree in spawnedTreeCors)
            {
                if (Vector2.Distance(spawnedTree, newTreeCors) < treeDistancing)
                {
                    validPos = false;
                    break;
                }
            }


        } while (!validPos);

        var instance = Instantiate(tree, newTreeCors, Quaternion.identity, transform);
        spawnedTrees.Add(instance);
        spawnedTreeCors.Add(newTreeCors);
    }

    public void DestroyTree(Vector3 treeCors)
    {
        for (int i = 0; i < spawnedTrees.Count; i++)
        {
            if (spawnedTrees[i].transform.position == treeCors)
            {
                playerScript.AddWood();
                SpriteRenderer treeRenderer = spawnedTrees[i].GetComponent<SpriteRenderer>();
                Collider2D treeCollider = spawnedTrees[i].GetComponent<Collider2D>();
                treeRenderer.sprite = choppedTree;
                treeCollider.enabled = false;
                spawnedTrees.RemoveAt(i);
                break;
            }
        }
    }

    public GameObject CalculateClosestTree()
    {
        Vector2 playerCors = (Vector2)player.transform.position;
        float smallestDistance = float.MaxValue;
        GameObject closestTree = null;

        foreach (GameObject tree in spawnedTrees)
        {
            TreeMine treeScript = tree.GetComponent<TreeMine>();
            if (!treeScript.playerInRange)
                continue;

            Vector2 treeCors = (Vector2)tree.transform.position;
            float distance = Vector2.Distance(treeCors, playerCors);

            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closestTree = tree;
            }
        }

        return closestTree;
    }
}