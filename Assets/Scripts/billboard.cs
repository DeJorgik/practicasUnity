using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    private SpriteRenderer sprite;
    private int cameraQuadrant;
    void Start()
    {
       sprite = GetComponent<SpriteRenderer>();
    }
    
    void LateUpdate()
    {
        //Pillar la cámara que se esta utiliando actualmente y mirar en esa dirección
        Vector3 cameraPos = Camera.main.transform.position;
        transform.LookAt(cameraPos);

        //Según el cuadrante de la camara le damos la vuelta o no
        cameraQuadrant = GetQuadrant(cameraPos);
        if (cameraQuadrant == 3 || cameraQuadrant == 4) { 
            //En este caso lo está mirando de frente
            sprite.flipX = true;
        } else
        {
            //En este caso lo está mirando por detras
            sprite.flipX = false;
        }
    }

    //Función que determina en qué cuadrante está un punto en relacción al objeto
    //Se utiliza para girar el sprite acorde con la posicion de la cámara
    public int GetQuadrant(Vector3 point)
    {
        // Create a 2D vector using the x and z components.
        Vector2 direction = new Vector2(point.x - transform.position.x, point.z - transform.position.z);

        // Calculate the angle in degrees.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle to be in the range of 0 to 360 degrees.
        if (angle < 0)
        {
            angle += 360;
        }

        // Determine the quadrant.
        if (angle >= 0 && angle < 90)
        {
            return 1; // Quadrant 1
        }
        else if (angle >= 90 && angle < 180)
        {
            return 2; // Quadrant 2
        }
        else if (angle >= 180 && angle < 270)
        {
            return 3; // Quadrant 3
        }
        else
        {
            return 4; // Quadrant 4
        }
    }
}
