using UnityEngine;
using UnityEngine.UIElements;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Artemito { 

    [System.Serializable]
    public abstract class Interaction: ICommand
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

        Task ICommand.Execute()
        {
            throw new System.NotImplementedException();
        }

        public abstract void Skip();
    }
}
