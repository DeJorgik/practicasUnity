using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Utility;


public class GameManager : MonoBehaviour {
    
    public KillPlane killZone;
    public InitialPos respawnZone;

    
    private bool isGuiOn;
    
    // Start is called before the first frame update
    void Start() {
        isGuiOn = false;
        
        // Nos suscribimos a los eventos
        EventManager.OnPlayerOutcome += playerOutcome;
        EventManager.OnPlayerdeath += updateLife;
        
        GuiManager.Instance.hideGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGuiOn) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0.0f;
        }
        else {
            
            Time.timeScale = 1.0f;
        }
    }


    public void Retry() {
        Time.timeScale = 1.0f;
        isGuiOn = false;
        SceneManager.LoadScene("Principal");
    }

    public void Exit() {
        Application.Quit();
    }


    private void updateLife() {

        if (GuiManager.Instance.updateLife()) {
            // Ha perdido todas las vidas
            EventManager.TriggerPlayerOutcome(GameConstants.gameResult.Defeat);
        }
        
        respawnZone.respawn();
        

        // if (hud.decreaseLifeCount()) {
        //     playerOutcome(GameConstants.gameResult.Defeat);
        // }
        // else {
        //     respawnZone.respawn();
        //     player.stopMovement();
        // }
    }

    private void playerOutcome(GameConstants.gameResult result) {
        GuiManager.Instance.popGameOver(result);
    }
}
