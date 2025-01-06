using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using System.Collections.Generic;
using System.Threading.Tasks;


#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Artemito { 
public class CompareProperty : PropertyInteraction, ICommand
{
        public Property values;

        public override string Name => "Compare property";
#if UNITY_EDITOR
            public override VisualElement InspectorField(Object target, SerializedObject serializedTarget)
            {
                VisualElement root = base.InspectorField(target, serializedTarget);

                VisualElement element = VisualTreeAssets.Instance.compareProperty.CloneTree();

                if (values == null)
                    values = new Property();

                ((DropdownField)element.Q("type")).RegisterValueChangedCallback((evt) =>
                {
                    values.variables.StringToSingleMember(evt.newValue);
                    EditorUtility.SetDirty(target);
                });
                if(property != null && property.variables != null)
                    ((DropdownField)element.Q("type")).choices = property.variables.GetMembersList();
                if(values.variables != null)
                {
                    ((DropdownField)element.Q("type")).value = values.variables.GetSingleMemberString();

                    VisualElement variableValues = element.Q("value");
                    foreach (var variable in values.variables.members)
                    {
                        variableValues.Add(variable.PropertyValuesInspector(target, serializedTarget));
                    }
                }

                root.Add(element);

                return root;
            }
#endif

        async Task ICommand.Execute()
        {
            bool equals = false;
            foreach (var variable in property.variables.members)
            {
                if(variable.Name == values.variables.members[0].Name)
                {
                    if (variable.Equals(values.variables.members[0]))
                        equals = true;
                }
            }

            Conditional cond = new Conditional();
            cond.condition = equals;
            cond.actionIfTrue = Conditional.GetPropertyAction.Continue;
            cond.actionIfFalse = Conditional.GetPropertyAction.Stop;

            CommandsQueue.Instance.AddConditional(cond);

            await Task.Yield();
        }

        public override void Skip()
        {
            throw new System.NotImplementedException();
        }
    }
}
