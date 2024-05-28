using UnityEngine;

[CreateAssetMenu]
public class WandConfiguration : ScriptableObject
{
    public ProjectileConfiguration projectileConfiguration;
    public int projectileCount;
    public GameObject projectileObject;
    public float attackCooldown;
}
