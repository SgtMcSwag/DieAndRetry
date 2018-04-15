using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Classe modifiant le volume des effets du jeu à partir du slider correspondant dans les options
 */

public class SetEffectsVolume : MonoBehaviour
{
    public Slider effectSlider;

    // Fonction appelant la fonction "ValueChangedCheck" lorsque la valeur du slider est modifiée
    public void Start()
    {
        effectSlider.onValueChanged.AddListener(delegate { ValueChangedCheck(); });
    }

    // Fonction enregistrant la nouvelle valeur du slider dans les préférences du joueur
    public void ValueChangedCheck()
    {
        PlayerPrefs.SetFloat("EffectVolume", effectSlider.value);
    }
}
