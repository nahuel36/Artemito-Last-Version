using Unity.Properties;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito { 

[System.Serializable]
public class StringProperty : PropertyVariable
{
    public string value;
    public override string Name => "string";

#if UNITY_EDITOR
    public override VisualElement PropertyValuesInspector(UnityEngine.Object myTarget, SerializedObject serializedObject)
    {
        VisualElement root = new VisualElement();

        TextField field = new TextField();

        field.label = "String value";

        field.SetBinding("value", new DataBinding
        {
            dataSourcePath = new PropertyPath(nameof(this.value)),
            dataSource = this
        });

        field.RegisterValueChangedCallback((evt) =>
        {
            EditorUtility.SetDirty(myTarget);
            serializedObject.ApplyModifiedProperties();
        }
        );

        root.Add(field);

        return root;
    }
#endif

    public override int CompareTo(object obj)
    {
        throw new System.NotImplementedException();
    }

    public override bool Equals(PropertyVariable other)
    {
        throw new System.NotImplementedException();
    }

    public override void CopyValues(PropertyVariable valuesContainer)
    {
        value = ((StringProperty)valuesContainer).value;
    }
}
}

