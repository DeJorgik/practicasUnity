using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    private static bool isOutcomeTriggered = false; 
    public static event Action OnPlayerdeath;
    public static event Action<GameConstants.gameResult> OnPlayerOutcome;

    public static event Action<Vector3> OnPlayerRespawn;


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

    public static void TriggerPlayerRespawn(Vector3 position) {
        if (OnPlayerRespawn != null) {
            OnPlayerRespawn.Invoke(position);
        }
    }

    public static void resetOutcomeFlag() {
        isOutcomeTriggered = false;
    }
}
