using Artemito;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.Properties;

using UnityEngine.UIElements;
using UnityEditor;

namespace Artemito { 

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog", order = 1), System.Serializable]
public class Dialog : ScriptableObject, PropertiesContainer
{
    public List<Subdialog> subdialogs;

    public List<Property> GetAllProperties(PropertyData data)
    {
        if (data.intData1 == -1 || data.intData2 == -1) return null;
        
        return subdialogs[data.intData1].options[data.intData2].properties;
    }

    public Property GetProperty(PropertyData data, string name)
    {
        if (data.intData1 == -1 || data.intData2 == -1) return null;

        foreach (Property property in subdialogs[data.intData1].options[data.intData2].properties)
        {
            if (property.name == name)
                return property;
        }

        return null;
    }

    public VisualElement PropertyInspectorField(UnityEngine.Object myTarget, SerializedObject serializedObject,PropertyData data, System.Action<PropertyData> onUpdateData)
    {
        VisualElement root = new VisualElement();

        VisualElement element = VisualTreeAssets.Instance.dialogPropertyField.Instantiate();

        List<string> subdialogsStrings = new List<string>();
        for (int i = 0; i < subdialogs.Count; i++)
        {
            subdialogsStrings.Add(i.ToString());
        }
        
        element.Q<DropdownField>("subdialog").choices = subdialogsStrings;
        element.Q<DropdownField>("subdialog").value = data.intData1.ToString();
        element.Q<DropdownField>("subdialog").RegisterValueChangedCallback((evt) =>
        {
            data.intData1 = int.Parse(evt.newValue);
            EditorUtility.SetDirty(myTarget);
            serializedObject.ApplyModifiedProperties();
        });

        if (data.intData1 != -1)
        {
            List<string> optionsStrings = new List<string>();
            for (int i = 0; i < subdialogs[data.intData1].options.Count; i++)
            {
                optionsStrings.Add(i.ToString());
            }

            element.Q<DropdownField>("option").choices = optionsStrings;
            element.Q<DropdownField>("option").value = data.intData2.ToString();

            element.Q<DropdownField>("option").RegisterValueChangedCallback((evt) =>
            {
                data.intData2 = int.Parse(evt.newValue);
                onUpdateData(data);
                EditorUtility.SetDirty(myTarget);
                serializedObject.ApplyModifiedProperties();
            });
        }

        root.Add(element);

        return root;
    }

    public void SetProperty(ref Property property, Property values)
    {
        
    }
}

}
