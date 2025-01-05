using System.Collections.Generic;
using UnityEngine;

namespace Artemito
{ 
[System.Serializable]
public class DialogOption
{
    public string name;
        public int id;
    public InteractionAttempsContainer interactions;
    public List<Property> properties;
}
}
