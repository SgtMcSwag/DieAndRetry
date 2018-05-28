using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Classe initialisant les volumes des différents sons du jeu au lancement de celui-ci
 */

public class InitVolume : MonoBehaviour
{

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    // Appelle la fonction "InitializeVolume" au lancement du jeu
    void Start()
    {
        InitializeVolume();
    }

    // Fonction initialisant les volumes des différents sons du jeu au maximum
    private void InitializeVolume()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
            PlayerPrefs.SetFloat("MasterVolume", 1.0f);
        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetFloat("MusicVolume", 1.0f);
        if (!PlayerPrefs.HasKey("EffectVolume"))
            PlayerPrefs.SetFloat("EffectVolume", 1.0f);

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        effectsSlider.value = PlayerPrefs.GetFloat("EffectVolume");
    }
}
