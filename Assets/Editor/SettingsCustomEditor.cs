using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using System;
using UnityEditor.Search;

namespace Artemito
{ 
[CustomEditor(typeof(Settings))]
public class SettingsCustomEditor : Editor
{
    public override VisualElement CreateInspectorGUI() {
        VisualElement root = new VisualElement();

        Settings myTarget = (Settings)target;

        if (myTarget.interactionTypes == null)
            myTarget.interactionTypes = new InteractionTypes();

        if (myTarget.propertyVariables == null)
            myTarget.propertyVariables = new PropertyVariableTypes();

        if (myTarget.verbs == null)
            myTarget.verbs = new List<Verb>();

        root.Add(InteractionTypesEditor.ShowUI(myTarget.interactionTypes,myTarget, serializedObject));
        
        root.Add(PropertiesVariableTypesEditor.ShowUI(myTarget.propertyVariables,myTarget,serializedObject));

        root.Add(ShowVerbs(myTarget,serializedObject));

        root.Add(ShowInventory(myTarget, serializedObject));

        return root;      
    }

    private VisualElement ShowInventory(Settings myTarget, SerializedObject serializedObject)
    {
        ObjectField inventoryField = new ObjectField();
        inventoryField.objectType = typeof(Inventory);
        inventoryField.label = "Inventory";
        inventoryField.value = myTarget.inventory;
        inventoryField.RegisterValueChangedCallback(evt =>
        {
            myTarget.inventory = (Inventory)evt.newValue;
            EditorUtility.SetDirty(myTarget);
            serializedObject.ApplyModifiedProperties();
        });
        return inventoryField;
    }

    private VisualElement ShowVerbs(Settings myTarget, SerializedObject serializedObject)
    {
        ListView listView = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

        listView.itemsSource = myTarget.verbs;

        listView.makeItem = () =>
        {
            var label = new TextField();
            return label;
        };

        listView.bindItem = (element, index) =>
        {
            Verb verb = myTarget.verbs[index];
            (element as TextField).SetBinding("value", new DataBinding
            {
                dataSourcePath = new PropertyPath(nameof(verb.Name)),
                dataSource = verb
            });
            (element as TextField).RegisterValueChangedCallback(evt =>
            {
                EditorUtility.SetDirty(myTarget);
                serializedObject.ApplyModifiedProperties();
                //verb.Name = evt.newValue;
            });
        };

        listView.headerTitle = "Verbs";

            listView.itemsAdded += new Action<IEnumerable<int>>((IEnumerable<int> k) =>
            {
                Verb newverb = new Verb();
                myTarget.lastVerbID++;
                newverb.id = myTarget.lastVerbID;
                listView.itemsSource[listView.itemsSource.Count - 1] = newverb;
            });
            
        return listView;
    }
}
}
