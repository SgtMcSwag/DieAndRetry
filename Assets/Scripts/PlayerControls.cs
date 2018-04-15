using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Classe gérant les contrôles et autres aspects du joueur
 */ 

public class PlayerControls : MonoBehaviour {

    // Variables d'état du joueur
    private bool isReady;
    private bool isGrounded;
    private bool hasJumped;
    private bool doubleJumpReady;
    private bool isDead = false;

    // Valeur de l'index de la scène actuelle
    private int currentScene;

    // Touches de mouvements du personnage
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;
    public KeyCode down;
    // Vitesse du joueur
    public float speed = 10.0f;
    // Vitesse de descente du joueur
    public float downSpeed = 3.0f;
    // Les différents objets nécessaires
    private Rigidbody2D rb2d;
    private GameManager gameManager;
    public Text levelTimerText;
    public Text totalTimerText;

    private LevelTimer levelTimer;
    private TotalTimer totalTimer;

    GameObject HUD;

    // Son de saut du joueur
    private AudioSource jumpSound;

    private Vector2 vect;

    // Fonction indiquant que le joueur est sur le sol
    public void Ground()
    {
        isGrounded = true;
    }

    // Fonction gérant la mort du joueur
    public void Die()
    {
        // Son état passe à "mort"
        isDead = true;
        // On appelle la fonction "PlayerDies()" du HUD
        HUD.SendMessage("PlayerDies", 0.5f, SendMessageOptions.RequireReceiver);
    }

    // Fonction reinitialisant le personnage
    public void ResetCharacter()
    {
        // On réinitialise les états du joueur
        transform.position = Vector2.down;
        isGrounded = false;
        hasJumped = false;
        doubleJumpReady = false;
        isDead = false;
        isReady = false;
        // On appelle la fonction "PlayerReborn()" du HUD
        HUD.SendMessage("PlayerReborn", 0.5f, SendMessageOptions.RequireReceiver);
    }

    // Fonction initialisant le personnage
    public void Ready()
    {
        // Le personnage est prêt à être déplacé
        isReady = true;
        // On empêche le personnage de tourner sur lui-même et on autorise les déplacements
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Fonction réinitialisant le personnage et sa position
    public void Restart()
    {
        // On appelle les fonctions précédemment décrites
        ResetCharacter();
        Ready();
        Invoke("Ready",  1);

        // On réinitialise le timer du niveau
        levelTimer.timerOn = true;
    }

    // Fonction réinitialisant la partie
    public void ReinitGame()
    {
        // On recommence la partie
        Restart();
        // Et on réinitialise le nombre de vies du joueur
        HUD.SendMessage("SetLives", 10);
    }
    
    void Start ()
    {
        // On initialise les objets nécessaires
        rb2d = GetComponent<Rigidbody2D>();
        HUD = GameObject.FindGameObjectWithTag("HUD");
        levelTimer = levelTimerText.GetComponent<LevelTimer>();
        totalTimer = totalTimerText.GetComponent<TotalTimer>();
        jumpSound = GetComponent<AudioSource>();
        // On empêche le son de saut d'être joué au démarrage
        jumpSound.playOnAwake = false;

        // On récupère l'index de la scène actuelle
        currentScene = SceneManager.GetActiveScene().buildIndex;

        // On récupère les touches de déplacement du personnage
        moveLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "Q"));
        moveRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Z"));
        down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downKey", "S"));

        // Après une seconde, on autorise le déplacement du personnage
        Invoke("Ready", 1);
        // Et on réinitialise le timer du niveau
        totalTimer.ResetTimer();

        // On active le timer
        levelTimer.timerOn = true;

        // On récupère l'objet gérant la partie
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void OnGUI()
    {
        // Si le personnage est mort, on affiche le texte de mort en rouge
        if (isDead)
        {
            GUI.contentColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "VOUS ETES MORT MAMENE");
        }
    }
    
    void Update () {
        // On récupère le volume des effets du jeu pour celui du saut
        jumpSound.volume = PlayerPrefs.GetFloat("EffectVolume");

        // Si le personnage n'est pas mort et est prêt à être déplacé
        if (!isDead && isReady)
        {
            var vel = rb2d.velocity;
            // Si on appuie sur le bouton de saut et que le personnage est à terre
            if (Input.GetKey(jump) && isGrounded)
            {
                vel.y = speed;
                // Le personnage n'est plus à terre
                isGrounded = false;
                // Le personnage a sauté une fois
                hasJumped = true;
                // On lance le son de saut
                jumpSound.Play();
            }
            // Si on appuit sur le bouton de saut alors que le personnage n'est pas à terre et est prêt pour le second saut
            else if (Input.GetKey(jump) && doubleJumpReady && !isGrounded)
            {
                vel.y = speed;
                // Le personnage ne peut plus sauter
                doubleJumpReady = false;
            }
            // On gère ensuite les déplacements selon le bouton pressé
            else if (Input.GetKey(down))
            {
                vel.y = -speed;
            }
            else if (Input.GetKey(moveLeft) && Input.GetKey(moveRight))
            {
                vel.x = 0f;
            }
            else if (Input.GetKey(moveLeft))
            {
                vel.x = -speed;
            }
            else if (Input.GetKey(moveRight))
            {
                vel.x = speed;
            }
            else if (!Input.anyKey)
            {
                vel.x = 0;
            }
            rb2d.velocity = vel;

            // Si le personnage a sauté une fois
            if (hasJumped)
            {
                // Et que le bouton de saut est enfoncé
                if (Input.GetKeyUp(jump))
                {
                    // Alors il est prêt à faire le second saut
                    doubleJumpReady = true;
                    hasJumped = false;
                }
            }
            // On applique les déplacements au personnage
            var pos = transform.position;
            transform.position = pos;
        }
        // Sinon, on bloque les mouvements du personnage et on réinitialise le timer du niveau
        else
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            
            levelTimer.ResetTimer();
        }
    }
}
