using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolin : MonoBehaviour
{
    public GameObject player;
    public float force;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 dir_lanzamiento = transform.position;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0,1,5)*force);
        }
    }
}
