using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour, IInteractable
{
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void OnInteract()
    {
        Debug.Log("Interacted with object");
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
}
