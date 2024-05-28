using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public LayerMask interactableLayer;
    public float interactionRadius = 1f;

    public Transform interactionPanelUi;
    public TextMeshProUGUI interactionPanelText;
    public Vector2 interactionPanelOffset = new Vector2(0, 0.1f);

    const string InteractionHeader = "Press E to ";

    private void Update()
    {
        CheckForInteractable();
        UpdateInteractableInfo();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnInteract();
            }
        }
    }

    RaycastHit2D[] hitBuffer = new RaycastHit2D[5];
    IInteractable currentInteractable;

    void CheckForInteractable()
    {
        // Check for interactable objects in the vicinity
        int hitCount = Physics2D.CircleCastNonAlloc(transform.position, interactionRadius, Vector2.zero, hitBuffer, 0f, interactableLayer);

        float closest = float.MaxValue;
        if (hitCount == 0)
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnUnhover();
                currentInteractable = null;
            }
            return;
        }

        for (int i = 0; i < hitCount; i++)
        {
            Debug.DrawLine(transform.position, hitBuffer[i].point, Color.green);
            if (hitBuffer[i].collider.TryGetComponent(out IInteractable interactable))
            {
                float distance = Vector2.Distance(transform.position, hitBuffer[i].point);
                if (distance < closest)
                {
                    if (currentInteractable != null && currentInteractable != interactable)
                    {
                        currentInteractable.OnUnhover();
                    }

                    closest = distance;
                    currentInteractable = interactable;
                    interactable.OnHover();
                }
            }
        }
    }

    void UpdateInteractableInfo()
    {
        if (currentInteractable != null)
        {
            interactionPanelUi.gameObject.SetActive(true);
            interactionPanelUi.position = Camera.main.WorldToScreenPoint(currentInteractable.GetPosition() + interactionPanelOffset);
            interactionPanelText.text = InteractionHeader + currentInteractable.GetMessage();
        }
        else
        {
            interactionPanelUi.gameObject.SetActive(false);
        }
    }
    
    private void OnDrawGizmos()
    {
        var lightGreen = new Color(0.5f, 1f, 0.5f, 0.5f);
        Gizmos.color = lightGreen;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
