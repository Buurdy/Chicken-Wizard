using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public int MaxItems = 5;


    [Header("UI")]
    public Transform ItemViewHolder;
    public GameObject ItemViewPrefab;

    List<SpellItemConfiguration> currentItems = new ();
    List<GameObject> itemViewObjects = new ();

    public WandConfiguration DefaultWandConfiguraiton { get; set; }

    private void Start()
    {
        InitPropertyMap();
    }

    Dictionary<string, PropertyInfo> wandPropertyMap;
    void InitPropertyMap()
    {
        wandPropertyMap = new Dictionary<string, PropertyInfo>();
        var wandConfigProperties = typeof(WandConfiguration).GetProperties();
        var projectileConfigProperties = typeof(ProjectileConfiguration).GetProperties();

        foreach (var property in wandConfigProperties)
        {
            wandPropertyMap.Add(property.Name, property);
        }
        foreach (var projectileProperty in projectileConfigProperties)
        {
            wandPropertyMap.Add($"projectile.{projectileProperty.Name}", projectileProperty);
        }
    }

    public bool AddItem(SpellItemConfiguration item)
    {
        if (currentItems.Count >= MaxItems)
        {
            return false;
        }
        currentItems.Add(item);
        RenderItemView();
        CalculateOverrides();
        return true;
    }

    void RenderItemView()
    {
        foreach (var item in itemViewObjects)
        {
            Destroy(item);
        }
        itemViewObjects.Clear();

        foreach (var item in currentItems)
        {
            var viewInstance = Instantiate(ItemViewPrefab, ItemViewHolder).GetComponent<ItemViewUI>();
            viewInstance.Set(item);
            itemViewObjects.Add(viewInstance.gameObject);
        }
    }

    void CalculateOverrides()
    {
        var overrideConfig = new WandConfiguration();
        //TODO
    }

    void CalculateSingleOverrde(WandConfiguration currentOverride)
    {
        //TODO
    }
}
