using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPickup : MonoBehaviour, IInteractable
{
    [SerializeField] bool moreProjectiles, projectileSpeed, projectileSize;
    PlayerController playerController;
    string nameOfComponent;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (moreProjectiles) nameOfComponent = "More Projectiles";
        else if (projectileSpeed) nameOfComponent = "Projectile Speed";
        else if (projectileSize) nameOfComponent = "Projectile Size";
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void OnInteract()
    {
        if (moreProjectiles)
        {
            playerController.projectileCount++;
            playerController.componentCount++;
            playerController.components.Add(nameOfComponent);
        }
        else if (projectileSpeed)
        {
            playerController.projectileSpeed++;
            playerController.componentCount++;
            playerController.components.Add(nameOfComponent);
        }
        else if(projectileSize)
        {
            playerController.projectileSize++;
            playerController.componentCount++;
            playerController.components.Add(nameOfComponent);
        }
        DestorySelf();
    }

    void DestorySelf()
    {
        Destroy(gameObject);
    }

    public void OnHover()
    {
    }

    public void OnUnhover()
    {
    }

    public string GetMessage()
    {
        return "Pickup " + nameOfComponent;
    }
}
