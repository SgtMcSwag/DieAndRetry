using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Classe gérant les interations entre le joueur et le sol
 */

public class Ground : MonoBehaviour {
    
    // Si une collision se produit entre le joueur et le sol, on appelle la fonction "Ground()" du joueur
    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.SendMessage("Ground", 1.0f, SendMessageOptions.RequireReceiver);
        }
    }
}
