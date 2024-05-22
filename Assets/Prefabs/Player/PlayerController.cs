using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject playerSprite;
    SpriteRenderer playerSpriteRenderer;
    [SerializeField] GameObject rotationPoint;
    [Header("Movement")]
    [SerializeField] float movementSpeed;
    float xInput, yInput;
    Vector3 movementVector;
    [Header("Animation")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxRotation;
    bool rotatingLeft = true;
    [SerializeField] float verticalSpeed, maxVerticalHeight;
    bool movingUp = true;


    // Start is called before the first frame update
    void Start()
    {
        playerSpriteRenderer = playerSprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gets the players input and stores it in a vector
        GetInput();
        //Applies the players input as movement
        Movement();
        //Plays walking animation when this is player input
        WalkAnimation();
    }

    //This function is used to get the players vertical and horizontal input and store it in a vector
    void GetInput()
    {
        //Stores players left/right input as a float, with -1 being left and 1 being right.
        xInput = Input.GetAxisRaw("Horizontal");
        //Stores players up/down input as a float, with -1 being down and 1 being up.
        yInput = Input.GetAxisRaw("Vertical");
        //Combines the inputs into a vector to get movement direciton. Normalizes this vector to ensure the player moves the same speed no matter the direction.
        movementVector = new Vector3(xInput, yInput, 0).normalized;
    }

    //This function is used to move the player character by the players input
    void Movement()
    {
        //Translates the player by the movementVector at the speed of movementSpeed.
        transform.Translate(movementVector * movementSpeed * Time.deltaTime);
    }

    void WalkAnimation()
    {
        //Checks to see if the player is inputting any movement
        if(movementVector.magnitude > 0)
        {
            //Checks which way the player is moving and flips the sprite to face to correct direction
            if(xInput > 0) playerSpriteRenderer.flipX = true;
            else if(xInput < 0) playerSpriteRenderer.flipX = false;

            //These control the rotation of the sprite
            //Rotates the player sprite depending on the current rotation direction
            if (rotatingLeft) playerSprite.transform.RotateAround(rotationPoint.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            else playerSprite.transform.RotateAround(rotationPoint.transform.position, Vector3.forward, -rotationSpeed * Time.deltaTime);
            //Checks the sprites current rotation to see if it has passed the maximum rotation allowed, and switches the direction of rotation if it has
            if (rotatingLeft && playerSprite.transform.localRotation.z >= maxRotation) rotatingLeft = false;
            else if (!rotatingLeft && playerSprite.transform.localRotation.z <= -maxRotation) rotatingLeft = true;

            //These control the vertical movement of the sprite
            //Moves the player up or down depending on the current vertical movement direction
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
