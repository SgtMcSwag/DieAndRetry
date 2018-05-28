using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Classe gérant le chronomètre total du jeu
 */

public class Timer : MonoBehaviour
{
    // Textes de timer
    public Text totalTimer;
    public Text levelTimer;

    // Variable indiquant si le chrono est lancé
    public bool timerOn = false;

    // Index de la scene actuelle
    private int currentScene;

    // Variables stockant la valeur du chrono
    public float totalTemps;
    private float levelTemps;

    public float tMinutes, tSeconds;
    public double tCentiemes;

    public float lMinutes, lSeconds;
    public double lCentiemes;

    // On récupère les objets nécessaires
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != 1)
            totalTemps = PlayerPrefs.GetFloat("TempsTotalActuel");
    }

    void OnGUI()
    {
        // On ne récupère que les centiemes
        tCentiemes = (totalTemps * 100) % 100;
        lCentiemes = (levelTemps * 100) % 100;

        // On récupère les minutes
        tMinutes = (int)((totalTemps) / 60);
        lMinutes = (int)((levelTemps) / 60);
        // Puis les secondes
        tSeconds = (int)((totalTemps) % 60);
        lSeconds = (int)((levelTemps) % 60);

        // Enfin, on affiche la valeur du chrono sur l'écran de jeu
        totalTimer.text = "Total time : " + tMinutes.ToString("00") + ":" + tSeconds.ToString("00") + ":" + tCentiemes.ToString("00");
        levelTimer.text = "Level time : " + lMinutes.ToString("00") + ":" + lSeconds.ToString("00") + ":" + lCentiemes.ToString("00");
    }

    private void Update()
    {
        if (timerOn)
        {
            totalTemps += Time.deltaTime;
            levelTemps += Time.deltaTime;
        }
    }

    // Fonction permettant de reinitialiser le chrono
    public void ResetTotalTimer(float temp)
    {
        totalTemps = 0.0f;
    }

    public void ResetLevelTimer(float temp)
    {
        levelTemps = 0.0f;
    }
}
