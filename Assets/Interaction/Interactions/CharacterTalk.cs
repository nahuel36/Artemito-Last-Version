using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito
{ 
public class CharacterTalk : CharacterInteraction, ICommand
{

        public override string Name => "Character talk";
        public string message = "";

        async Task ICommand.Execute()
        {
            IMessageTalker talker = character.messageTalker;
            talker.Talk(message, true);
            while (talker.Talking)
                await Task.Yield();
        }

        public override VisualElement InspectorField(Object target, SerializedObject serializedTarget)
        {
            VisualElement root = base.InspectorField(target, serializedTarget);

            TextField text = new TextField("Message");
            text.value = message;
            text.RegisterValueChangedCallback((evt) =>
            {
                message = evt.newValue;
                EditorUtility.SetDirty(target);
            });

            root.Add(text);
            return root;
        }

        public override void Skip()
        {
            throw new System.NotImplementedException();
        }
    }
}
