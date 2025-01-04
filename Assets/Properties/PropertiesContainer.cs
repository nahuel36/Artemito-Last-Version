using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito { 
public interface PropertiesContainer 
{
    public void SetProperty(ref Property property,Property values);
    public Property GetProperty(PropertyData data, string name);

    public List<Property> GetAllProperties(PropertyData data);
    public VisualElement PropertyInspectorField(UnityEngine.Object myTarget, SerializedObject serializedObject, PropertyData data, System.Action<PropertyData> onUpdateData);

}
}