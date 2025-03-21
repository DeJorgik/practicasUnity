using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPos : MonoBehaviour {
    private Vector3 spawnPosition;
    
    void Awake() {
        spawnPosition = transform.position + new Vector3(0,2,0);
    }

    public Vector3 getPos() {
        return spawnPosition;
    }

    public void respawn() {
        EventManager.TriggerPlayerRespawn();
        Debug.Log("Se ha spawneado en el " + spawnPosition);
    }
}
