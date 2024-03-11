using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject[] Prefabs; // Array of prefabs
    public float spawnInterval = 6.0f;
 
    public Collider fallingRegion;
    public float enemyMass = 1.0f; // Adjust mass 

    private float timer;

    void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        // Check if it's time to spawn 
        if (timer >= spawnInterval)
        {
            // Spawn 
            SpawnRandom();

            // Reset timer
            timer = 0f;
        }
    }

    void SpawnRandom()
    {
        // Select a random fruit prefab from the array
        GameObject randomPrefab = Prefabs[Random.Range(0, Prefabs.Length)];

        // Randomly determine spawn position within the falling region
        Vector3 spawnPosition = new Vector3(
            Random.Range(fallingRegion.bounds.min.x, fallingRegion.bounds.max.x),
            fallingRegion.bounds.min.y, // Spawn from the platform
            Random.Range(fallingRegion.bounds.min.z, fallingRegion.bounds.max.z)
        );

        // Instantiate random fruit at spawn position
        GameObject enemy = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

        enemy.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        SkinnedMeshRenderer skinnedRenderer = enemy.GetComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = enemy.GetComponent<MeshRenderer>();

        if (skinnedRenderer != null)
        {
            skinnedRenderer.enabled = true;
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
            else
            {
                meshRenderer = enemy.AddComponent<MeshRenderer>(); // Adding MeshRenderer if not present
                meshRenderer.enabled = false;
                meshRenderer.material = new Material(skinnedRenderer.material);
            }
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer component not found on the enemy GameObject.");
        }

        Transform houseTransform = GameObject.FindGameObjectWithTag("House").transform;

        // Add EnemyMovement script to the spawned entity
        EnemyMovement enemyMovement = enemy.AddComponent<EnemyMovement>();

        // Assign NavMeshAgent and house variables
        if (enemyMovement != null)
        {
            enemyMovement.agent = enemy.GetComponent<NavMeshAgent>();
            enemyMovement.house = houseTransform; // Assign your house transform here
        }

        // Add NavMeshAgent component
        NavMeshAgent navMeshAgent = enemy.AddComponent<NavMeshAgent>();

        // Configure NavMeshAgent properties
        if (navMeshAgent != null)
        {
            navMeshAgent.radius = 0.2f;
            navMeshAgent.height = 1f; 
            navMeshAgent.speed = 1f; 
        }


        MeshCollider enemyCollider = enemy.AddComponent<MeshCollider>();

        if (enemyCollider != null)
        {
            enemyCollider.convex = true;
        }
    }
}

