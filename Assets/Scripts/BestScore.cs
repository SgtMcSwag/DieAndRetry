using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Classe récupérant et affichant le meilleur temps total dans le menu
 */ 

public class BestScore : MonoBehaviour {

    private float minutes, seconds, centiemes;
    private int essais;

    // Texte à modifier
    public Text totalScoreText;

	// On récupère le meilleur temps dans des variables
	void OnGUI () {
        minutes = PlayerPrefs.GetFloat("MinutesTotal");
        seconds = PlayerPrefs.GetFloat("SecondsTotal");
        centiemes = PlayerPrefs.GetFloat("CentiemesTotal");

        essais = PlayerPrefs.GetInt("NbTotalTries");

        // Si un temps est enregistré, on l'affiche. Sinon, on affiche "/" à la place
        if (seconds != 0)
            totalScoreText.text = "Temps Total : " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + centiemes.ToString("00")
                                + " (Morts : " + essais + ")";
        else
            totalScoreText.text = "Temps Total : /";
    }
}
