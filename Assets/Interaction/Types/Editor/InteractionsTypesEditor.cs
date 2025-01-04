using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using UnityEngine.UIElements;
using System.Numerics;
using Microsoft.SqlServer.Server;

namespace Artemito
{ 
public class InteractionTypesEditor : Editor
{
    //implementar remove

    public static VisualElement ShowUI(InteractionTypes types, UnityEngine.Object target, SerializedObject serializedObject)
    {
        VisualElement root = new VisualElement();

        if (types.GetInteractionTypes() != null)
        {
            ListView listView = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

            listView.itemsSource = types.GetInteractionTypes();

            listView.overridingAddButtonBehavior = (actual_list_view, actual_button) =>
            {
                GenericMenu menu = new GenericMenu();

                foreach(var actual_type in InteractionTypeRegistry.GetAllTypes()) 
                {
                    // Agregar opciones al menú
                    menu.AddItem(new GUIContent(actual_type.Name), false, () =>
                    {
                        types.AddInteractionType(actual_type);
                        listView.Rebuild();
                        listView.itemsSource = null;
                        listView.itemsSource = types.GetInteractionTypes();
                        EditorUtility.SetDirty(target);
                        serializedObject.ApplyModifiedProperties();
                    });
                }
                // Mostrar el menú en la posición del mouse
                menu.ShowAsContext(); 
            };

            listView.headerTitle = "Interaction types";

            root.Add(listView);

        }
        return root;
    }
}
}