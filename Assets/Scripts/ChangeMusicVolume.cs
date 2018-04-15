using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Fonction permettant la modification du volume de la musique
 */

public class ChangeMusicVolume : MonoBehaviour
{

    public AudioSource Music;
    float volume;

    // Récupère le volume à partir des préférences du joueur, entrées dans les options
    void Start()
    {
        volume = PlayerPrefs.GetFloat("MusicVolume");
        // Applique le volume récupéré au volume de la musique
        Music.volume = volume;
    }

    // Récupère et applique à chaque frame le volume obtenu à partir des options
    void Update()
    {
        volume = PlayerPrefs.GetFloat("MusicVolume");
        Music.volume = volume;
    }
}
