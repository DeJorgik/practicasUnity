using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openBridge : MonoBehaviour
{
    public enum EjeRotacion{
        x,
        y,
        z
    }
    
    
    public float angle;
    float max_angle;
    float min_angle;
    public float speed;
    public bool direction;
    public EjeRotacion ejeSeleccionado;
    private Vector3 ejeRotacion;

    private Quaternion estadoInicial;
    private Quaternion estadoActual;
    private Quaternion estadoFinal;
    

    // Start is called before the first frame update
    void Start() {
        ejeRotacion = Vector3.zero;
        
        switch (ejeSeleccionado) {
            case EjeRotacion.x:
                ejeRotacion = Vector3.right;
                break;
            case EjeRotacion.y:
                ejeRotacion = Vector3.up;
                break;
            case EjeRotacion.z:
                ejeRotacion = Vector3.forward;
                break;
        }


        estadoFinal = Quaternion.AngleAxis(angle, ejeRotacion);
        estadoInicial = transform.rotation;
        transform.rotation = estadoInicial;
    }
    


    // Update is called once per frame
    void Update() {

        if (direction)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, estadoFinal, speed * Time.deltaTime);
        else
            transform.rotation = Quaternion.RotateTowards(transform.rotation, estadoInicial, speed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, estadoFinal) < 0.1f || Quaternion.Angle(transform.rotation, estadoInicial) < 0.1f)
            direction = !direction;
        
        Debug.Log("Direccion es" + direction);
        
    }
}