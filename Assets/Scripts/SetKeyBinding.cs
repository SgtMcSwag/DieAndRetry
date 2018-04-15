using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Classe permettant de gérer la modification des touches du jeu dans le menu
 */ 

public class SetKeyBinding : MonoBehaviour {

    // On récupère les objets nécessaires
    public GameObject button;
    public Text keyText;

    // Et on prépare un event
    private Event keyEvent;

    // Variable indiquant si le jeu attend que l'utilisateur presse un bouton
    private bool waitingForKey = false;

    void OnGUI ()
    {
        // On récupère l'event actuel
        keyEvent = Event.current;

        // Selon le bouton auquel est attaché le script
        switch (button.name)
        {
            case "RespawnButton":
                // Affiche dans le texte du bouton la touche enregistrée, ou à défaut la touche par défaut
                keyText.text = PlayerPrefs.GetString("respawnKey", "Space");
                // Si l'utilisateur presse une touche alors que le jeu l'attend, on l'enregistre et on termine l'attente
                if (waitingForKey && keyEvent.isKey)
                {
                    PlayerPrefs.SetString("respawnKey", keyEvent.keyCode.ToString());
                    waitingForKey = false;
                }
                break;
            case "UpButton":
                // Affiche dans le texte du bouton la touche enregistrée, ou à défaut la touche par défaut
                keyText.text = PlayerPrefs.GetString("jumpKey", "Z");
                // Si l'utilisateur presse une touche alors que le jeu l'attend, on l'enregistre et on termine l'attente
                if (waitingForKey && keyEvent.isKey)
                {
                    PlayerPrefs.SetString("jumpKey", keyEvent.keyCode.ToString());
                    waitingForKey = false;
                }
                break;
            case "DownButton":
                // Affiche dans le texte du bouton la touche enregistrée, ou à défaut la touche par défaut
                keyText.text = PlayerPrefs.GetString("downKey", "S");
                // Si l'utilisateur presse une touche alors que le jeu l'attend, on l'enregistre et on termine l'attente
                if (waitingForKey && keyEvent.isKey)
                {
                    PlayerPrefs.SetString("downKey", keyEvent.keyCode.ToString());
                    waitingForKey = false;
                }
                break;
            case "LeftButton":
                // Affiche dans le texte du bouton la touche enregistrée, ou à défaut la touche par défaut
                keyText.text = PlayerPrefs.GetString("leftKey", "Q");
                // Si l'utilisateur presse une touche alors que le jeu l'attend, on l'enregistre et on termine l'attente
                if (waitingForKey && keyEvent.isKey)
                {
                    PlayerPrefs.SetString("leftKey", keyEvent.keyCode.ToString());
                    waitingForKey = false;
                }
                break;
            case "RightButton":
                // Affiche dans le texte du bouton la touche enregistrée, ou à défaut la touche par défaut
                keyText.text = PlayerPrefs.GetString("rightKey", "D");
                // Si l'utilisateur presse une touche alors que le jeu l'attend, on l'enregistre et on termine l'attente
                if (waitingForKey && keyEvent.isKey)
                {
                    PlayerPrefs.SetString("rightKey", keyEvent.keyCode.ToString());
                    waitingForKey = false;
                }
                break;
        }
    }

    // Fonction mettant le jeu en attente d'une pression de touche de la part de l'utilisateur
    public void waitForKey()
    {
        waitingForKey = true;
    }
}
