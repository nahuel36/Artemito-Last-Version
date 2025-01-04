using Artemito;
using Unity.Properties;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito
{ 

[System.Serializable]
public abstract class CharacterInteraction : Interaction
{
    public Character character;

#if UNITY_EDITOR
    public override VisualElement InspectorField(UnityEngine.Object target, SerializedObject serializedTarget)
    {
        VisualElement element = base.InspectorField(target, serializedTarget);
        VisualElement field = VisualTreeAssets.Instance.characterInteraction.Instantiate();

        ObjectField characterField = (ObjectField)field.Q("character");
        characterField.value = character;

        characterField.RegisterValueChangedCallback((evt) =>
        {
            character = (Character)evt.newValue;
            EditorUtility.SetDirty(target);
            serializedTarget.ApplyModifiedProperties();
        });

        element.Add(field);



        return element;
    }
#endif

}
}
