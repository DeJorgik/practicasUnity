using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float speed;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, Time.deltaTime*speed);
        }
    }
}
