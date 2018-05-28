using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Classe gérant l'ensemble du fonctionnement du jeu
 */

public class GameManager : MonoBehaviour
{

    // Variable indiquant si le joueur est mort
    private bool playerDead = false;

    // Nombre de tentatives du joueur
    public int PlayerTries;
    public int LevelPlayerTries;

    // Touche permettant de recommencer après être mort
    public KeyCode restart;

    // Index de la scene actuelle
    public int currentScene;

    // Les objets nécessaires
    private Timer timers;
    public GUISkin layout;
    GameObject Player;

    public GameObject finishFlag;
    private EndScript endScript;


    // Fonction passant le joueur à l'état "mort"
    public void PlayerDies()
    {
        playerDead = true;
    }

    // Fonction passant le joueur à l'état "vivant"
    public void PlayerReborn()
    {
        playerDead = false;
    }

    // Fonction diminuant le nombre de tentatives du joueur
    public void Tried()
    {
        PlayerTries++;
        LevelPlayerTries++;
    }

    // Fonction modifiant le nombre de tentatives du joueur
    public void SetTries(int tries)
    {
        PlayerTries = tries;
    }

    // Fonction gérant les affichages à l'écran
    private void OnGUI()
    {
        GUI.skin = layout;
        GUI.contentColor = Color.red;
        // On affiche le nombre de tentatives du joueur
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "Tentatives : " + PlayerTries);
        GUI.Label(new Rect(Screen.width / 2 - 240, 40, 170, 100), "Tentatives sur le niveau : " + LevelPlayerTries);

        // Si le joueur est mort
        if (playerDead && !endScript.endGame)
        {
            // On affiche un bouton "RETRY" qui, si il est cliqué :
            if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RETRY") || (Input.GetKey(restart)))
            {
                // On relance la partie
                Player.SendMessage("Restart", 0.5f, SendMessageOptions.RequireReceiver);
                // Le joueur gagne une "tentative"
                Tried();
            }
        }
        // Si le joueur a terminé tous les niveaux
        if (endScript.endGame)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 60, 60, 130, 53), "FELICITATIONS") || (Input.GetKey(restart)))
            {
                // On retourne au menu
                SceneManager.LoadScene(0);
            }
        }

        
    }

    void Start()
    {
        // On initialise les objets nécessaires
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Player = GameObject.FindGameObjectWithTag("Player");
        timers = Player.GetComponent<Timer>();
        endScript = finishFlag.GetComponent<EndScript>();

        // On récupère dans les préférences la touche permettant de recommencer
        restart = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("respawnKey", "Space"));

        // On initialise le nombre d'essais du joueur à 0 si il s'agit du premier niveau
        if (currentScene == 1)
        {
            PlayerTries = 0;
            PlayerPrefs.SetInt("NbTotalTries", PlayerTries);
        }
        else
            PlayerTries = PlayerPrefs.GetInt("NbTotalTries");

        // Et on initialise toujours le nombre d'essais du joueur pour le niveau actuel à 0
        LevelPlayerTries = 0;
    }
}