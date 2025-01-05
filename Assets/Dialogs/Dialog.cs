using Artemito;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.Properties;

using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Artemito { 

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog", order = 1), System.Serializable]
public class Dialog : ScriptableObject, PropertiesContainer
{
    public List<Subdialog> subdialogs;
        public int lastSubdialogID = 0;
    public List<Property> GetAllProperties(PropertyData data)
    {
        if (data.intData1 == -1 || data.intData2 == -1) return null;
        
        return subdialogs[int.Parse(GetSubdialogIndexAndNameFromID(data.intData1).Split(":")[0])-1].options[int.Parse(GetOptionIndexAndNameFromID(data.intData2, data.intData1).Split(":")[0])-1].properties;
    }

    public Property GetProperty(PropertyData data, string name)
    {
        if (data.intData1 == -1 || data.intData2 == -1) return null;

        foreach (Property property in subdialogs[int.Parse(GetSubdialogIndexAndNameFromID(data.intData1).Split(":")[0])-1].options[int.Parse(GetOptionIndexAndNameFromID(data.intData2, data.intData1).Split(":")[0])-1].properties)
        {
            if (property.name == name)
                return property;
        }

        return null;
    }
#if UNITY_EDITOR
        public VisualElement PropertyInspectorField(UnityEngine.Object myTarget, SerializedObject serializedObject,PropertyData data, System.Action<PropertyData> onUpdateData)
    {
        VisualElement root = new VisualElement();

        VisualElement element = VisualTreeAssets.Instance.dialogPropertyField.Instantiate();

        List<string> subdialogsStrings = new List<string>();
        for (int i = 0; i < subdialogs.Count; i++)
        {
            subdialogsStrings.Add((i+1).ToString()+":"+ subdialogs[i].name);
        }
        
        element.Q<DropdownField>("subdialog").choices = subdialogsStrings;
        element.Q<DropdownField>("subdialog").value = GetSubdialogIndexAndNameFromID(data.intData1);
        element.Q<DropdownField>("subdialog").RegisterValueChangedCallback((evt) =>
        {
            data.intData1 = GetSubdialogIDFromIndexAndName(evt.newValue);
            EditorUtility.SetDirty(myTarget);
            serializedObject.ApplyModifiedProperties();
        });

        if (data.intData1 != -1)
        {
            List<string> optionsStrings = new List<string>();
            for (int i = 0; i < subdialogs[data.intData1-1].options.Count; i++)
            {
                optionsStrings.Add((i+1).ToString());
            }

            element.Q<DropdownField>("option").choices = optionsStrings;
            element.Q<DropdownField>("option").value = GetOptionIndexAndNameFromID(data.intData2, data.intData1);

            element.Q<DropdownField>("option").RegisterValueChangedCallback((evt) =>
            {
                data.intData2 = GetOptionIDFromIndexAndName(evt.newValue, data.intData1);
                onUpdateData(data);
                EditorUtility.SetDirty(myTarget);
                serializedObject.ApplyModifiedProperties();
            });
        }

        root.Add(element);

        return root;
    }
#endif
        public void SetProperty(ref Property property, Property values)
    {
        
    }

        public int GetSubdialogIDFromIndexAndName(string name)
        {
            name = name.Split(':')[0];
            return subdialogs[int.Parse(name) - 1].id;
        }

        public string GetSubdialogIndexAndNameFromID(int id)
        {
            foreach (Subdialog subdialog in subdialogs)
            {
                if (subdialog.id == id)
                    return (subdialogs.IndexOf(subdialog)+1).ToString() + ":" +  subdialog.name;
            }

            return "";
        }

        public int GetOptionIDFromIndexAndName(string name, int subdialogID)
        {
            name = name.Split(':')[0];

            string subdialog = GetSubdialogIndexAndNameFromID(subdialogID);
            subdialog = subdialog.Split(':')[0];

            return subdialogs[int.Parse(subdialog)-1].options[int.Parse(name) - 1].id;
        }

        public string GetOptionIndexAndNameFromID(int id, int subdialogID)
        {
            foreach (Subdialog subdialog in subdialogs)
            {
                if (subdialog.id == subdialogID)
                { 
                    foreach(DialogOption option in subdialog.options)
                    {
                        if (option.id == id)
                            return (subdialog.options.IndexOf(option) + 1).ToString() + ":" + option.name;
                    
                    }
                }
            }

            return "";
        }
    }

}
