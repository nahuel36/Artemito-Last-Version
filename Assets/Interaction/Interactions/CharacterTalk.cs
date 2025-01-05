using System.Threading.Tasks;
using UnityEngine;

namespace Artemito
{ 
public class CharacterTalk : CharacterInteraction, ICommand
{
        IMessageTalker talker;

        public override string Name => "Character talk";
        public string message = "hola mundo";
        public void Skip()
        {
            throw new System.NotImplementedException();
        }

        async Task ICommand.Execute()
        {
            talker = character.messageTalker;
            talker.Talk("hola mundo", true);
            while (talker.Talking)
                await Task.Yield();
        }
    }
}
