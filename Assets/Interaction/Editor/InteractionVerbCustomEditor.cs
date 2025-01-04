using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.Properties;
using System.Collections.Generic;

namespace Artemito { 
public class InteractionVerbCustomEditor : Editor
{
    public static VisualElement ShowUI(List<InteractionVerb> verbs, UnityEngine.Object myTarget, SerializedObject serializedObject)
    {
        VisualElement root = new VisualElement();

        ListView listView = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

        listView.itemsSource = verbs;

        listView.makeItem = () =>
        {
            VisualElement visualElement = new VisualElement();
            return visualElement;
        };

        listView.bindItem = (vElem, index) =>
        {
            vElem.Clear();
            DropdownField verbField = new DropdownField();
            InteractionVerb verb = verbs[index];

            verbField.RegisterValueChangedCallback(evt =>
            {
                verb.verbID = Settings.Instance.GetVerbIDFromName(evt.newValue);
                EditorUtility.SetDirty(myTarget);
                serializedObject.ApplyModifiedProperties();
            });
            verbField.value = Settings.Instance.GetVerbNameFromID(verb.verbID);
            Settings.Instance.verbs.ForEach(v =>
            {
                verbField.choices.Add(v.Name);
            });
            vElem.Add(verbField);
            
            if (verbs[index].interactions == null)
                verbs[index].interactions = new InteractionAttempsContainer();

            vElem.Add(InteractionAttempContainerEditor.ShowUI(verbs[index].interactions, myTarget, serializedObject));
        };

        listView.headerTitle = "verbs";

        root.Add(listView);

        return root;
    }
}
}
