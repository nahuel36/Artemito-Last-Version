using UnityEngine;
using UnityEngine.UIElements;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Artemito
{ 

[System.Serializable]
public abstract class PropertyVariable: IComparable,IEquatable<PropertyVariable>
{
    public virtual string Name { get; }

#if UNITY_EDITOR
    public virtual VisualElement PropertyValuesInspector(UnityEngine.Object myTarget, SerializedObject serializedObject)
    {
        return new VisualElement();
    }
#endif
    public virtual bool Contains(PropertyVariable var)
    {
        return false;
    }
    public abstract int CompareTo(object obj);
    public abstract bool Equals(PropertyVariable other);
    public abstract void CopyValues(PropertyVariable valuesContainer);
}
}
