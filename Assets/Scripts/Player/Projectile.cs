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
    PlayerController playerController;

    public void Spawn(Vector2 direction)
    {
        playerController = FindObjectOfType<PlayerController>();
        Debug.DrawRay(transform.position, direction, Color.red, 1f);
        lastPosition = transform.position;
        this.direction = direction.normalized;
        //this.configuration = configuraiton;
        Invoke(nameof(OnLifeTimeLost), playerController.projectileLifetime);
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
            transform.position += (Vector3)direction * playerController.projectileSpeed * Time.deltaTime;
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
        foreach (var hit in hits)
        {
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage();
            }
        }
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