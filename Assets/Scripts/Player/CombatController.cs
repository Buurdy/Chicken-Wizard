using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Transform attackPoint;
    public Transform rotationTransform;

    SpellManager spellManager;
    ProjectileFactory projectileFactory;
    WandConfiguration WandConfiguration => spellManager.CurrentWandConfiguration;
    public Vector3 AimDirection { get; private set; }
    private void Awake()
    {
       spellManager = GetComponent<SpellManager>();
        projectileFactory = new ProjectileFactory(WandConfiguration);

    }

    void PollForEvents()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    float lastAttack;
    private void Attack()
    {
       if (Time.time < lastAttack)
       {
           return;
       }
       lastAttack = Time.time + WandConfiguration.attackCooldown;

        for (int i = 0; i < WandConfiguration.projectileCount; i++)
        {
            SpawnProjectile();
        }
    }

    void SpawnProjectile()
    {
        float angleMod = UnityEngine.Random.Range(-WandConfiguration.projectileConfiguration.randomAccuracy, WandConfiguration.projectileConfiguration.randomAccuracy);
        var useAimDirection = Quaternion.Euler(0, 0, angleMod) * AimDirection;

        Projectile projectile = projectileFactory.SpawnProjectile(attackPoint.position, useAimDirection, WandConfiguration.projectileConfiguration);
    }

    private void Update()
    {
        PointToAttackDirection();
        PollForEvents();
    }

    void PointToAttackDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;
        rotationTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        AimDirection = direction;
    }
}
