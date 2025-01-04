using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace Artemito
{
    [System.Serializable]
    public class ChangeProperty : PropertyInteraction
    {
        public override string Name => "Change property";

        public UnityEngine.Object copyContainer;
        public Property values;
#if UNITY_EDITOR
        public override VisualElement InspectorField(UnityEngine.Object target,SerializedObject serializedTarget)
        {
            VisualElement visualElement = base.InspectorField(target, serializedTarget);

            VisualElement field = VisualTreeAssets.Instance.changeProperty.Instantiate();

            visualElement.Add(field);

            if (property != null) {
                if (values == null)
                    values = new Property();

                EnumFlagsField valuesFlags = field.Q<EnumFlagsField>("flags");
                valuesFlags.choices = property.variables.GetMembersList();
                valuesFlags.value = values.variables.MembersToEnum(valuesFlags.choices);
                List<int> masks = new List<int>();
                for (int i = 0;i < valuesFlags.choices.Count;i++)
                {
                    masks.Add(1 << i);
                }
                valuesFlags.choicesMasks = masks;
                valuesFlags.RegisterValueChangedCallback((evt) =>
                {
                    values.variables.EnumToMembers(valuesFlags.choices, evt.newValue);
                    EditorUtility.SetDirty(target);
                });

                VisualElement variableValues = field.Q("values");
                foreach (var variable in values.variables.members)
                {
                    variableValues.Add(variable.PropertyValuesInspector(target, serializedTarget));
                }
                
            }

            return visualElement;
        }
#endif

        public override void Execute()
        {
            for(int i=0;i< values.variables.members.Count;i++) 
            {
                for (int j = 0;j< property.variables.members.Count;j++)
                {
                    if (property.variables.members[j].Name == values.variables.members[i].Name)
                    {
                        property.variables.members[j].CopyValues(values.variables.members[i]);
                    }
                }
            }
        }


        public override Interaction Copy(Interaction inter)
        { 
            ChangeProperty characterChangeProperty = new ChangeProperty();
            characterChangeProperty.propertyContainer = propertyContainer;
            return characterChangeProperty;
        }
    }
}