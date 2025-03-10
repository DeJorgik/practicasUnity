using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telescopy : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject telescopyCamera;


    // Start is called before the first frame update
    void Start()
    {
        telescopyCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            mainCamera.SetActive(false);
            telescopyCamera.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            mainCamera.SetActive(true);
            telescopyCamera.SetActive(false);
        }
    }
}
