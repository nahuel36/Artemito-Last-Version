using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Artemito { 
[System.Serializable] //,ExecuteInEditMode]
    public class Character : RoomInteractuable, PropertiesContainer
    {
        public IMessageTalker messageTalker;

        public VisualElement PropertyInspectorField(System.Action<PropertyData> onUpdateData)
        {
            return new VisualElement();
        }

        public void Execute()
        {
            foreach(Interaction interaction in verbs[0].interactions.attemps[0].interactions)
                CommandsQueue.Instance.AddCommand(interaction);
            //inventoryInteractions[0].interactions.attemps[0].interactions[0].Execute();
        }

        private void Start()
        {
            messageTalker = new LucasArtText(this.transform, new TextTimeCalculator());
        }


    }
}
