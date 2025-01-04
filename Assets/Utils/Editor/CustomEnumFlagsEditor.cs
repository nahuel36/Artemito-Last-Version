using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito { 
public class CustomEnumFlagsEditor : MonoBehaviour
{
    

    public static void ShowUI(VisualElement vElem, CustomEnumFlagsField field, UnityEngine.Object myTarget, SerializedObject serializedObject)
    {
        List<string> flags = Settings.Instance.propertyVariables.GetVariablesNames();
        vElem.Q<EnumFlagsField>("flags").choices = flags;
        List<int> masks = new List<int>();
        for (int i = 0; i < flags.Count; i++) 
        {
            masks.Add(1 << i);
        }
        vElem.Q<EnumFlagsField>("flags").choicesMasks = masks;
        vElem.Q<EnumFlagsField>("flags").value = field.MembersToEnum(flags);
        vElem.Q<EnumFlagsField>("flags").RegisterValueChangedCallback(
        (evt) =>
        {
            field.EnumToMembers(flags,evt.newValue);
            EditorUtility.SetDirty(myTarget);
        });

        
    
    }

    public static void ShowAddMenu(List<Property> properties, ListView list, UnityEngine.Object myTarget, SerializedObject serializedObject) {
        GenericMenu menu = new GenericMenu();

        foreach (Type type in Settings.Instance.propertyVariables.GetVariablesTypes())
        {
            menu.AddItem(new GUIContent(Settings.Instance.propertyVariables.GetVariableName(type)), false, () =>
            {
                PropertyVariable newvariable = Settings.Instance.propertyVariables.GetVariableInstance(type);
                properties.Add(new Property(){ variables = new CustomEnumFlagsField() { members = new List<PropertyVariable> { newvariable } } });
                list.itemsSource = null;
                list.itemsSource = properties;
                list.Rebuild();
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(myTarget);
            });
        }
        menu.ShowAsContext();
    }
}
}
