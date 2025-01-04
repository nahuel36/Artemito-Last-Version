using Artemito;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEditor;
using Unity.Properties;
using UnityEditor.UIElements;
using System;
using UnityEngine.TextCore.Text;
using Microsoft.SqlServer.Server;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace Artemito { 
public class InteractionCustomEditor : MonoBehaviour
{
    private static Interaction copiedItem;
    private static List<EventCallback<ChangeEvent<string>>> fieldUserCallback = new List<EventCallback<ChangeEvent<string>>>();

    public static VisualElement ShowUI(List<Interaction> interactions, UnityEngine.Object myTarget, SerializedObject myTargetSerialized)
    { 
        VisualElement root = new VisualElement();

        ListView list = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

        list.headerTitle = "interactions";

        list.makeItem = () =>
        {
            VisualElement make_elem = new VisualElement();
            return make_elem;
        };

        list.bindItem = (bind_elem, index) =>
        {
            var itemObj = interactions[index];

            bind_elem.Clear();

            bind_elem.Add(VisualTreeAssets.Instance.interactionItem.Instantiate());

            DropdownField dropdown = ((DropdownField)bind_elem.Q("dropdown"));
            dropdown.choices = Settings.Instance.interactionTypes.GetInteractionsNames();
            dropdown.value = Settings.Instance.interactionTypes.GetInteractionName(itemObj.GetType());

            dropdown.RegisterValueChangedCallback((evt) =>
            {
                Interaction inter = Settings.Instance.interactionTypes.GetInteractionInstanceFromName(evt.newValue);
                dropdown.value = Settings.Instance.interactionTypes.GetInteractionName(inter.GetType());
                interactions[index] = inter;
                EditorUtility.SetDirty(myTarget);
            });

            VisualElement typeContent = bind_elem.Q("typeContent");
            typeContent.Clear();
            typeContent.Add(itemObj.InspectorField(myTarget,myTargetSerialized));

            bind_elem.RegisterCallback<MouseDownEvent>(evt => OnMouseDown(evt, list, index, ((ListView)list)));
        };

        list.overridingAddButtonBehavior = (actual_list, actual_button) =>
        {
            GenericMenu menu = new GenericMenu();

            foreach(Type type in Settings.Instance.interactionTypes.GetInteractionTypes())
            { 
                // Agregar opciones al menú
                menu.AddItem(new GUIContent(Settings.Instance.interactionTypes.GetInteractionName(type)), false, () =>
                {
                    Interaction inter = Settings.Instance.interactionTypes.GetInteractionInstance(type);
                    interactions.Add(inter);
                    list.itemsSource = null;
                    list.itemsSource = interactions;
                    list.Rebuild();
                    myTargetSerialized.ApplyModifiedProperties();
                    EditorUtility.SetDirty(myTarget);
                });
            }
            // Mostrar el menú en la posición del mouse
            menu.ShowAsContext();
        };

        list.itemsSource = interactions;

        root.Add(list);

        return root;
    }



    private static void OnMouseDown(MouseDownEvent evt, VisualElement listItem, int index, ListView list)
    {
#if UNITY_EDITOR
        if (evt.button == 1)
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Copy " + typeof(Interaction).ToString()), false, () =>
            {
                copiedItem = (Interaction)list.itemsSource[index];
            });
            if (copiedItem != null)
            {
                genericMenu.AddItem(new GUIContent("Paste " + typeof(Interaction).ToString()), false, () =>
                {
                    list.itemsSource[index] = copiedItem.Copy(copiedItem);

                    list.Rebuild();
                });
            }
            genericMenu.AddItem(new GUIContent("Cancel"), false, () =>
            {
            });
            genericMenu.DropDown(new Rect(Event.current.mousePosition, Vector2.zero));
            evt.StopPropagation();
        }
#endif
    }
}
}
