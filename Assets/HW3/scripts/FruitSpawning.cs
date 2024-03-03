using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // Array of fruit prefabs
    public float spawnInterval = 1.0f;
    public float fruitLifetime = 6.0f; // Lifetime of each fruit
    public Collider fallingRegion;
    public float fruitMass = 1.0f; // Adjust mass for fruits

    private float timer;

    void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a fruit
        if (timer >= spawnInterval)
        {
            // Spawn a random fruit
            SpawnRandomFruit();

            // Reset timer
            timer = 0f;
        }
    }

    void SpawnRandomFruit()
    {
        // Select a random fruit prefab from the array
        GameObject randomFruitPrefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

        // Randomly determine spawn position within the falling region
        Vector3 spawnPosition = new Vector3(
            Random.Range(fallingRegion.bounds.min.x, fallingRegion.bounds.max.x),
            fallingRegion.bounds.min.y - 3, // Spawn from unser the platform
            Random.Range(fallingRegion.bounds.min.z, fallingRegion.bounds.max.z)
        );

        // Instantiate random fruit at spawn position
        GameObject fruit = Instantiate(randomFruitPrefab, spawnPosition, Quaternion.identity);

        fruit.transform.localScale = new Vector3(20f, 20f, 20f);

        // Add Rigidbody component and adjust mass
        Rigidbody fruitRigidbody = fruit.AddComponent<Rigidbody>();
        MeshCollider fruitCollider = fruit.AddComponent<MeshCollider>();

        if (fruitRigidbody != null)
        {
            fruitRigidbody.useGravity = true;
            fruitRigidbody.mass = fruitMass; // Adjust mass
            fruitRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous; // Set collision detection to Continuous
        }

        if (fruitCollider != null)
        {
            fruitCollider.convex = true;
        }

        // Add a script to handle staying and disappearing after hitting the floor
        FruitLifetimeHandler handler = fruit.AddComponent<FruitLifetimeHandler>();
        handler.lifetime = fruitLifetime;
    }
}

public class FruitLifetimeHandler : MonoBehaviour
{
    public float lifetime = 6.0f; // Total lifetime of the fruit
    private float timer = 0.0f;


    void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            // If the total lifetime of the fruit is over, destroy it
            Destroy(gameObject);
        }
    }
}
