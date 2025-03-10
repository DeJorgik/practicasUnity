using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiempoVidaCilindro : MonoBehaviour
{
    public float timeToDestruct = 5f; // Time in seconds before the object is destroyed

    private float timer;

    void Start()
    {
        timer = timeToDestruct; // Initialize the timer
    }

    void Update()
    {
        timer -= Time.deltaTime; // Decrement the timer

        if (timer <= 0f)
        {
            Destroy(gameObject); // Destroy the GameObject this script is attached to
        }
    }
}
