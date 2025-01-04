using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Unity.Properties;
using System;

namespace Artemito { 
public class InventoryInteractionCustomEditor : Editor
{
    public static VisualElement ShowUI(List<InventoryInteraction> inventoryInteractions, UnityEngine.Object myTarget, SerializedObject serializedObject)
    {
        VisualElement root = new VisualElement();

        ListView listView = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

        listView.itemsSource = inventoryInteractions;

        listView.makeItem = () =>
        {
            VisualElement visualElement = new VisualElement();
            return visualElement;
        };

        listView.bindItem = (vElem, index) =>
        {
            vElem.Clear();
            DropdownField inventoryField = new DropdownField();
            InventoryInteraction inventoryInt = inventoryInteractions[index];
            inventoryField.value = Settings.Instance.inventory.GetIndexAndNameFromID(inventoryInt.inventoryID);
            inventoryField.RegisterValueChangedCallback(evt =>
            {
                inventoryInt.inventoryID = Settings.Instance.inventory.GetIDFromIndexAndName(evt.newValue);
                EditorUtility.SetDirty(myTarget);
                serializedObject.ApplyModifiedProperties();
            });
            for (int i = 0; i < Settings.Instance.inventory.items.Count; i++)
            {
                if (string.IsNullOrEmpty(Settings.Instance.inventory.items[i].name))
                    inventoryField.choices.Add((i+1).ToString()+":");
                else
                    inventoryField.choices.Add((i+1).ToString() + ":" + Settings.Instance.inventory.items[i].name);
            }

            vElem.Add(inventoryField);

            if (inventoryInt.interactions == null) 
                inventoryInt.interactions = new InteractionAttempsContainer();

            vElem.Add(InteractionAttempContainerEditor.ShowUI(inventoryInteractions[index].interactions, myTarget, serializedObject));
        };

        listView.headerTitle = "inventory";

        root.Add(listView);

        return root;

    }
}
}
