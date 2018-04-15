using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Classe gérant la fin d'un niveau
 */ 

public class EndScript : MonoBehaviour {

    // L'index de la scene actuelle
    private int currentScene;
    // Le nombre de vies restantes au joueur
    private int nbLives;

    // On récupère les différents objets nécessaires
    private GameObject hud;
    GameManager gameManager;

    private LevelTimer levelTimer;
    private TotalTimer totalTimer;

    public Text levelTimerText;
    public Text totalTimerText;

    private AudioSource finishSound;

    // Lorsqu'une collision entre le drapeau de fin de niveau et le joueur se produit
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            // On lance le son de fin de niveau
            finishSound.Play();

            // On récupère le nombre de vies
            nbLives = gameManager.PlayerLives;

            // On enregistre le niveau suivant et le nombre de vies restantes
            PlayerPrefs.SetInt("CurrentLevel", currentScene + 1);
            PlayerPrefs.SetInt("NbLives", nbLives);

            // On met à jour les meilleurs scores
            updateScore();
            
            // On charge le prochain niveau après 2 secondes
            Invoke("LoadNextLevel", 2);
        }
    }
    
    // On initialise les différentes valeurs et objets qu'on va utiliser
    void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        hud = GameObject.Find("HUD");
        gameManager = hud.GetComponent<GameManager>();
        levelTimer = levelTimerText.GetComponent<LevelTimer>();
        totalTimer = totalTimerText.GetComponent<TotalTimer>();

        finishSound = GetComponent<AudioSource>();
        // On empêche le son de fin de niveau d'être joué à l'initialisation
        finishSound.playOnAwake = false;
    }
	
	void Update () {
        // On récupère le volume du son de fin de niveau
        finishSound.volume = PlayerPrefs.GetFloat("EffectVolume");
    }

    // Fonction permettant de mettre à jour le meilleur score si celui-ci est meilleur que le dernier
    void updateScore()
    {
        // Si aucun score n'est enregistré, on enregistre l'actuel
        if (PlayerPrefs.GetFloat("MinutesFloor" + currentScene) == 0 && PlayerPrefs.GetFloat("SecondsFloor" + currentScene) == 0 && PlayerPrefs.GetFloat("CentiemesFloor" + currentScene) == 0)
        {
            PlayerPrefs.SetFloat("MinutesFloor" + currentScene, levelTimer.minutes);
            PlayerPrefs.SetFloat("SecondsFloor" + currentScene, levelTimer.seconds);
            PlayerPrefs.SetFloat("CentiemesFloor" + currentScene, (float)levelTimer.centiemes);
        }
        // Sinon, on vérifie que le score actuel est meilleur que celui qui est déjà enregistré, puis on l'enregistre
        else if (levelTimer.minutes <= PlayerPrefs.GetFloat("MinutesFloor" + currentScene))
        {
            if (levelTimer.seconds == PlayerPrefs.GetFloat("SecondsFloor" + currentScene))
            {
                if ((float)levelTimer.centiemes <= PlayerPrefs.GetFloat("CentiemesFloor" + currentScene))
                {
                    PlayerPrefs.SetFloat("MinutesFloor" + currentScene, levelTimer.minutes);
                    PlayerPrefs.SetFloat("SecondsFloor" + currentScene, levelTimer.seconds);
                    PlayerPrefs.SetFloat("CentiemesFloor" + currentScene, (float)levelTimer.centiemes);
                }
            }
            else if (levelTimer.seconds < PlayerPrefs.GetFloat("SecondsFloor" + currentScene))
            {
                PlayerPrefs.SetFloat("MinutesFloor" + currentScene, levelTimer.minutes);
                PlayerPrefs.SetFloat("SecondsFloor" + currentScene, levelTimer.seconds);
                PlayerPrefs.SetFloat("CentiemesFloor" + currentScene, (float)levelTimer.centiemes);
            }
        }
        
        // On vérifie qu'il s'agit du dernier niveau
        if (currentScene == 2)
        {
            // Si oui, on vérifie si aucun temps total n'est enregistré, si oui on enregistre l'actuel
            if (PlayerPrefs.GetFloat("MinutesTotal") == 0 && PlayerPrefs.GetFloat("SecondsTotal") == 0 && PlayerPrefs.GetFloat("CentiemesTotal") == 0)
            {
                PlayerPrefs.SetFloat("MinutesTotal", totalTimer.minutes);
                PlayerPrefs.SetFloat("SecondsTotal", totalTimer.seconds);
                PlayerPrefs.SetFloat("CentiemesTotal", (float)totalTimer.centiemes);
            }
            // Si non, on vérifie que le temps total est meilleur que celui déjà enregistré et on l'enregistre
            else if (totalTimer.minutes <= PlayerPrefs.GetFloat("MinutesTotal"))
            {
                if (totalTimer.seconds == PlayerPrefs.GetFloat("SecondsTotal"))
                {
                    if ((float)totalTimer.centiemes <= PlayerPrefs.GetFloat("CentiemesTotal"))
                    {
                        PlayerPrefs.SetFloat("MinutesTotal", totalTimer.minutes);
                        PlayerPrefs.SetFloat("SecondsTotal", totalTimer.seconds);
                        PlayerPrefs.SetFloat("CentiemesTotal", (float)totalTimer.centiemes);
                    }
                }
                else if (totalTimer.seconds < PlayerPrefs.GetFloat("SecondsTotal"))
                {
                    PlayerPrefs.SetFloat("MinutesTotal", totalTimer.minutes);
                    PlayerPrefs.SetFloat("SecondsTotal", totalTimer.seconds);
                    PlayerPrefs.SetFloat("CentiemesTotal", (float)totalTimer.centiemes);
                }
            }
        }
    }

    // Fonction permettant de charger le niveau suivant
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(currentScene + 1);
    }
}
