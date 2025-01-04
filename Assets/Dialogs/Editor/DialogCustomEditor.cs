using Artemito;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;

namespace Artemito
{ 
[CustomEditor(typeof(Dialog))]
public class DialogCustomEditor : Editor
{
    private Interaction copiedItem;

    public override VisualElement CreateInspectorGUI()
    {
        Dialog myTarget = target as Dialog;
        VisualElement root = new VisualElement();

        if (myTarget.subdialogs == null)
            myTarget.subdialogs = new List<Subdialog>();

        root.Add(SubdialogCustomEditor.ShowUI(myTarget.subdialogs, myTarget, serializedObject));

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
