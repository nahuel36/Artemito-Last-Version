using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Artemito { 
public class SubdialogCustomEditor
{
    public static VisualElement ShowUI(List<Subdialog> subdialogs, UnityEngine.Object myTarget, SerializedObject serializedObject)
    {
        VisualElement root = new VisualElement();

        
        ListView listView = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

        listView.itemsSource = subdialogs;

        listView.headerTitle = "subdialogs";

        listView.makeItem = () =>
        {
            VisualElement ve = new VisualElement();
            return ve;
        };

        listView.bindItem = (vElem, index) =>
        {
            vElem.Clear();

            if (subdialogs[index].options == null)
                subdialogs[index].options = new System.Collections.Generic.List<DialogOption>();


            vElem.Add(DialogOptionCustomEditor.ShowUI(subdialogs[index].options, index, myTarget, serializedObject));

        };

        listView.itemsAdded += new Action<IEnumerable<int>>((IEnumerable<int> k) =>
        {
            Subdialog subdialog = new Subdialog();
            ((Dialog)myTarget).lastSubdialogID++;
            subdialog.id = ((Dialog)myTarget).lastSubdialogID; 
            subdialogs[subdialogs.Count - 1] = subdialog;
            EditorUtility.SetDirty(myTarget);
            serializedObject.ApplyModifiedProperties();
        });

        root.Add(listView);


        return root;

    }
}
}
