using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FInalTarget : MonoBehaviour
{
    //Callback cuando entra un collider
    private void OnTriggerEnter(Collider other)
    {
        EventManager.TriggerPlayerOutcome(GameConstants.gameResult.Victory);
        // //ganar partida
        //Reproducir sonido
    }
}
