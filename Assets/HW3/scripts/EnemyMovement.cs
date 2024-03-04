using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float despawnDistance = 3.0f;
    public Transform house;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the enemy has reached the house
        if (Vector3.Distance(transform.position, house.position) <= despawnDistance)
        {
            // Destroy the enemy GameObject
            Destroy(gameObject);
        }
        else
        {
            // Set the destination of the NavMeshAgent to the house
            agent.destination = house.position;
        }
    }
}
