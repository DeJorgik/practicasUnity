using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPos : MonoBehaviour
{
    public GameObject playerObjec;

    // Start is called before the first frame update
    void Start()
    {
        playerObjec.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
