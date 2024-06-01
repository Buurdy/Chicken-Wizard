using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentPickup : MonoBehaviour, IInteractable
{
    [SerializeField] bool moreProjectiles, projectileSpeed;
    PlayerController playerController;
    string nameOfComponent;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (moreProjectiles) nameOfComponent = "more projectiles";
        else if (projectileSpeed) nameOfComponent = "projectile speed";
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void OnInteract()
    {
        if(moreProjectiles)
        {
            playerController.projectileCount++;
        }
        else if (projectileSpeed)
        {
            playerController.projectileSpeed++;
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
