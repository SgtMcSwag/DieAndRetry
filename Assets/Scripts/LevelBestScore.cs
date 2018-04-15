using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Fonction gérant l'affichage du meilleur temps sur un niveau dans le menu
 */ 

public class LevelBestScore : MonoBehaviour
{

    private float minutes, seconds, centiemes;

    // On récupère l'index du niveau actuel
    public int level;
    // Et le texte où afficher le score
    public Text LevelBestScoreText;
    
    // On récupère le score enregistré dans des variables
    void Start()
    {
        minutes = PlayerPrefs.GetFloat("MinutesFloor" + level);
        seconds = PlayerPrefs.GetFloat("SecondsFloor" + level);
        centiemes = PlayerPrefs.GetFloat("CentiemesFloor" + level);
    }

    // Si un score sur ce niveau est déjà enregistré, on l'affiche, et on affiche "/" sinon
    void Update ()
    {
        if (seconds != 0)
            LevelBestScoreText.text = "Niveau" + level + " : " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + centiemes.ToString("00");
        else
            LevelBestScoreText.text = "Niveau" + level + " : /";
    }
}
