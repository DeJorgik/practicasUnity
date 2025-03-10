using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerarObstaculos : MonoBehaviour
{
    public GameObject objectToInstantiate; // The object to be spawned
    public float timeBetweenSpawns = 2f;    // Time in seconds between spawns
    public Vector3 initialSpeed = Vector3.zero; // Initial speed of the spawned object
    public Vector3 initialRotation = Vector3.zero; // Initial rotation of the spawned object (Euler angles)
    public bool spawnOnStart = true;       // Should it start spawning right away?

    protected float timer;

    void Start()
    {

        timer = timeBetweenSpawns;
        if (spawnOnStart)
        {
            SpawnObject(); // Spawn the first object immediately if needed.
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnObject();
            timer = timeBetweenSpawns; // Reset the timer
        }
    }

    protected void SpawnObject()
    {
        if (objectToInstantiate != null) // Check if an object is assigned
        {
            // Instantiate with the specified rotation:
            Quaternion spawnRotation = Quaternion.Euler(initialRotation); // Convert Euler angles to Quaternion
            GameObject spawnedObject = Instantiate(objectToInstantiate, transform.position, spawnRotation);


            // Apply initial speed
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>(); // Check for a Rigidbody
            if (rb != null)
            {
                rb.velocity = initialSpeed;
            }
            else
            {
                Debug.LogWarning("Spawned object has no Rigidbody.  Initial speed will not be applied.");
            }
        }
        else
        {
            Debug.LogError("Object to instantiate is not assigned!");
        }
    }
}

