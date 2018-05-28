using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Classe gérant la fin d'un niveau
 */ 

public class EndScript : MonoBehaviour {

    // Variable indiquant si le jeu est terminé
    public bool endGame = false;

    // L'index de la scene actuelle
    private int currentScene;

    // Le nombre de tentatives du joueur
    private int nbTries;

    // Le nombre de tentatives du joueur sur le niveau actuel
    private int levelNbTries;

    // On récupère les différents objets nécessaires
    private GameObject hud;
    GameManager gameManager;

    public GameObject player;
    private Rigidbody2D rb2d;

    private Timer timers;

    private AudioSource finishSound;

    // Lorsqu'une collision entre le drapeau de fin de niveau et le joueur se produit
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            // On freeze le jeu au contact du drapeau
            FreezeGame();

            // On lance le son de fin de niveau
            finishSound.Play();

            // On récupère le nombre de tentatives
            nbTries = gameManager.PlayerTries;
            levelNbTries = gameManager.LevelPlayerTries;

            // On met à jour les meilleurs scores
            UpdateScore();
            
            // On charge le prochain niveau après 2 secondes
            Invoke("LoadNextLevel", 2);
        }
    }
    
    // On initialise les différentes valeurs et objets qu'on va utiliser
    void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        hud = GameObject.Find("HUD");
        gameManager = hud.GetComponent<GameManager>();
        timers = player.GetComponent<Timer>();
        rb2d = player.GetComponent<Rigidbody2D>();

        finishSound = GetComponent<AudioSource>();

        // On empêche le son de fin de niveau d'être joué à l'initialisation
        finishSound.playOnAwake = false;
    }
	
	void Update () {
        // On récupère le volume du son de fin de niveau
        finishSound.volume = PlayerPrefs.GetFloat("EffectVolume");
    }

    // Fonction permettant de mettre à jour le meilleur score si celui-ci est meilleur que le dernier
    void UpdateScore()
    {
        // On enregistre le temps total actuel afin de continuer de l'augmenter au prochain niveau
        PlayerPrefs.SetFloat("TempsTotalActuel", timers.totalTemps);

        // On enregistre aussi le nombre d'essais du joueur avec de le récupérer au prochain niveau
        PlayerPrefs.SetInt("NbTotalTries", nbTries);

        // Si aucun score n'est enregistré, on enregistre l'actuel
        if (PlayerPrefs.GetFloat("MinutesFloor" + currentScene) == 0 && PlayerPrefs.GetFloat("SecondsFloor" + currentScene) == 0 && PlayerPrefs.GetFloat("CentiemesFloor" + currentScene) == 0)
        {
            PlayerPrefs.SetFloat("MinutesFloor" + currentScene, timers.lMinutes);
            PlayerPrefs.SetFloat("SecondsFloor" + currentScene, timers.lSeconds);
            PlayerPrefs.SetFloat("CentiemesFloor" + currentScene, (float)timers.lCentiemes);

            // Et le nombre de tentatives du niveau actuel
            PlayerPrefs.SetInt("NbTriesFloor" + currentScene, levelNbTries);
        }
        // Sinon, on vérifie que le score actuel est meilleur que celui qui est déjà enregistré, puis on l'enregistre
        else if (timers.lMinutes <= PlayerPrefs.GetFloat("MinutesFloor" + currentScene))
        {
            if (timers.lSeconds == PlayerPrefs.GetFloat("SecondsFloor" + currentScene))
            {
                if ((float)timers.lCentiemes <= PlayerPrefs.GetFloat("CentiemesFloor" + currentScene))
                {
                    PlayerPrefs.SetFloat("MinutesFloor" + currentScene, timers.lMinutes);
                    PlayerPrefs.SetFloat("SecondsFloor" + currentScene, timers.lSeconds);
                    PlayerPrefs.SetFloat("CentiemesFloor" + currentScene, (float)timers.lCentiemes);

                    // Et le nombre de tentatives du niveau actuel
                    PlayerPrefs.SetInt("NbTriesFloor" + currentScene, levelNbTries);
                }
            }
            else if (timers.lSeconds < PlayerPrefs.GetFloat("SecondsFloor" + currentScene))
            {
                PlayerPrefs.SetFloat("MinutesFloor" + currentScene, timers.lMinutes);
                PlayerPrefs.SetFloat("SecondsFloor" + currentScene, timers.lSeconds);
                PlayerPrefs.SetFloat("CentiemesFloor" + currentScene, (float)timers.lCentiemes);

                // Et le nombre de tentatives du niveau actuel
                PlayerPrefs.SetInt("NbTriesFloor" + currentScene, levelNbTries);
            }
        }
        
        // On vérifie qu'il s'agit du dernier niveau
        if (currentScene == 4)
        {
            // Si oui, on vérifie si aucun temps total n'est enregistré, si oui on enregistre l'actuel
            if (PlayerPrefs.GetFloat("MinutesTotal") == 0 && PlayerPrefs.GetFloat("SecondsTotal") == 0 && PlayerPrefs.GetFloat("CentiemesTotal") == 0)
            {
                PlayerPrefs.SetFloat("MinutesTotal", timers.tMinutes);
                PlayerPrefs.SetFloat("SecondsTotal", timers.tSeconds);
                PlayerPrefs.SetFloat("CentiemesTotal", (float)timers.tCentiemes);

                // On enregistre le nombre de tentatives totales
                PlayerPrefs.SetInt("NbTotalTries", nbTries);
            }
            // Si non, on vérifie que le temps total est meilleur que celui déjà enregistré et on l'enregistre
            else if (timers.tMinutes <= PlayerPrefs.GetFloat("MinutesTotal"))
            {
                if (timers.tSeconds == PlayerPrefs.GetFloat("SecondsTotal"))
                {
                    if ((float)timers.tCentiemes <= PlayerPrefs.GetFloat("CentiemesTotal"))
                    {
                        PlayerPrefs.SetFloat("MinutesTotal", timers.tMinutes);
                        PlayerPrefs.SetFloat("SecondsTotal", timers.tSeconds);
                        PlayerPrefs.SetFloat("CentiemesTotal", (float)timers.tCentiemes);

                        // On enregistre le nombre de tentatives totales
                        PlayerPrefs.SetInt("NbTotalTries", nbTries);
                    }
                }
                else if (timers.tSeconds < PlayerPrefs.GetFloat("SecondsTotal"))
                {
                    PlayerPrefs.SetFloat("MinutesTotal", timers.tMinutes);
                    PlayerPrefs.SetFloat("SecondsTotal", timers.tSeconds);
                    PlayerPrefs.SetFloat("CentiemesTotal", (float)timers.tCentiemes);

                    // On enregistre le nombre de tentatives totales
                    PlayerPrefs.SetInt("NbTotalTries", nbTries);
                }
            }
        }
    }

    // Fonction permettant de charger le niveau suivant
    private void LoadNextLevel()
    {
        // Si il s'agit du dernier niveau, on load le menu
        if (currentScene == 4)
        {
            // On affiche un bouton de fin à partir du GameManager
            endGame = true;
        }
        else
            SceneManager.LoadScene(currentScene + 1);
    }

    // Fonction qui bloque les mouvements du personnage et arrête les timers
    private void FreezeGame ()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        
        timers.timerOn = false;
    }
}


