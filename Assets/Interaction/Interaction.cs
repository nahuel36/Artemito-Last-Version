using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Artemito { 

    [System.Serializable]
    public abstract class Interaction
    {
        public virtual string Name { get; }
        public virtual Interaction Copy(Interaction inter) {
                return inter;
        }

#if UNITY_EDITOR
        public virtual VisualElement InspectorField(UnityEngine.Object target, SerializedObject serializedTarget) 
        {  
            return new VisualElement(); 
        }
#endif
        public virtual void Execute() 
        { 
        
        }
    }
}
