using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito {
    [System.Serializable]
    public class RoomInteractuable : RoomPropertyContainer
    {
        public List<InteractionVerb> verbs;
        public List<InventoryInteraction> inventoryInteractions;

    }
}
