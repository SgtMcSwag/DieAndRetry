using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe permettant de quitter le jeu à partir du menu
 */ 

public class QuitGame : MonoBehaviour
{
    // Fonction permettant de quitter le jeu
    public void Quit()
    {
        // Si le jeu est ouvert dans unity, on quitte le Play Mode. Sinon, on quitte l'application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
