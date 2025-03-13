using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour {

    public static GuiManager Instance { get; private set; }


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Lo mantiene entre escenas
    }


    [SerializeField] private GameOverGui gameOverGui;
    [SerializeField] private LifeHud lifeHud;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    
    
    
}
