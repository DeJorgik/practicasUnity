using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static event Action OnPlayerdeath;
    public static event Action<GameConstants.gameResult> OnPlayerOutcome;


    public static void TriggerPlayerDeath() {
        if (OnPlayerdeath != null) {
            OnPlayerdeath.Invoke();
        }
    }
    
    public static void TriggerPlayerOutcome(GameConstants.gameResult result) {
        if (OnPlayerOutcome != null) {
            OnPlayerOutcome.Invoke(result);
        }
    }
}
