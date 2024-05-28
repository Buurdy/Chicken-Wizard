using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemViewUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    SpellItemConfiguration SpellItemConfiguration;
    public Image SpellImage; 

    public void Set(SpellItemConfiguration spellItemConfiguration)
    {
        SpellItemConfiguration = spellItemConfiguration;
        SpellImage.sprite = spellItemConfiguration.Icon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovered on item");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Unhovered on item");
    }
}