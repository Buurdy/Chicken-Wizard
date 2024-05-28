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

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        if (other.gameObject.tag == "Player" )
        {
            hitText.GetComponent<IncreaseHitText>().Increase();
        }
    }
}