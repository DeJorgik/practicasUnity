using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPos : MonoBehaviour {
    private Vector3 spawnPosition;
    
    void Awake() {
        spawnPosition = transform.position + new Vector3(0,2,0);
    }

    public void respawn() {
        EventManager.TriggerPlayerRespawn(spawnPosition);
        Debug.Log("Se ha spawneado en el " + spawnPosition);
    }
}
