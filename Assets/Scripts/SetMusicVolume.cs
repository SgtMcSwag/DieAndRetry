using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Classe modifiant le volume de la musique du jeu à partir du slider correspondant dans les options
 */

public class SetMusicVolume : MonoBehaviour
{

    public Slider musicSlider;

    // Fonction appelant la fonction "ValueChangedCheck" lorsque la valeur du slider est modifiée
    public void Start()
    {
        musicSlider.onValueChanged.AddListener(delegate { ValueChangedCheck(); });
    }

    // Fonction enregistrant la nouvelle valeur du slider dans les préférences du joueur
    public void ValueChangedCheck()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
}
