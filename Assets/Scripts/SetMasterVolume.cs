using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Classe modifiant le volume général du jeu à partir du slider correspondant dans les options
 */

public class SetMasterVolume : MonoBehaviour
{

    public Slider masterSlider;

    // Fonction appelant la fonction "ValueChangedCheck" lorsque la valeur du slider est modifiée
    public void Start()
    {
        masterSlider.onValueChanged.AddListener(delegate { ValueChangedCheck(); });
    }

    // Fonction enregistrant la nouvelle valeur du slider dans les préférences du joueur
    public void ValueChangedCheck()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
        AudioListener.volume = masterSlider.value;
    }
}
