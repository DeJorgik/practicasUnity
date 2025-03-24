using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour {

    public static GuiManager Instance { get; private set; }


    [SerializeField] private GameObject gameOverPrefab;
    [SerializeField] private GameObject lifePrefab;
    
    private GameObject gameOverObject;
    private GameObject lifeHudObject;
    
    private GameOverGui gameOverGui;
    private LifeHud lifeHud;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gameOverObject = Instantiate(gameOverPrefab, Vector3.zero, Quaternion.identity);
        lifeHudObject = Instantiate(lifePrefab, Vector3.zero, Quaternion.identity);


        gameOverGui = gameOverObject.GetComponent<GameOverGui>();
        lifeHud = lifeHudObject.GetComponent<LifeHud>();
        
        DontDestroyOnLoad(gameOverObject);
        DontDestroyOnLoad(lifeHudObject);
        
        DontDestroyOnLoad(this);
    }


    // Los conseguimos una vez y permanecen para el resto de la ejecucion
    
    

    public void hideGameOver() {
        gameOverGui.deactivate();
    }
    
    public void popGameOver(GameConstants.gameResult result) {
        if (result == GameConstants.gameResult.Victory) {
            gameOverGui.winMessage();
        } else if (result == GameConstants.gameResult.Defeat) {
            gameOverGui.defeatMessage();
        }
        
        gameOverGui.activate();
    }

    public bool updateLife() {
        return lifeHud.decreaseLifeCounter();
    }

    public void resetHud() {
        lifeHud.resetLifeHud();
    }
    
    
    
}
