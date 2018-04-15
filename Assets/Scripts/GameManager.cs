using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Classe gérant l'ensemble du fonctionnement du jeu
 */

public class GameManager : MonoBehaviour {

    // Variable indiquant si le joueur est mort
    private bool playerDead = false;

    // Nombre de vies du joueur
    public int PlayerLives = 10;
    // Touche permettant de recommencer après être mort
    public KeyCode restart;

    // Index de la scene actuelle
    private int currentScene;

    // Les objets nécessaires
    public Text totalTimerText;
    private TotalTimer totalTimer;
    public GUISkin layout;
    GameObject Player;

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

    // Fonction diminuant le nombre de vies du joueur
    public void LoseLife ()
    {
        PlayerLives--;
    }

    // Fonction modifiant le nombre de vies du joueur
    public void SetLives (int lives)
    {
        PlayerLives = lives;
    }

    // Fonction gérant les affichages à l'écran
    private void OnGUI()
    {
        GUI.skin = layout;
        // On affiche le nombre de vies restantes du joueur
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "Lives : " + PlayerLives);
        
        // Si le joueur est mort
        if (playerDead)
        {
            // Et qu'il n'a plus de vies restantes
            if (PlayerLives == 0)
            {
                // On affiche un bouton "RESTART" qui, si il est cliqué : 
                if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
                {
                    // Si il ne s'agit pas du premier niveau, on repart sur celui-ci
                    if (currentScene != 1)
                    {
                        SceneManager.LoadScene(1);
                    }
                    // On réinitialise la partie
                    Player.SendMessage("ReinitGame", 0.5f, SendMessageOptions.RequireReceiver);
                    // Et on réinitialise le chrono
                    totalTimer.ResetTimer();
                }
            }
            // Si le joueur à encore des vies restantes
            else
            {
                // On affiche un bouton "RETRY" qui, si il est cliqué :
                if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RETRY") || (Input.GetKey(restart)))
                {
                    // On relance la partie
                    Player.SendMessage("Restart", 0.5f, SendMessageOptions.RequireReceiver);
                    // Le joueur perd une vie
                    LoseLife();
                }
            }
        }
    }
    
    void Start () {
        // On initialise les objets nécessaires
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Player = GameObject.FindGameObjectWithTag("Player");
        totalTimer = totalTimerText.GetComponent<TotalTimer>();

        // On récupère dans les préférences la touche permettant de recommencer
        restart = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("respawnKey", "Space"));
    }
}
