using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerarObstaculos : MonoBehaviour {
    public GameObject objectToInstantiate; // The object to be spawned
    public float timeBetweenSpawns = 2f; // Time in seconds between spawns
    public Vector3 initialSpeed = Vector3.zero; // Initial speed of the spawned object
    public Vector3 initialRotation = Vector3.zero; // Initial rotation of the spawned object (Euler angles)
    public bool spawnOnStart = true; // Should it start spawning right away?

    protected float timer;
    protected AudioSource audioSource;

    private Coroutine spawnObjectCoroutine;

    void Start() {
        EventManager.OnGamePause += gameIsPaused;
        audioSource = GetComponent<AudioSource>();
        
        spawnObjectCoroutine = StartCoroutine(SpawnObject());
    }


    private void gameIsPaused(bool isPaused) {
        if (isPaused) {
            // Para pausarla
            if (spawnObjectCoroutine != null) {
                StopCoroutine(spawnObjectCoroutine);
                spawnObjectCoroutine = null;
            }
        }
        else {
            // Para activarla
            if (spawnObjectCoroutine == null) {
                spawnObjectCoroutine = StartCoroutine(SpawnObject());
            }
        }
    }
    
    // Corrutina para spawnear los troncos(simplemente lo he reuitilizado)
    IEnumerator SpawnObject() {
        while (true) {
            if (objectToInstantiate != null) 
            {
                
                Quaternion spawnRotation = Quaternion.Euler(initialRotation); 
                GameObject spawnedObject = Instantiate(objectToInstantiate, transform.position, spawnRotation);

                
                Rigidbody rb = spawnedObject.GetComponent<Rigidbody>(); 
                if (rb != null) {
                    rb.velocity = initialSpeed;
                }
                else {
                    Debug.LogWarning("No tiene rigibody");
                }
            }
            else {
                Debug.LogError("No hay objeto para instanciar");
            }

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}