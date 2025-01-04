using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
using UnityEditor;

namespace Artemito { 
public class InteractionAttempCustomEditor : MonoBehaviour
{
    public static VisualElement ShowUI(List<InteractionAttemp> attemps, UnityEngine.Object myTarget, SerializedObject myTargetSerialized)
    {
        VisualElement root = new VisualElement();
        
        ListView list = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();


        list.makeItem = () =>
        {
            VisualElement make_elem = new VisualElement();
            return make_elem;
        };

        list.bindItem = (bind_elem,index) =>
        {
            bind_elem.Clear();

            if (attemps[index].interactions == null)
                attemps[index].interactions = new List<Artemito.Interaction>();

            bind_elem.Add(InteractionCustomEditor.ShowUI(attemps[index].interactions, myTarget, myTargetSerialized));
        };

        list.itemsSource = attemps;

        list.headerTitle = "attemps";

        list.itemsAdded += new Action<IEnumerable<int>>((IEnumerable<int> k) =>
        {
            attemps[attemps.Count-1] = new InteractionAttemp();
            EditorUtility.SetDirty(myTarget);
            myTargetSerialized.ApplyModifiedProperties();
        });


        /*
        list.onAdd = (actual_list) =>
        {
            attemps.Add(new InteractionAttemp());
        };
        */
        root.Add(list);

        return root;

    }
}
}
