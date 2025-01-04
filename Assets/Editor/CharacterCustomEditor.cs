using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Reflection;
using Unity.Properties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;


namespace Artemito { 
[CustomEditor(typeof(Character))]
public class CharacterCustomEditor : Editor
{



    private Interaction copiedItem;
    
    public override VisualElement CreateInspectorGUI()
    {
        Character myTarget = target as Character;
        VisualElement root = new VisualElement();

        if (myTarget.properties == null)
            myTarget.properties = new List<Property>();

        if (myTarget.verbs == null)
            myTarget.verbs = new List<InteractionVerb>();

        if (myTarget.inventoryInteractions == null)
            myTarget.inventoryInteractions = new List<InventoryInteraction>();

        root.Add(PropertiesCustomEditor.ShowUI(myTarget.properties, myTarget, serializedObject));
        root.Add(InteractionVerbCustomEditor.ShowUI(myTarget.verbs, myTarget, serializedObject));
        root.Add(InventoryInteractionCustomEditor.ShowUI(myTarget.inventoryInteractions, myTarget, serializedObject));

        return root;
    }

        private void OnMouseDown(MouseDownEvent evt, VisualElement listItem, int index, ListView list)
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
