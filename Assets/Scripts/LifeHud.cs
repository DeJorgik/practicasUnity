using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LifeHud : MonoBehaviour {
    [SerializeField] private GameObject lifePrefab; // Lo que vamos a meter
    [SerializeField] private Transform panel;

    [SerializeField] private int lifeNumber;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float spacing;

    private int currentLifeNumber;
    private List<GameObject> lifeList;
    
    private int counter = 0;


    private void Start() {
        lifeList = new List<GameObject>();
        resetLifeHud();
        counter = 0;
        Debug.Log("VIDAS ACTUALES -> " + currentLifeNumber);
        Debug.Log("TAM LIST -> " + lifeList.Count);
    }

    public void resetLifeHud() {
        currentLifeNumber = lifeNumber;
        clearLifes();
        renderLife();
    }

    public bool decreaseLifeCounter() {
        
        bool result = false;
        int index = lifeList.Count - 1;
        
        if (index >= 0) {
            // Mientras tengamos mas de 0 vidas
            
            Destroy(lifeList[index]);
            lifeList.RemoveAt(index);
            currentLifeNumber--;
        }
        else {
            result = true;
        }

        Debug.Log("INdice -> " + index);
        Debug.Log("Veces ---> " + counter++);
        
        Debug.Log("Se llama desde " + gameObject.name);
        
        return result;
    }

    private void renderLife() {
        for (int i = 0; i < currentLifeNumber; i++) {
            Vector3 position = panel.position + new Vector3(spacing * i , 0 , 0);
            lifeList.Add(Instantiate(lifePrefab, position, Quaternion.identity, panel));
        }
    }

    private void clearLifes() {
        for (int i = 0; i < lifeList.Count; i++) {
            Destroy(lifeList[i]);
        }
        lifeList.Clear();
    }
}
