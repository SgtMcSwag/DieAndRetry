using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe initialisant les volumes des différents sons du jeu au lancement de celui-ci
 */

public class InitVolume : MonoBehaviour
{

    // Appelle la fonction "InitializeVolume" au lancement du jeu
    void Start()
    {
        InitializeVolume();
    }

    // Fonction initialisant les volumes des différents sons du jeu au maximum
    private void InitializeVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", 1.0f);
        PlayerPrefs.SetFloat("MusicVolume", 1.0f);
        PlayerPrefs.SetFloat("EffectVolume", 1.0f);
    }
}
