using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Centinela : GenerarObstaculos
{
    public int projectileSpeed;
    private bool playerVisible;
    private GameObject player;
    public GameObject spawnPos;

    void Start()
    {
        playerVisible = false;
        timer = timeBetweenSpawns;

    }

    void Update()
    {
        if (playerVisible)
        {

            transform.LookAt(player.transform.position);
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                SpawnObject();
                timer = timeBetweenSpawns;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerVisible = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerVisible = false;
            player = null;
        }
    }

    //Sobrecarga del método
    new void SpawnObject()
    {
        if (objectToInstantiate != null)
        {

            GameObject spawnedObject = Instantiate(objectToInstantiate);
            //apuntar hacia el jugador y mover
            Vector3 direction = transform.forward;
            direction.Normalize();
            spawnedObject.transform.position = spawnPos.transform.position;
            spawnedObject.transform.LookAt(player.transform.position);
            
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direction*projectileSpeed);
            }
        }
    }
}

