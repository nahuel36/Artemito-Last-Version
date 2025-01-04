using UnityEngine;
using System.Collections.Generic;

namespace Artemito
{ 

[System.Serializable]
public class InventoryItem
{
    public string name;
    public Sprite image;
    public List<InteractionVerb> interactionsVerbs;
    public List<InventoryInteraction> inventoryInteractions;
    public List<Property> properties;
}
}
