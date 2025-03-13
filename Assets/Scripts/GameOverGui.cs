using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGui : MonoBehaviour {
    private TMP_Text message;
    
    private void Start() {
        
    }

    public void activate() {
        gameObject.SetActive(true);
    }

    public void deactivate() {
        gameObject.SetActive(false);
    }

    public void winMessage() {
        message.SetText("¡Has ganado!");
    }
    
    public void defeatMessage() {
        message.SetText("¡Has perdido!");
    }
    
}
