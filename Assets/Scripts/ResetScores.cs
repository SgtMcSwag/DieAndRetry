using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe permettant de réinitialiser les scores du joueur à partir du menu
 */ 

public class ResetScores : MonoBehaviour {

    // Fonction de réinitialisation des scores
    public void ReinitialiserScores ()
    {
        // On réinitialise les scores de chaque niveau
        for (var i = 1; i < 6; i++) { 
            PlayerPrefs.SetFloat("MinutesFloor" + i, 0);
            PlayerPrefs.SetFloat("SecondsFloor" + i, 0);
            PlayerPrefs.SetFloat("CentiemesFloor" + i, 0);
        }
        // Et le temps total
        PlayerPrefs.SetFloat("MinutesTotal", 0);
        PlayerPrefs.SetFloat("SecondsTotal", 0);
        PlayerPrefs.SetFloat("CentiemesTotal", 0);
    }
}
