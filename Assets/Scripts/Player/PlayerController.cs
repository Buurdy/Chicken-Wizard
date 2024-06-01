using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject playerSprite;
    SpriteRenderer playerSpriteRenderer;
    [SerializeField] GameObject rotationPoint;
    Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] float movementSpeed;
    float xInput, yInput;
    Vector2 movementVector;

    [Header("Dodge")]
    [SerializeField] float dodgeTime;
    [SerializeField] float dodgeSpeedMultiplier, dodgeCooldown;
    float dodgeTimeEnd, dodgeCooldownEnd;
    bool isDodging;

    [Header("Animation")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxRotation;
    bool rotatingLeft = true;
    [SerializeField] float verticalSpeed, maxVerticalHeight;
    bool movingUp = true;

    [Header("Animation")]
    public int projectileCount;
    public int projectileSpeed, projectileLifetime;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Gets a reference to the sprite renderer of the chicken sprite
        playerSpriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the players input and stores it in a vector
        GetInput();
        //Plays walking animation when this is player input and the player is not currently dodging
        if(!isDodging) WalkAnimation();
        //Quickly rolls forward when inputted
        Dodge();
    }


    private void FixedUpdate()
    {
        //Applies input as player movement
        Movement();
    }

    //This function is used to get the players vertical and horizontal input and store it in a vector
    void GetInput()
    {
        //Checks if the player is dodging, this just makes it so they can only dodge in one direction
        if (!isDodging)
        {
            //Stores players left/right input as a float, with -1 being left and 1 being right.
            xInput = Input.GetAxisRaw("Horizontal");
            //Stores players up/down input as a float, with -1 being down and 1 being up.
            yInput = Input.GetAxisRaw("Vertical");
        }
        //Combines the inputs into a vector to get movement direciton. Normalizes this vector to ensure the player moves the same speed no matter the direction.
        movementVector = new Vector2(xInput, yInput).normalized;
    }

    //This function is used to apply input to player movement
    void Movement()
    {
        //Moves the player in the direction of movementVector at the speed of movementSpeed
        rb.velocity = movementVector * movementSpeed * Time.fixedDeltaTime;
    }

    //This function is used to dodge roll when inputted
    void Dodge()
    {
        //Checks if the player presses the dodge key, they are currently not dodging, and if the dodge is off cooldown
        if (Input.GetKeyDown(KeyCode.Space) && !isDodging && Time.time >= dodgeCooldownEnd)
        {
            //Sets the dodging state to true
            isDodging = true;
            //Sets when the dodge will finish
            dodgeTimeEnd = Time.time + dodgeTime;
            //Increases the speed of the player
            movementSpeed *= dodgeSpeedMultiplier;
            //Resets the player sprite
            playerSprite.transform.localPosition = new Vector3(0, 0, 0);
            playerSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        //Checks if the player has been dodging for the right amount of time
        if (isDodging && Time.time >= dodgeTimeEnd)
        {
            //Resets movement speed
            movementSpeed /= dodgeSpeedMultiplier;
            //Resets the player sprite
            playerSprite.transform.localPosition = new Vector3(0, 0, 0);
            playerSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
            movingUp = true;
            //Sets when the cooldown period will be finished
            dodgeCooldownEnd = Time.time + dodgeCooldown;
            //Sets the dodging state to false
            isDodging = false;
        }
        //Checks if the player is currently still dodging
        else if(isDodging && Time.time < dodgeTimeEnd)
        {
            //These check for which way the player is currently facing, and rotates the sprite in that direction. The rotation amount changes depending on how long the dodge roll is
            if (playerSpriteRenderer.flipX) playerSprite.transform.eulerAngles -= Vector3.forward * ((360 / dodgeTime) * Time.deltaTime);
            else playerSprite.transform.eulerAngles += Vector3.forward * ((360 / dodgeTime) * Time.deltaTime);
        }
    }

    void WalkAnimation()
    {
        //Checks if the player is moving
        if(movementVector.magnitude > 0)
        {
            //Checks which way the player is moving and flips the sprite to face to correct direction
            if(xInput > 0) playerSpriteRenderer.flipX = true;
            else if(xInput < 0) playerSpriteRenderer.flipX = false;

            //These control the rotation of the sprite
            //Rotates the player sprite depending on the current rotation direction. Rotates the sprite around the rotation point, which should be at the chickens feet
            if (rotatingLeft) playerSprite.transform.RotateAround(rotationPoint.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            else playerSprite.transform.RotateAround(rotationPoint.transform.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
            //Checks the sprites current rotation to see if it has passed the maximum rotation allowed, and switches the direction of rotation if it has
            if (rotatingLeft && playerSprite.transform.localRotation.z >= maxRotation) rotatingLeft = false;
            else if (!rotatingLeft && playerSprite.transform.localRotation.z <= -maxRotation) rotatingLeft = true;


            //These control the vertical movement of the sprite
            //Moves the player up or down on the sprites local axis depending on the current vertical movement direction
            if (movingUp) playerSprite.transform.localPosition += new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            else playerSprite.transform.localPosition -= new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            //Checks the sprites current local vertical position to see if it has passed the maximum amount of movement, and switches direction if it has
            if (movingUp && playerSprite.transform.localPosition.y >= maxVerticalHeight) movingUp = false;
            else if(!movingUp && playerSprite.transform.localPosition.y <= 0) movingUp = true;
        }
        //Executes if the player is not pressing any input
        else
        {
            //Resets the character sprites position and rotation
            playerSprite.transform.localPosition = new Vector3(0, 0, 0);
            playerSprite.transform.localRotation = Quaternion.Euler(0,0,0);
            movingUp = true;
        }
    }
}
