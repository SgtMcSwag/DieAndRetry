using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Classe gérant les contrôles et autres aspects du joueur
 */

public class PlayerControls : MonoBehaviour
{

    // Variables d'état du joueur
    private bool isReady;
    private bool isGrounded = false;
    private bool hasJumped = false;
    private bool doubleJumpReady = false;
    private bool isDead = false;
    private bool facingRight = true;
    private bool wallJumpingR = false;
    private bool wallJumpingL = false;

    // Variable indiquant que le personnage slide contre un mur
    public bool wallSliding;

    // Références portant sur le contact avec un mur
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;

    // Valeur de l'index de la scène actuelle
    private int currentScene;

    // Touches de mouvements du personnage
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode jump;
    public KeyCode down;

    // Variable permettant le "drag" horizontal du personnage
    private float drag = 0.3f;

    // Vitesse du joueur
    public float speed = 10.0f;
    // Vitesse de descente du joueur
    public float downSpeed = 3.0f;
    // Les différents objets nécessaires
    private Rigidbody2D rb2d;
    private GameManager gameManager;
    public GameObject player;

    private Timer timers;

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
        transform.Rotate(0, 0,90);
        Camera.main.transform.Rotate(0, 0, 90);

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

        // Et active les deux timers
        timers.timerOn = true;

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
    }

    // Fonction réinitialisant la partie
    public void ReinitGame()
    {
        // On recommence la partie
        Restart();
        // Et on réinitialise le nombre de tentatives du joueur
        PlayerPrefs.SetInt("NbTotalTries", 0);
    }

    void Start()
    {
        // On initialise les objets nécessaires
        rb2d = GetComponent<Rigidbody2D>();
        HUD = GameObject.FindGameObjectWithTag("HUD");
        timers = player.GetComponent<Timer>();
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

        // On récupère l'objet gérant la partie
        gameManager = GameObject.FindObjectOfType<GameManager>();

        // Après une seconde, on autorise le déplacement du personnage
        StartCoroutine(WaitForReady());
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

    void Update()
    {

        // On récupère le volume des effets du jeu pour celui du saut
        jumpSound.volume = PlayerPrefs.GetFloat("EffectVolume");

        // Si le personnage n'est pas mort et est prêt à être déplacé
        if (!isDead && isReady)
        {
            this.vect = rb2d.velocity;

            // On gère les déplacements selon le bouton pressé et si le personnage est en train de wall jump

            if (Input.GetKey(jump) && isGrounded && !wallSliding)
            {
                vect.y = speed;
                // Le personnage n'est plus à terre
                isGrounded = false;
                // Le personnage a sauté une fois
                hasJumped = true;
                // On lance le son de saut
                jumpSound.Play();
            }
            // Si on appuie sur le bouton de saut alors que le personnage n'est pas à terre et est prêt pour le second saut
            else if (Input.GetKey(jump) && doubleJumpReady && !isGrounded && !wallSliding)
            {
                vect.y = speed;
                // Le personnage ne peut plus sauter
                doubleJumpReady = false;
            }
            // On gère ensuite les déplacements selon le bouton pressé
            else if (Input.GetKey(down))
            {
                vect.y = -speed;
            }
            else if (Input.GetKey(moveLeft) && Input.GetKey(moveRight))
            {
                vect.x = 0f;
            }
            else if (Input.GetKey(moveLeft) && !wallJumpingL)
            {
                vect.x = -speed;
            }
            else if (Input.GetKey(moveRight) && !wallJumpingR)
            {
                vect.x = speed;
            }
            else if (!Input.anyKey)
            {
                vect.x = 0;
            }
            rb2d.velocity = vect;

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
        }
        // Sinon, on bloque les mouvements du personnage et on réinitialise le timer du niveau
        else
        {
            // On bloque la position du personnage
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            //timers.ResetLevelTimer(Time.time);
            timers.timerOn = false;
        }

        // Si le personnage n'est pas au sol
        if (!isGrounded)
        {
            // On vérifie si le joueur est contre un mur
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.25f, wallLayerMask);

            // Si le joueur se déplace dans le même sens que le mur
            if ((facingRight && Input.GetKey(moveRight)) || (!facingRight && Input.GetKey(moveLeft)))
            {
                if (wallCheck)
                {
                    // Alors il slide
                    wallSliding = true;
                }
            }
        }

        // Si le joueur est au sol ou n'est pas contre un mur
        if (!wallCheck || isGrounded)
            // Alors il ne slide pas
            wallSliding = false;
    }

    // Fonction appliquant un drag horizontal après chaque frame et gérant les sauts
    private void FixedUpdate()
    {
        // On applique les déplacements si on ne slide pas
        if (!wallSliding)
        {

            // On change la direction dans laquelle 
            if (vect.x < 0)
            {
                facingRight = false;
            }
            else
            {
                facingRight = true;
            }
            // On change l'orientation du sprite du personnage selon la direction dans laquelle il se déplace
            Flip();
            // On applique le déplacement du personnage
            rb2d.velocity = vect;
        }
        else
        {
            // On gère le "wall slide"
            HandleWallSliding();
        }



        // On applique un drag horizontal
        var vel = rb2d.velocity;
        vel.x *= 1.0f - drag;
        rb2d.velocity = vel;
    }

    // Fonction gérant le sliding du personnage contre un mur
    void HandleWallSliding()
    {
        // On réduit la vitesse de chute du personnage
        rb2d.velocity = new Vector2(rb2d.velocity.x, -speed / 8);

        // Si le joueur saute alors qu'il slide
        if (Input.GetKey(jump))
        {
            // On vérifie dans quel sens le mur se trouve et on oriente le saut dans le sens opposé
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-500, speed));
                rb2d.velocity = new Vector3(rb2d.velocity.x, 10f, rb2d.velocity.y);

                // On indique que le personnage wall jump afin de modifier les possibilités de déplacement en conséquence
                wallJumpingR = true;
                Invoke("WallJumpR", 0.3f);
            }
            else
            {
                rb2d.AddForce(new Vector2(500, speed));
                rb2d.velocity = new Vector3(rb2d.velocity.x, 10f, rb2d.velocity.y);

                wallJumpingL = true;
                Invoke("WallJumpL", 0.3f);
            }
        }
    }

    // Fonction permettant de retourner le sprite du personnage
    private void Flip()
    {
        // On récupère le scale du personnage
        Vector3 theScale = transform.localScale;

        // Selon la direction dans laquelle il regarde, on retourne le personnage dans le bon sens
        if (facingRight)
        {
            theScale.x = 1;
        }
        else
        {
            theScale.x = -1;
        }

        // Et on applique la modification
        transform.localScale = theScale;
    }

    // Fonctions à appeler avec Invoke() permettant de rétablir les déplacements après un wall jump
    void WallJumpR()
    {
        wallJumpingR = false;
    }

    void WallJumpL()
    {
        wallJumpingL = false;
    }

    // Co-routine permettant de préparer le début d'un niveau
    IEnumerator WaitForReady()
    {
        yield return new WaitForSeconds(1);
        Ready();

        // Et on réinitialise les timers
        if (currentScene == 1)
            timers.ResetTotalTimer(Time.time);

        // On reset le timer du niveau
        timers.ResetLevelTimer(Time.time);

        // Et active les deux timers
        timers.timerOn = true;
    }
}
