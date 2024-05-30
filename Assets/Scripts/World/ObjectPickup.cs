using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour, IInteractable
{
    public SpellItemConfiguration SpellItemConfiguration;
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void OnInteract()
    {
        Debug.Log("Interacted with object");
        var spellManager = GameObject.FindObjectOfType<SpellManager>();
        if (spellManager.AddItem(SpellItemConfiguration)) {
            DestorySelf();
        }
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
        return "Pickup " + SpellItemConfiguration.Name;
    }
}
