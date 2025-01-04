using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Artemito { 

[System.Serializable]
public class IntegerProperty : PropertyVariable
{
    [SerializeField] int value;
    public override string Name => "integer";
#if UNITY_EDITOR
    public override VisualElement PropertyValuesInspector(UnityEngine.Object myTarget,SerializedObject serializedObject)
    {
        VisualElement root = new VisualElement();

        IntegerField field = new IntegerField();

        field.label = "Integer value";

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
        value = ((IntegerProperty)valuesContainer).value;
    }
}
}