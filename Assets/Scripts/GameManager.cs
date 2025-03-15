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

    private bool isGuiOn;

    public static GameManager Instance { get; private set; }


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    // Start is called before the first frame update
    void Start() {
        isGuiOn = false;

        // Nos suscribimos a los eventos
        EventManager.OnPlayerOutcome += playerOutcome;
        EventManager.OnPlayerdeath += updateLife;
        EventManager.OnPlayerRespawn += respawnPlayer;


        if (playerInstance == null) {
            playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        if (playerInstance != null && player == null) {
            player = playerInstance.GetComponent<playerScript>();
        }

        respawnZone.respawn();
        GuiManager.Instance.hideGameOver();
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
    }

    public void respawnPlayer(Vector3 spawnPosition) {
        if (player != null) {
            player.respawnAt(spawnPosition);
        }
    }
}