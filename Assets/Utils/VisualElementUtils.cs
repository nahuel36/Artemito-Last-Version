using Unity.Properties;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito { 
public static class VisualElementUtils
{
    public static void HideVisualElement()
    {

    }
#if UNITY_EDITOR
    public static void SetBinding<FieldType,ObjectType>(VisualElement elem, string propertyPath, object dataSource, UnityEngine.Object target, SerializedObject serializedTarget) where FieldType:VisualElement where ObjectType:UnityEngine.Object
    {
        ((FieldType)elem).SetBinding("value", new DataBinding
        {
            dataSourcePath = new PropertyPath(propertyPath),
            dataSource = dataSource
        });

        ((FieldType)elem).RegisterCallback<ChangeEvent<ObjectType>>((evt) =>
        {
            EditorUtility.SetDirty(target);
            serializedTarget.ApplyModifiedProperties();
        });

    }
#endif
    public static void RegisterChangeAndSaveData()
    {

    }
}
}
