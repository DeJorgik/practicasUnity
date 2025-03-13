using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHud : MonoBehaviour {
    [SerializeField] private GameObject lifePrefab; // Lo que vamos a meter
    [SerializeField] private Transform panel;

    [SerializeField] private int lifeNumber;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float spacing;

    private List<GameObject> lifeList;


    private void Start() {
        renderLife();
    }

    public bool decreaseLifeCounter() {
        
        bool result = false;
        
        lifeNumber--;
        if (lifeNumber == 0) { // Quiere decir que ha muerto
            result = true;
        }
        renderLife();
        return result;
    }

    private void renderLife() {
        for (int i = 0; i < lifeNumber; i++) {
            Vector3 position = panel.position + new Vector3(spacing * i , 0 , 0);
            lifeList.Add(Instantiate(lifePrefab, position, Quaternion.identity, panel));
        }
    }
}
