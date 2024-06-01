using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

public class EnemyNavigation : MonoBehaviour
{
        //[SerializeField] Transform target;
        public Transform target;
        public TextMeshProUGUI text;
        //private int timesHit = 0;
        private float cooldown;

        public IncreaseHitText hittext;
    NavMeshAgent agent;

    bool rotatingLeft = true;
    bool movingUp = true;
    [SerializeField] float rotationSpeed, maxRotation, verticalSpeed, maxVerticalHeight;
    [SerializeField] GameObject rotatePoint;
    [SerializeField] SpriteRenderer foxSprite;
    Vector3 lastPos;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        
        WalkAnimation();
        lastPos = transform.position;
    }

    void WalkAnimation()
    {
        //Checks which way the player is moving and flips the sprite to face to correct direction
        if (agent.desiredVelocity.x > 0) foxSprite.flipX = true;
        else if (agent.desiredVelocity.x < 0) foxSprite.flipX = false;

        //Checks if the player is moving
        if (lastPos != transform.position)
        {
            //These control the rotation of the sprite
            //Rotates the player sprite depending on the current rotation direction. Rotates the sprite around the rotation point, which should be at the chickens feet
            if (rotatingLeft) foxSprite.transform.RotateAround(rotatePoint.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            else foxSprite.transform.RotateAround(rotatePoint.transform.position, Vector3.forward, -rotationSpeed * Time.deltaTime);

            //Checks the sprites current rotation to see if it has passed the maximum rotation allowed, and switches the direction of rotation if it has
            if (rotatingLeft && foxSprite.transform.localRotation.z >= maxRotation) rotatingLeft = false;
            else if (!rotatingLeft && foxSprite.transform.localRotation.z <= -maxRotation) rotatingLeft = true;


            //These control the vertical movement of the sprite
            //Moves the player up or down on the sprites local axis depending on the current vertical movement direction
            if (movingUp) foxSprite.transform.localPosition += new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            else foxSprite.transform.localPosition -= new Vector3(0, verticalSpeed * Time.deltaTime, 0);
            //Checks the sprites current local vertical position to see if it has passed the maximum amount of movement, and switches direction if it has
            if (movingUp && foxSprite.transform.localPosition.y >= maxVerticalHeight) movingUp = false;
            else if (!movingUp && foxSprite.transform.localPosition.y <= 0) movingUp = true;
        }
        //Executes if the player is not pressing any input
        else
        {
            //Resets the character sprites position and rotation
            foxSprite.transform.localPosition = new Vector3(0, 0, 0);
            foxSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
            movingUp = true;
        }
    }

    void  OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && cooldown <= 0)
        {
            /*timesHit +=1;
            cooldown = 1;*/
            hittext.Increase();
        }
    }
}
