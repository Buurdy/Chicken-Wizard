using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.Collections;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPos;

    public Transform target;

    public GameObject target2;

   public NavMeshAgent agent;

    RaycastHit2D hit;

   private bool setRotation = false;
    private float timer = 0;

    [SerializeField] SpriteRenderer llamaSprite;
    bool movingDown = true;
    [SerializeField] float squishSpeed, maxSquish;
    Vector3 normalSize;
    Vector3 lastPos;
   

    public LayerMask layermask;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target2 = GameObject.Find("Player");
        target = target2.transform;
        normalSize = llamaSprite.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (setRotation == false)
        {
            setRotation = true;
            spawnPos.transform.right = target.position - spawnPos.transform.position; //Sets rotation so enemy always starts looking at player
  
        }
        //agent.updateRotation = false;
        if(timer <= 0)
        {
           
            
        }
        else
        {
            timer -= Time.deltaTime;
        }

        Quaternion rotation = Quaternion.LookRotation(target.position - spawnPos.transform.position , spawnPos.transform.TransformDirection(Vector3.up));
        spawnPos.transform.rotation = new Quaternion( 0 , 0 , rotation.z , rotation.w );

        hit = Physics2D.Raycast(spawnPos.position, target.position - spawnPos.position, 10f, layermask);
        Debug.DrawRay(spawnPos.position, spawnPos.right * 10f, Color.red);
        
        if (hit.collider != null)
        {
            if (Physics2D.Raycast(spawnPos.position, target.position - spawnPos.position, 10f, layermask))
            {
                Debug.Log(hit.collider.gameObject.name);
                hit = Physics2D.Raycast(spawnPos.position, target.position - spawnPos.position, 10f, layermask);
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    if(timer <= 0)
                    {
                        GameObject instance = Instantiate(projectile, spawnPos.position, spawnPos.transform.rotation);
            
                        //instance.GetComponent<EnemyProjectile>().hitText = target2;
                        timer = 1;
                        //print("shot");
                    }
                agent.SetDestination(gameObject.transform.position);
                print("playerfound");
                }
                else
                {
        
                ToPlayer();
                print("playernotfound");
                }
            }
        }

        MovementAnimation();
        lastPos = transform.position;
    }

    void MovementAnimation()
    {
        //Checks which way the player is moving and flips the sprite to face to correct direction
        if (agent.desiredVelocity.x > 0) llamaSprite.flipX = true;
        else if (agent.desiredVelocity.x < 0) llamaSprite.flipX = false;

        //Checks if the player is moving
        if (lastPos != transform.position)
        {
            //These control the rotation of the sprite
            //Rotates the player sprite depending on the current rotation direction. Rotates the sprite around the rotation point, which should be at the chickens feet
            if (movingDown)
            {
                llamaSprite.transform.localScale -= new Vector3(0, squishSpeed * Time.deltaTime, 0);
                llamaSprite.transform.localPosition += new Vector3(0, squishSpeed * Time.deltaTime, 0);
            }
            else
            {
                llamaSprite.transform.localScale += new Vector3(0, squishSpeed * Time.deltaTime, 0);
                llamaSprite.transform.localPosition -= new Vector3(0, squishSpeed * Time.deltaTime, 0);
            }

            //Checks the sprites current rotation to see if it has passed the maximum rotation allowed, and switches the direction of rotation if it has
            if (movingDown && llamaSprite.transform.localScale.y <= maxSquish) movingDown = false;
            else if (!movingDown && llamaSprite.transform.localScale.y >= normalSize.y) movingDown = true;
        }
        //Executes if the player is not pressing any input
        else
        {
            //Resets the character sprites position and rotation
            llamaSprite.transform.localPosition = new Vector3(0, 0, 0);
            llamaSprite.transform.localScale = normalSize;
            movingDown = true;
        }
    }

    public void ToPlayer()
    {
        agent.SetDestination(target.position);
    }
}
