using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Artemito { 

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory"),System.Serializable]
public class Inventory : ScriptableObject, PropertiesContainer
{
    public List<InventoryItem> items;
    public int lastID = 0;
    
    public int GetIDFromIndexAndName(string index)
    {
            index = index.Split(':')[0];

            return items[int.Parse(index)-1].id; ;
    }   

    public string GetIndexAndNameFromID(int id)
    {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].id == id)
                {
                    return (i+1).ToString() + ":" + items[i].name;
                }
            }

            return null;
    }

    public List<Property> GetAllProperties(PropertyData data)
    {
        return items[data.intData1].properties;
    }

    public Property GetProperty(PropertyData data, string name)
    {
        foreach(Property property in items[data.intData1].properties)
        {
            if (property.name == name)
            {
                return property;
            }
        }
        return null;
    }

#if UNITY_EDITOR
    public VisualElement PropertyInspectorField(UnityEngine.Object myTarget, SerializedObject serializedObject, PropertyData data, System.Action<PropertyData> onUpdateData)
    {

        VisualElement root = new VisualElement();

        VisualElement element = VisualTreeAssets.Instance.inventoryPropertyField.Instantiate();

        List<string> itemsStrings = new List<string>();
        for (int i = 0; i < items.Count; i++)
        {
            itemsStrings.Add(i.ToString());
        }

        element.Q<DropdownField>("inventory").choices = itemsStrings;
        element.Q<DropdownField>("inventory").value = data.intData1.ToString();
        element.Q<DropdownField>("inventory").RegisterValueChangedCallback((evt) =>
        {
            data.intData1 = int.Parse(evt.newValue);
            onUpdateData(data);
            EditorUtility.SetDirty(myTarget);
            serializedObject.ApplyModifiedProperties();
        });

        root.Add(element);

        return root;
    }
#endif
    public void SetProperty(ref Property property, Property values)
    {
        
    }
}
}
