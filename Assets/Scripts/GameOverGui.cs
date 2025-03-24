using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGui : MonoBehaviour {
    [SerializeField] private TMP_Text message;

    [SerializeField] private Button exitButton;
    [SerializeField] private Button retryButton;

    private void Start() {
        exitButton.onClick.AddListener(GameManager.Instance.Exit);
        retryButton.onClick.AddListener(GameManager.Instance.Retry);
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
