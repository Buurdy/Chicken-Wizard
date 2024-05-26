using UnityEngine;

public interface IInteractable
{
    Vector2 GetPosition();
    void OnInteract();
    void OnHover();
    void OnUnhover();
}
