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
        hitText = GameObject.Find("Hit Text");
        Debug.Log(hitText);
    }

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;

        if(Time.time >= endLifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            hitText.GetComponent<IncreaseHitText>().Increase();
        }
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Projectile")
        {
            Debug.Log(collision.gameObject.tag);
            Destroy(gameObject);
        }
    }
}