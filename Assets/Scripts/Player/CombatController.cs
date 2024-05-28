using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Transform attackPoint;
    public Transform rotationTransform;
    public WandConfiguration wandConfiguration;

    SpellManager spellManager;
    ProjectileFactory projectileFactory;

    public Vector3 AimDirection { get; private set; }
    private void Awake()
    {
        projectileFactory = new ProjectileFactory(wandConfiguration);
        spellManager = GetComponent<SpellManager>();
        spellManager.DefaultWandConfiguraiton = wandConfiguration;
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
       lastAttack = Time.time + wandConfiguration.attackCooldown;
       Projectile projectile = projectileFactory.SpawnProjectile(attackPoint.position, AimDirection, wandConfiguration.projectileConfiguration);
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
