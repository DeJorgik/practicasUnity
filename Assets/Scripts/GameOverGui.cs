using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGui : MonoBehaviour {
    [SerializeField] private TMP_Text message;
    
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
