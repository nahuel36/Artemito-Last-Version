using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito
{ 
public class CharacterTalk : CharacterInteraction, ICommand
{
        IMessageTalker talker;

        public override string Name => "Character talk";
        public string message = "hola mundo";

        async Task ICommand.Execute()
        {
            talker = character.messageTalker;
            talker.Talk("hola mundo", true);
            while (talker.Talking)
                await Task.Yield();
        }

        public override VisualElement InspectorField(Object target, SerializedObject serializedTarget)
        {
            VisualElement root = base.InspectorField(target, serializedTarget);



            return root;
        }

        public override void Skip()
        {
            throw new System.NotImplementedException();
        }
    }
}
