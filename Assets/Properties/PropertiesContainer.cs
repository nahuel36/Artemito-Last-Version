using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito { 
public interface PropertiesContainer 
{
    public void SetProperty(ref Property property,Property values);
    public Property GetProperty(PropertyData data, string name);

    public List<Property> GetAllProperties(PropertyData data);

#if UNITY_EDITOR
        public VisualElement PropertyInspectorField(UnityEngine.Object myTarget, SerializedObject serializedObject, PropertyData data, System.Action<PropertyData> onUpdateData);
#endif
    }
}