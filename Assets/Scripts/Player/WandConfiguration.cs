using UnityEngine;

[CreateAssetMenu]
public class WandConfiguration : ScriptableObject
{
    public ProjectileConfiguration projectileConfiguration;
    public GameObject projectileObject;
    public float attackCooldown;
}
