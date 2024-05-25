using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float collisionRadius = 0.05f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Spawn(Vector2 direction, ProjectileConfiguration configuraiton)
    {
        Debug.DrawRay(transform.position, direction, Color.red, 1f);
        rb.AddForce(direction.normalized * configuraiton.speed, ForceMode2D.Impulse);
        Invoke(nameof(OnLifeTimeLost), configuraiton.lifetime);
    }

    void OnLifeTimeLost()
    {
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }
}