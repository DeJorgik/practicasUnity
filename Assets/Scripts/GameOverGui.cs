using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGui : MonoBehaviour {
    private TMP_Text message;

    private Button retryButton;
    private Button exitButton;

    private GameObject panel;

    private void Awake() {
        panel = transform.Find("Panel").gameObject;

        if (panel != null) {
            retryButton = panel.transform.Find("BotonReintentar").gameObject.GetComponent<Button>();
            exitButton = panel.transform.Find("BotonSalir").gameObject.GetComponent<Button>();
            
            message = panel.transform.Find("textoResultado").GetComponent<TMP_Text>();
        }



        if (retryButton != null) {
            // Le metemos el listener
            retryButton.onClick.AddListener( (() => GameManager.Instance.Retry()));
        }

        if (exitButton != null) {
            exitButton.onClick.AddListener((() => GameManager.Instance.Exit()));
        }
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

    private void Update() {
        if (gameObject.activeSelf) {
            Debug.Log("Estamos activos");
        }
        else {
            Debug.Log("No estamos activos");
        }
    }
}
