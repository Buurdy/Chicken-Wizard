using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float collisionRadius = 0.05f;
    public LayerMask collisionMask;
    bool isSpawned = false;
    ProjectileConfiguration configuration;
    Vector2 direction;
    Vector2 lastPosition;

    public void Spawn(Vector2 direction, ProjectileConfiguration configuraiton)
    {
        Debug.DrawRay(transform.position, direction, Color.red, 1f);
        this.direction = direction.normalized;
        this.configuration = configuraiton;
        Invoke(nameof(OnLifeTimeLost), configuraiton.lifetime);
        isSpawned = true;
    }

    private void Update()
    {
        MoveProjectile();
        CheckForCollision();
        lastPosition = transform.position;
    }

    private void MoveProjectile()
    {
        if (isSpawned)
        {
            transform.position += (Vector3)direction * configuration.speed * Time.deltaTime;
        }
    }

    void CheckForCollision()
    {
        var between = (Vector2)transform.position - lastPosition;
        var hits = Physics2D.CircleCastAll(lastPosition, collisionRadius, between, between.magnitude, collisionMask);
        if (hits.Length > 0)
        {
            OnCollision(hits);
        }
    }

    void OnCollision(RaycastHit2D[] hits)
    {
        OnLifeTimeLost();
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