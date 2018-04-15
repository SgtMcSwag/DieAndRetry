using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe permettant la détection de la condition de mort
 */ 

public class deathByScript : MonoBehaviour {

    // Lorsqu'une collision se produit entre un "danger" et le joueur, on appelle la fonction "Die()" de celui-ci
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.SendMessage("Die", 1.0f, SendMessageOptions.RequireReceiver);
        }
    }
}
