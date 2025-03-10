using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FInalTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Callback cuando entra un collider
    private void OnTriggerEnter(Collider other)
    {
        //ganar partida
        SceneManager.LoadScene(0);
    }
}
