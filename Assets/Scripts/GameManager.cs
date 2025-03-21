using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Utility;


public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject respawnZone;
    [SerializeField] private GameObject playerPrefab;

    private GameObject playerInstance;
    private playerScript player;
    private InitialPos respawn;

    private bool isGuiOn;
    

    public static GameManager Instance { get; private set; }


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(playerPrefab); // Nos quedamos con el prefab del jugador, para el resto
    }


    // Start is called before the first frame update
    void Start() {
        isGuiOn = false;

        EventManager.OnPlayerOutcome += playerOutcome;
        EventManager.OnPlayerdeath += updateLife;
        EventManager.OnPlayerRespawn += respawnPlayer;

        SceneManager.sceneLoaded += OnLoadedScene;

        
        
        if (respawnZone != null) {
            respawn = respawnZone.GetComponent<InitialPos>();
            Debug.Log("El spawn es null");
        }

        if (respawn == null) {
            Debug.Log("El script de respawn es null");
        }
        
        if (playerInstance == null) {
            playerInstance = Instantiate(playerPrefab, respawn.getPos(), Quaternion.identity);
            if (playerInstance != null) {
                player = playerInstance.GetComponent<playerScript>();
            }
        }

        GuiManager.Instance.hideGameOver();
    }


    private void loadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void OnLoadedScene(Scene scene , LoadSceneMode mode) {
        // Reseteamos cosas de la interfaz
        isGuiOn = false;
        GuiManager.Instance.hideGameOver();
        
        if (respawnZone == null) {
            respawnZone = GameObject.FindGameObjectWithTag("RespawnZone"); // Pillamos el spawn
            if (respawnZone != null) {
                respawn = respawnZone.GetComponent<InitialPos>();
            }
        }
        
        
        respawnPlayer();
    }
    private void OnDisable() {
        EventManager.OnPlayerOutcome -= playerOutcome;
        EventManager.OnPlayerdeath -= updateLife;
        EventManager.OnPlayerRespawn -= respawnPlayer;
    }

    // Update is called once per frame
    void Update() {
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
        EventManager.resetOutcomeFlag();

        Time.timeScale = 1.0f;
        isGuiOn = false;
        GuiManager.Instance.hideGameOver();
        // GuiManager.Instance.resetHud();
        SceneManager.LoadScene("Nivel_1");
    }

    public void Exit() {
        Application.Quit();
    }


    private void updateLife() {
        if (GuiManager.Instance.updateLife()) {
            // Ha perdido todas las vidas
            EventManager.TriggerPlayerOutcome(GameConstants.gameResult.Defeat);
            Debug.Log("HA PERDIDO");
        }

        respawnPlayer();
    }

    private void playerOutcome(GameConstants.gameResult result) {
        isGuiOn = true;
        GuiManager.Instance.popGameOver(result);

        if (result == GameConstants.gameResult.Victory) {
            loadNextScene();
        }
    }

    public void respawnPlayer() {
        if (player != null) {
            player.respawnAt(respawn.getPos());
        }
        
    }
}