using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour
{
    public float collisionRadius = 0.05f;
    //Rigidbody2D rb;

    public float Speed;

    public GameObject hitText;

    [SerializeField] float lifetime;
    float endLifetime;

    private void Awake()
    {
        endLifetime = Time.time + lifetime;
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;

        if(Time.time >= endLifetime)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" )
        {
            hitText.GetComponent<IncreaseHitText>().Increase();
        }
        if(other.gameObject.tag != "Enemy" || other.gameObject.tag != "Projectile")
        {
            Destroy(gameObject);
        }
    }
}