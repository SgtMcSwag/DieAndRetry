using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThrough : MonoBehaviour {

    //This script is attached to the player
    private KeyCode jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Z")); //assign jump button here
    private KeyCode downKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downKey", "S")); //assign down button here
         
    public BoxCollider2D box ; //assign the BoxCollider2D of your player and set it to is trigger
         
     private void Update()
    {
        // If you want your object to go down on your platform
        // Similar to the Contra game platform feature.

        if (Input.GetKeyDown(jumpKey))
        {
            box.isTrigger = true;
        }
        if (Input.GetKeyUp(jumpKey))
        {
            box.isTrigger = false;
        }
    }

}
