using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour {

    
    [SerializeField] private GameObject prefabGameOver;
    [SerializeField] private GameObject prefabLifeHud;

    private GameObject gameOverObject;
    private GameObject lifeHudObject;
    
    private GameOverGui gameOverGui;
    private LifeHud lifeHud;

    public static GuiManager Instance { get; private set; }


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);


        if (prefabGameOver != null) {
            // Instanciamos un nuevo gameobject, este va a seguir hasta el final
            gameOverObject = Instantiate(prefabGameOver, Vector3.zero, Quaternion.identity);
            gameOverGui = gameOverObject.GetComponent<GameOverGui>();
        }

        if (prefabLifeHud != null) {
            lifeHudObject = Instantiate(prefabLifeHud, Vector3.zero, Quaternion.identity);
            lifeHud = lifeHudObject.GetComponent<LifeHud>();
        }
        
        


        if (gameOverObject != null) {
            DontDestroyOnLoad(gameOverObject);
        }

        if (lifeHudObject != null) {
            DontDestroyOnLoad(lifeHudObject);
        }
        
    }

    private void Start() {
        EventManager.loadScene += OnLoadedScene;
    }


    

    private void OnLoadedScene() {
        
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

    public void resetHud() {
        lifeHud.resetLifeHud();
    }
    
    
    
}
