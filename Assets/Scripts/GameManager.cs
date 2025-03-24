using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityStandardAssets.Utility;
using Cursor = UnityEngine.Cursor;


public class GameManager : MonoBehaviour {
    // [SerializeField] private GameObject respawnZone;
    [SerializeField] private GameObject playerPrefab;

    private GameObject playerInstance;
    private playerScript player;
    private InitialPos respawn;

    private GameObject respawnZone;

    private bool isGuiOn;
    private int sceneIndex;
    

    public static GameManager Instance { get; private set; }


    private void Awake() {
        sceneIndex = 0;
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(playerPrefab); // Nos quedamos con el prefab del jugador, para el resto
        DontDestroyOnLoad(this);
    }


    // Start is called before the first frame update
    void Start() {
        isGuiOn = false;

        EventManager.OnPlayerOutcome += playerOutcome;
        EventManager.OnPlayerdeath += updateLife;
        SceneManager.sceneLoaded += OnLoadedScene;
        
        // Tenemos que buscar de primeras el spawn
        
        respawnZone = GameObject.FindGameObjectWithTag("Respawn");
        
        if (respawnZone != null) {
            respawn = respawnZone.GetComponent<InitialPos>();
            Debug.Log("El spawn es null");
        }

        if (respawn == null) {
            Debug.Log("El script de respawn es null");
        }
        
        if (playerInstance == null) {
            playerInstance = Instantiate(playerPrefab, respawn.getPosition(), Quaternion.identity);
            if (playerInstance != null) {
                player = playerInstance.GetComponent<playerScript>();
            }
        }

        GuiManager.Instance.hideGameOver();
    }


    private void loadNextScene() {
        sceneIndex++;
        SceneManager.LoadScene(sceneIndex);
    }


    private void OnLoadedScene(Scene scene , LoadSceneMode mode) {
        // Reseteamos cosas de la interfaz
        isGuiOn = false;
        GuiManager.Instance.hideGameOver();
        EventManager.resetOutcomeFlag();
        GuiManager.Instance.resetHud();

        if (respawnZone == null) {
            // Se encuentra
            respawnZone = GameObject.FindGameObjectWithTag("Respawn"); // Pillamos el spawn
            if (respawnZone != null) {
                respawn = respawnZone.GetComponent<InitialPos>();
            }
            Debug.Log("Se asigna uno nuevo");
        }
        
        if (playerInstance == null) {
            playerInstance = Instantiate(playerPrefab, respawn.getPosition(), Quaternion.identity);
            if (playerInstance != null) {
                player = playerInstance.GetComponent<playerScript>();
            }
        }
        
        respawnPlayer();
    }
    private void OnDisable() {
        EventManager.OnPlayerOutcome -= playerOutcome;
        EventManager.OnPlayerdeath -= updateLife;
    }

    // Update is called once per frame
    void Update() {

        
        
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        
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

        sceneIndex = 0;
        Time.timeScale = 1.0f;
        isGuiOn = false;
        SceneManager.LoadScene(sceneIndex);
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
        else {
             respawnPlayer();
        }
    }

    private void playerOutcome(GameConstants.gameResult result) {
        isGuiOn = true;
        
        Debug.Log("SE HA TRIGGEADO");
        EventManager.TriggerGamePause(isGuiOn);
        
        if (result == GameConstants.gameResult.Victory && sceneIndex < SceneManager.sceneCountInBuildSettings - 1) {
            // Aqui seguimos avanzando los niveles
            loadNextScene();
        }
        else{
            // Aqui o hemos agotado las vidas o bien hemos ganado
            GuiManager.Instance.popGameOver(result);
            Debug.Log("HA PERDIDO , AHORA SE MOSTRARA EL GAMEOVER");
        }
    }

    public void respawnPlayer() {
        if (player != null) {
            player.respawnAt(respawn.getPosition());
        }
    }

    public bool getGuiStatus() {
        return isGuiOn;
    }
}