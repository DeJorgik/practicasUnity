using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class ShotgunScript : MonoBehaviour {
    [SerializeField] private ParticleSystem bulletTracerPrefab;
    private int layerMask;

    private Vector3 originPoint;
    
    private Vector3 leftPoint;
    private Vector3 rightPoint;
    private Vector3 upperPoint;
    private Vector3 lowerPoint;

    private List<Vector3> hitPoints;

    private float maxDistance;
    private float spread;

    private void Start() {

        layerMask = LayerMask.GetMask("Enemigo");
        
        hitPoints = new List<Vector3>();
        maxDistance = 5f;
        spread = 0.5f;
        int distance = 3;
        
        originPoint = new Vector3(0, 0, distance);
        
        hitPoints.Add(originPoint);
        hitPoints.Add(new Vector3(-spread, 0, distance));  // Izquierda
        hitPoints.Add(new Vector3(spread, 0, distance));   // Derecha
        hitPoints.Add(new Vector3(0, spread, distance));   // Arriba
        hitPoints.Add(new Vector3(0, -spread, distance));  // Abajo

        
        hitPoints.Add(new Vector3(-spread * 2, spread, distance));    // Arriba izquierda
        hitPoints.Add(new Vector3(spread * 2, spread, distance));     // Arriba derecha
        hitPoints.Add(new Vector3(-spread * 2, -spread, distance));   // Abajo izquierda
        hitPoints.Add(new Vector3(spread * 2, -spread, distance));    // Abajo derecha
        hitPoints.Add(new Vector3(-spread, spread * 2, distance));    // Arriba izquierda alto
        hitPoints.Add(new Vector3(spread, spread * 2, distance));     // Arriba derecha alto
        hitPoints.Add(new Vector3(-spread, -spread * 2, distance));   // Abajo izquierda bajo
        hitPoints.Add(new Vector3(spread, -spread * 2, distance));    // Abajo derecho bajo

    }

    // Update is called once per frame
    void Update() {
        Quaternion rotation = transform.rotation;

        List<Vector3> rotatedPoints = new List<Vector3>();
        for (int i = 0; i < hitPoints.Count; i++) {
            rotatedPoints.Add(rotation * hitPoints[i]);
        }
        
        Vector3 playerTorso = transform.position + new Vector3(0, 1, 0);


        for (int i = 0; i < hitPoints.Count; i++) {
            Debug.DrawRay(playerTorso , rotatedPoints[i] , Color.cyan);
        }
        
        if (Input.GetMouseButtonDown(0)) {
            
            for (int i = 0; i < rotatedPoints.Count; i++) {
                Ray bulletRay = new Ray(playerTorso, rotatedPoints[i]);
                spawnBulletTracer(bulletRay);
                RaycastHit hit;
                if (Physics.Raycast(bulletRay, out hit, maxDistance, layerMask)) {
                    Destroy(hit.collider.gameObject);
                }
                
            }
        }
    }
    
    private void spawnBulletTracer(Ray ray) {
        ParticleSystem tracer = Instantiate(bulletTracerPrefab, ray.origin, transform.rotation);
        tracer.transform.LookAt(ray.origin + ray.direction);
        Destroy(tracer,tracer.main.duration);
    }
}