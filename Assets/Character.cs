using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Artemito { 
[System.Serializable] //,ExecuteInEditMode]
    public class Character : RoomInteractuable, PropertiesContainer
    {
        public string Name;
        public VisualElement PropertyInspectorField(System.Action<PropertyData> onUpdateData)
        {
            return new VisualElement();
        }

        public void Execute()
        {
            //verbs[0].interactions.attemps[0].interactions[0].Execute();
            inventoryInteractions[0].interactions.attemps[0].interactions[0].Execute();
        }
    }
}
