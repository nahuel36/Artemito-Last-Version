using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

namespace Artemito
{ 
public class CharacterTalk : CharacterInteraction, ICommand
{

        public override string Name => "Character talk";
        public string[] messages;

        async Task ICommand.Execute()
        {
            IMessageTalker talker = character.messageTalker;

            StringBuilder sb = new StringBuilder();

            foreach (string message in messages)
            {
                sb.Append(message);
            }

            talker.Talk(sb.ToString(), true);
            while (talker.Talking)
                await Task.Yield();
        }

        public void UpdateMessages(VisualElement root, Object target)
        {
            for (int i=0; i<messages.Length;i++)
            {
                TextField text = new TextField("Message");

                text.value = messages[i];

                int index = i;

                text.RegisterValueChangedCallback((evt) =>
                {
                    messages[index] = evt.newValue;
                    EditorUtility.SetDirty(target);
                });

                root.Add(text);
            }
        }

        public override VisualElement InspectorField(Object target, SerializedObject serializedTarget)
        {
            VisualElement root = base.InspectorField(target, serializedTarget);

            UpdateMessages(root, target);

            Button addMessage = new Button(() =>
            {
                ArrayUtility.Add(ref messages, "");
                UpdateMessages(root, target);
                EditorUtility.SetDirty(target);
            });

            addMessage.text = "Add message";
            root.Add(addMessage);

            return root;
        }

        public override void Skip()
        {
            throw new System.NotImplementedException();
        }
    }
}
