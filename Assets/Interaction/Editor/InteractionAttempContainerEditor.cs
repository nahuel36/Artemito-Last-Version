using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

namespace Artemito {
public class InteractionAttempContainerEditor : MonoBehaviour
{
    public static VisualElement ShowUI(InteractionAttempsContainer container, UnityEngine.Object myTarget, SerializedObject myTargetSerialized)
    {
        VisualElement root = new VisualElement();

        if (container.attemps == null)
            container.attemps = new System.Collections.Generic.List<InteractionAttemp>();

        root.Add(InteractionAttempCustomEditor.ShowUI(container.attemps,myTarget, myTargetSerialized));

        return root;
    }
}
}