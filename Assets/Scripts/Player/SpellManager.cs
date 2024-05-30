using System;
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

    public WandConfiguration DefaultWandConfiguraiton;
    public WandConfiguration CurrentWandConfiguration { get; private set; }


    private void Awake()
    {
        CurrentWandConfiguration = DefaultWandConfiguraiton;
    }
    private void Start()
    {
        InitPropertyMap();
    }

    Dictionary<string, FieldInfo> wandPropertyMap;
    void InitPropertyMap()
    {
        wandPropertyMap = new Dictionary<string, FieldInfo>();
        var wandConfigProperties = typeof(WandConfiguration).GetFields();
        var projectileConfigProperties = typeof(ProjectileConfiguration).GetFields();

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

        if (currentItems.Count == 0)
        {
            ItemViewHolder.gameObject.SetActive(false);
            return;
        } else
        {
            ItemViewHolder.gameObject.SetActive(true);
        }
        foreach (var item in currentItems)
        {
            var viewInstance = Instantiate(ItemViewPrefab, ItemViewHolder).GetComponent<ItemViewUI>();
            viewInstance.Set(item);
            itemViewObjects.Add(viewInstance.gameObject);
        }
    }

    void CalculateOverrides()
    {
        if (currentItems.Count == 0)
        {
            CurrentWandConfiguration = DefaultWandConfiguraiton;
            return;
        }

        var overrideConfig = new WandConfiguration();
        CopyDefaultConfig(DefaultWandConfiguraiton, overrideConfig);

        foreach (var item in currentItems)
        {
            CalculateSingleOverrde(overrideConfig, item);
        }
        CurrentWandConfiguration = overrideConfig;
    }

    void CopyDefaultConfig(WandConfiguration defaultConfig, WandConfiguration copyTarget)
    {
        foreach (var property in wandPropertyMap.Values)
        {
            object target = property.DeclaringType == typeof(ProjectileConfiguration) ? copyTarget.projectileConfiguration : copyTarget;
            object source = property.DeclaringType == typeof(ProjectileConfiguration) ? defaultConfig.projectileConfiguration : defaultConfig;
            Debug.Log(property.Name + "type = " + property.DeclaringType);
            property.SetValue(target, property.GetValue(source));
        }
    }

    void CalculateSingleOverrde(WandConfiguration currentOverride, SpellItemConfiguration itemConfiguration)
    {
        foreach (var item in itemConfiguration.WandSpellOverrides)
        {
            if (wandPropertyMap.TryGetValue(item.PropertyName, out var value))
            {
                object target = currentOverride;
                var split = item.PropertyName.Split('.');
                if (split.Length > 1 && split[0] == "projectile")
                {
                    target = currentOverride.projectileConfiguration;
                }
                switch (item.Operation)
                {
                    case WandSpellOverride.OperationType.Add:
                        if (value.FieldType.IsAssignableFrom(typeof(float)))
                        {
                            value.SetValue(target, (float)value.GetValue(target) + Convert.ToSingle(item.Value));
                        } 
                        else if (value.FieldType.IsAssignableFrom(typeof(int)))
                        {
                            value.SetValue(target, (int)value.GetValue(target) * Convert.ToInt32(item.Value));
                        }
                        break;
                    case WandSpellOverride.OperationType.Multiply:
                        if (value.FieldType.IsAssignableFrom(typeof(float)))
                        {
                            value.SetValue(target, (float)value.GetValue(target) * Convert.ToSingle(item.Value));
                        } else if (value.FieldType.IsAssignableFrom(typeof(int)))
                        {
                            value.SetValue(target, (int)value.GetValue(target) * Convert.ToInt32(item.Value));
                        }
                        break;
                    case WandSpellOverride.OperationType.Set:
                        if (value.FieldType.IsAssignableFrom(typeof(float)))
                        {
                            value.SetValue(target, Convert.ToSingle(item.Value));
                        } else if (value.FieldType.IsAssignableFrom(typeof(int)))
                        {
                            value.SetValue(target, Convert.ToInt32(item.Value));
                        }
                        break;
                }
            }
        }
    }
}
