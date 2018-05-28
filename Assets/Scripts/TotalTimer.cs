﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Classe gérant le chronomètre total du jeu
 */

public class TotalTimer : MonoBehaviour {

    // Variable indiquant si le chrono est lancé
    public bool timerOn = false;

    // Texte affichant le timer
    public Text totalTimer;

    // Variable permettant de reinitialiser le timer
    private float tempTime = 0f;
    // Variable stockant la valeur du chrono
    private float timer;

    public float minutes, seconds;
    public double centiemes;

    // On récupère les objets nécessaires
    void Start () {
        totalTimer = GetComponent<Text>() as Text;
	}
	
	void Update () {
        
        // Si le chrono est lancé
        if (timerOn)
        {
            // On adapte la valeur du chrono selon les resets
            timer = Time.time - tempTime;
        // On arrondit le temps à la seconde précise
        //double second = System.Math.Floor(Time.time);
        // Puis aux centiemes
        //double first = System.Math.Round(Time.time, 2);

        // Afin de ne récupérer que les centiemes
        centiemes = /*(first - second) * 100;*/ (timer * 100) % 100;

            // On récupère les minutes
            minutes = (int)((timer - 1) / 60f);
            // Puis les secondes
            seconds = (int)((timer - 1) % 60f);
            // Enfin, on affiche la valeur du chrono sur l'écran de jeu
            totalTimer.text = "Total time : " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + centiemes.ToString("00");
        }
	}

    // Fonction permettant de reinitialiser le chrono
    public void ResetTimer(float temp)
    {
        tempTime = temp;
    }
}
