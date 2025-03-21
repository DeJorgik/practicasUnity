using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    private static bool isOutcomeTriggered = false; 
    public static event Action OnPlayerdeath;
    public static event Action<GameConstants.gameResult> OnPlayerOutcome;

    public static event Action loadScene; 

    public static event Action OnPlayerRespawn;


    public void TriggerLoadScene()
    {
        if (loadScene != null) {
            loadScene.Invoke();
        }
    }

    public static void TriggerPlayerDeath() {
        if (OnPlayerdeath != null) {
            OnPlayerdeath.Invoke();
        }
    }
    
    
    public static void TriggerPlayerOutcome(GameConstants.gameResult result) {
        if (isOutcomeTriggered) return; // Evitamo sque se repita

        isOutcomeTriggered = true;
        if (OnPlayerOutcome != null) {
            OnPlayerOutcome.Invoke(result);
        }
    }

    public static void TriggerPlayerRespawn() {
        if (OnPlayerRespawn != null) {
            OnPlayerRespawn.Invoke();
        }
    }

    public static void resetOutcomeFlag() {
        isOutcomeTriggered = false;
    }
}
