using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public float distance;
    float max_distance;
    float min_distance;
    public float speed;
    public bool direction;


    [SerializeField] private ParticleSystem particles;
    // Start is called before the first frame update
    
    void Start() {
        if (particles == null) {
            Debug.Log("Problemon");
        }
        
        max_distance = max_distance + distance;
        min_distance = min_distance - distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(max_distance, transform.position.y, transform.position.z), speed*Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(min_distance, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }

        if (transform.position.x >= max_distance || transform.position.x <= min_distance)
        {
            direction = !direction;

            if (particles != null) {
                Debug.Log("Estamos haciendo cosas");
                ParticleSystem.ShapeModule shape = particles.shape;
                shape.rotation = new Vector3(0, direction ? 270 : 90, 0);
            }
        }
    }
}
