using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.UIElements;
using Unity.Properties;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using Unity.VisualScripting;

namespace Artemito {

    [CustomEditor(typeof(Inventory)), System.Serializable]
    public class InventoryCustomEditor : Editor
    {
        private class ItemData {
            public int id;
            public Button button;
        }
        private List<ItemData> itemsButtonsList = new List<ItemData>();
        private int itemSize = 75;
        private Button selectedButton;

        public override VisualElement CreateInspectorGUI()
        {
            Inventory myTarget = (Inventory)target;

            VisualElement root = VisualTreeAssets.Instance.inventoryView.Instantiate();

            root.Q<VisualElement>("selected").enabledSelf = false;

            Button addButton = root.Q<Button>("add");
            addButton.clicked += () =>
            {
                myTarget.lastID++;
                InventoryItem item = new InventoryItem();
                item.id = myTarget.lastID;
                myTarget.items.Add(item);
                UpdateItems(root);
            };

            VisualElement itemsView = root.Q<VisualElement>("items");
            itemsView.Clear();
            itemsButtonsList.Clear();
            UpdateItems(root);


            return root;
        }

        private void UpdateItems(VisualElement root)
        {
            Inventory myTarget = (Inventory)target;

            VisualElement itemsView = root.Q<VisualElement>("items");

            for (int j = 0; j < myTarget.items.Count; j++)
            {
                InventoryItem item = myTarget.items[j];
                if (itemsButtonsList.FindAll(delegate (ItemData data) { return data.id == item.id; }).Count == 0)
                {
                    Button button = new Button();
                    button.text = j.ToString();

                    if (!string.IsNullOrEmpty(item.name))
                    {
                        button.text = myTarget.items[j].name;
                    }
                    if (item.image != null)
                    {
                        button.style.backgroundImage = new StyleBackground(item.image);
                    }

                    button.AddToClassList("button");
                    button.clicked += () =>
                    {
                        if (selectedButton != null)
                        {
                            selectedButton.RemoveFromClassList("selected");
                        }
                        selectedButton = button;
                        button.AddToClassList("selected");
                        ShowSelectedPanel(root, item);
                    };

                    itemsView.Add(button);
                    itemsButtonsList.Add(new ItemData() { button = button, id = item.id });


                }   
            }
        }

        private void ShowSelectedPanel(VisualElement root, InventoryItem item)
        {
            // Obtén el panel de selección y actívalo
            var selectedPanel = root.Q<VisualElement>("selected");
            selectedPanel.enabledSelf = true;

            ResetSelectedPanel(root);

            // Configura el campo de texto
            var nameField = selectedPanel.Q<TextField>("name");
            nameField.value = item.name; // Asigna el valor actual

            nameFieldUserCallback = (evt) =>
            {
                item.name = evt.newValue; // Actualiza el nombre del modelo
                UpdateItems(root); // Refresca la lista de botones
            };
            nameField.RegisterValueChangedCallback(nameFieldUserCallback);

            // Configura el campo de imagen
            var spriteField = selectedPanel.Q<ObjectField>("sprite");
            spriteField.value = item.image; // Asigna el valor actual

            spriteFieldUserCallback = (evt) =>
            {
                item.image = (Sprite)evt.newValue; // Actualiza la imagen del modelo
                UpdateItems(root); // Refresca la lista de botones
            };

            spriteField.RegisterValueChangedCallback(spriteFieldUserCallback);

            var interactionsPanel = root.Q<VisualElement>("selected").Q<VisualElement>("interactions");

            interactionsPanel.Clear();

            if (item.interactionsVerbs == null)
                item.interactionsVerbs = new List<InteractionVerb>();

            if (item.inventoryInteractions == null)
                item.inventoryInteractions = new List<InventoryInteraction>();

            if(item.properties == null)
                item.properties = new List<Property>();

            interactionsPanel.Add(InteractionVerbCustomEditor.ShowUI(item.interactionsVerbs, (Inventory)target, serializedObject));
            interactionsPanel.Add(InventoryInteractionCustomEditor.ShowUI(item.inventoryInteractions, (Inventory)target, serializedObject));
            interactionsPanel.Add(PropertiesCustomEditor.ShowUI(item.properties, (Inventory)target, serializedObject));
        }

        // Almacena las referencias a los callbacks para poder desregistrarlos
        private EventCallback<ChangeEvent<string>> nameFieldUserCallback;
        private EventCallback<ChangeEvent<UnityEngine.Object>> spriteFieldUserCallback;
        private void ResetSelectedPanel(VisualElement root)
        {
            var selectedPanel = root.Q<VisualElement>("selected");

            // Limpia los callbacks y valores previos de los campos
            var nameField = selectedPanel.Q<TextField>("name");
            if(nameFieldUserCallback!=null)
                nameField.UnregisterValueChangedCallback(nameFieldUserCallback); // Limpia todos los callbacks previos
            nameField.value = ""; // Resetea el valor

            var spriteField = selectedPanel.Q<ObjectField>("sprite");
            if (spriteFieldUserCallback != null)
                spriteField.UnregisterValueChangedCallback(spriteFieldUserCallback); // Limpia todos los callbacks previos
            spriteField.value = null; // Resetea el valor
        }
    }
}
