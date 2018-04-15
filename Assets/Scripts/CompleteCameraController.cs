using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe permettant à la camera de suivre le joueur
 */ 

public class CompleteCameraController : MonoBehaviour
{
    // On récupère l'objet joueur
    public GameObject player;       

    // On récupère la distance entre le joueur et la camera
    private Vector3 offset;         

    // On calcule et enregistre la valeur de la distance
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Après chaque frame, on applique à la caméra un déplacement égal à la distance séparant celle-ci du joueur
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}