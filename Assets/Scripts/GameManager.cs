using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Utility;


public class GameManager : MonoBehaviour {
    [SerializeField] private InitialPos respawnZone;
    [SerializeField] private GameObject playerPrefab;

    private GameObject playerInstance;
    private playerScript player;
    private GameObject respawn;

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
        
        // Nos suscribimos a los eventos
        EventManager.OnPlayerOutcome += playerOutcome;
        EventManager.OnPlayerdeath += updateLife;
        EventManager.OnPlayerRespawn += respawnPlayer;
        
        // Escenas

        SceneManager.sceneLoaded += OnLoadedScene;


        if (playerInstance == null) {
            playerInstance = Instantiate(playerPrefab, new Vector3(10,10,10), Quaternion.identity);
        }

        if (playerInstance != null && player == null) {
            player = playerInstance.GetComponent<playerScript>();
        }

        respawnZone.respawn();
        GuiManager.Instance.hideGameOver();
    }

    private void loadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void OnLoadedScene(Scene scene , LoadSceneMode mode) {
        // Reseteamos cosas de la interfaz
        isGuiOn = false;
        GuiManager.Instance.hideGameOver();
        
        if (respawn == null) {
            respawn = GameObject.FindGameObjectWithTag("RespawnZone"); // Pillamos el spawn
        }
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

        respawnZone.respawn();
    }

    private void playerOutcome(GameConstants.gameResult result) {
        isGuiOn = true;
        GuiManager.Instance.popGameOver(result);

        if (result == GameConstants.gameResult.Victory) {
            loadNextScene();
        }
    }

    public void respawnPlayer(Vector3 spawnPosition) {
        if (player != null) {
            player.respawnAt(spawnPosition);
            
            Debug.Log("Se spawnea en el " + spawnPosition);
        }
    }
}