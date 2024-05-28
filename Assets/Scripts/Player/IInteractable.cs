using UnityEngine;

public interface IInteractable
{
    public string GetMessage();
    Vector2 GetPosition();
    void OnInteract();
    void OnHover();
    void OnUnhover();
}
