using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPos : MonoBehaviour {
    private Vector3 spawnPosition;
    private AudioSource AudioSource;
    void Start() {
        spawnPosition = transform.position + new Vector3(0,2,0);
        AudioSource = GetComponent<AudioSource>();
    }

    public void respawn() {
        EventManager.TriggerPlayerRespawn(spawnPosition);
        if (AudioSource != null)
        {
            AudioSource.Play();
        }
    }
}
