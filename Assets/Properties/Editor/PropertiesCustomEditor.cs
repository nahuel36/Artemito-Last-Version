using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito { 
public class PropertiesCustomEditor : Editor
{
    public static VisualElement ShowUI(List<Property> properties, UnityEngine.Object myTarget, SerializedObject serializedObject)
    {
        VisualElement root = new VisualElement();

        ListView list = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

        list.itemsSource = properties;

        list.headerTitle = "properties";

        list.makeItem = () =>
        {
            VisualElement vElem = new VisualElement();
            return vElem;
        };

        list.bindItem = (vElem, index) =>
        {
            vElem.Clear();
            vElem.Add(VisualTreeAssets.Instance.propertyItem.Instantiate());

            Property current_prop = properties[index];

            vElem.Q<TextField>("name").SetBinding("value", new DataBinding
                        {
                            dataSourcePath = new PropertyPath(nameof(current_prop.name)),
                            dataSource = current_prop
                        });

            vElem.Q<TextField>("name").RegisterValueChangedCallback(value =>
            {
                EditorUtility.SetDirty(myTarget); serializedObject.ApplyModifiedProperties();
            });

            if (properties[index].variables == null)
                properties[index].variables = new CustomEnumFlagsField();

            CustomEnumFlagsEditor.ShowUI(vElem, properties[index].variables, myTarget, serializedObject);

            foreach (var variable in properties[index].variables.members)
            {
                vElem.Add(variable.PropertyValuesInspector(myTarget,serializedObject));
            }
        };



        list.overridingAddButtonBehavior = (actual_list, actual_button) =>
        {
            CustomEnumFlagsEditor.ShowAddMenu(properties,list,myTarget, serializedObject);
        };


        root.Add(list);

        return root;

    }
    }

}
