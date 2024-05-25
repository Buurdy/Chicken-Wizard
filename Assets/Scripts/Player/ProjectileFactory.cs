using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory
{
    private readonly WandConfiguration wandConfiguration;

    public ProjectileFactory(WandConfiguration defaultProjectileConfiguration)
    {
        this.wandConfiguration = defaultProjectileConfiguration;
    }

    public Projectile SpawnProjectile(Vector3 position, Vector3 direction, ProjectileConfiguration projectileConfiguration)
    {
        Projectile projectile = Object.Instantiate(wandConfiguration.projectileObject, position, Quaternion.identity).GetComponent<Projectile>();
        projectile.Spawn(direction, projectileConfiguration);
        return projectile;
    }
}

