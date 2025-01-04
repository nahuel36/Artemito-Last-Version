using Artemito;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito { 

[System.Serializable]
public class RoomPropertyContainer : MonoBehaviour, PropertiesContainer
{
    [SerializeField] public List<Property> properties;

    public List<Property> GetAllProperties(PropertyData data)
    {
        return properties;
    }

    public Property GetProperty(PropertyData data, string name)
    {
        foreach (Property property in GetAllProperties(data))
        {
            if (property.name == name)
                return property;
        }
        return null;
    }

    public VisualElement PropertyInspectorField(UnityEngine.Object myTarget, SerializedObject serializedObject, PropertyData data, System.Action<PropertyData> onUpdateData)
    {
        return new VisualElement();
    }

    public void SetProperty(ref Property property, Property values)
    {
        
    }
}
}
