using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Classe récupérant et affichant le meilleur temps total dans le menu
 */ 

public class BestScore : MonoBehaviour {

    private float minutes, seconds, centiemes;

    // Texte à modifier
    public Text totalScoreText;

	// On récupère le meilleur temps dans des variables
	void Start () {
        minutes = PlayerPrefs.GetFloat("MinutesTotal");
        seconds = PlayerPrefs.GetFloat("SecondsTotal");
        centiemes = PlayerPrefs.GetFloat("CentiemesTotal");
    }
	
	// Si un meilleur temps est enregistré, on l'afficher. Sinon, on affiche "/" à la place
	void Update () {
        if (seconds != 0)
            totalScoreText.text = "Temps Total : " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + centiemes.ToString("00");
        else
            totalScoreText.text = "Temps Total : /";
    }
}
