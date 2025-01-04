using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace Artemito { 
public class DialogOptionCustomEditor
{
    public static VisualElement ShowUI(List<DialogOption> options, UnityEngine.Object myTarget, SerializedObject serializedObject)
    {
        VisualElement root = new VisualElement();

        ListView listView = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

        listView.itemsSource = options;

        listView.headerTitle = "options";

        listView.makeItem = () =>
        {
            VisualElement ve = new VisualElement();
            return ve;
        };

        listView.bindItem = (vElem, index) =>
        {
            vElem.Clear();

            if (options[index].interactions == null)
                options[index].interactions = new InteractionAttempsContainer();

            if (options[index].properties == null)
                options[index].properties = new List<Property>();

            vElem.Add(InteractionAttempContainerEditor.ShowUI(options[index].interactions, myTarget, serializedObject));
            vElem.Add(PropertiesCustomEditor.ShowUI(options[index].properties, myTarget, serializedObject));

        };


        listView.itemsAdded += new Action<IEnumerable<int>>((IEnumerable<int> k) =>
        {
            options[options.Count - 1] = new DialogOption();
            EditorUtility.SetDirty(myTarget);
            serializedObject.ApplyModifiedProperties();
        });

        

        root.Add(listView);
        return root;
    }
}
}
