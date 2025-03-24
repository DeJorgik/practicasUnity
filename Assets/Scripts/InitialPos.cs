using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPos : MonoBehaviour {
    private Vector3 spawnPosition;
    private AudioSource AudioSource;
    void Awake() {
        spawnPosition = transform.position + new Vector3(0,2,0);
    }

    private void Start() {
        AudioSource = GetComponent<AudioSource>();
    }


    private void Update() {
        // if (AudioSource == null) {
        //     AudioSource = GetComponent<AudioSource>();
        //     Debug.Log("Se ha pillado");
        // }
    }


    public Vector3 getPosition() {
        Debug.Log("Spawnea en " + spawnPosition);
        if (AudioSource != null) {
            AudioSource.Play();
            Debug.Log("Se esta reproduciendo");
        }
        else {
            Debug.Log("SI ES NULL");
        }
        return spawnPosition;
    }
    
    
}
