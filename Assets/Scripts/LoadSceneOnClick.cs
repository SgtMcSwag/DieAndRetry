using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Classe permettant de charger une scène par rapport à son index
 */ 

public class LoadSceneOnClick : MonoBehaviour
{
    // Fonction chargeant la scène
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
