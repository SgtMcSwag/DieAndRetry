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
    private int essais;

    // On récupère l'index du niveau actuel
    public int level;
    // Et le texte où afficher le score
    public Text LevelBestScoreText;
    
    // On récupère le score enregistré dans des variables
    void OnGUI()
    {
        minutes = PlayerPrefs.GetFloat("MinutesFloor" + level);
        seconds = PlayerPrefs.GetFloat("SecondsFloor" + level);
        centiemes = PlayerPrefs.GetFloat("CentiemesFloor" + level);

        essais = PlayerPrefs.GetInt("NbTriesFloor" + level);

        // Si un score sur ce niveau est déjà enregistré, on l'affiche, et on affiche "/" sinon
        if (seconds != 0)
            LevelBestScoreText.text = "Niveau" + level + " : " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + centiemes.ToString("00")
                                    + " (Morts : " + essais + ")";
        else
            LevelBestScoreText.text = "Niveau" + level + " : /";
    }
}
