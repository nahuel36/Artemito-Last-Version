using Artemito;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Artemito { 

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
public class Settings : ScriptableObject
{
    [SerializeField] public InteractionTypes interactionTypes;
    [SerializeField] public PropertyVariableTypes propertyVariables;
    [SerializeField] public List<Verb> verbs;
        [SerializeField] public int lastVerbID = 0;
    [SerializeField] public Inventory inventory;
    public static Settings Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load("Settings") as Settings;
            return _instance;
        }
    }

    private static Settings _instance;

        public int GetVerbIDFromName(string name) 
        { 
            foreach (Verb verb in verbs)
            {
                if (verb.Name == name)
                    return verb.id;
            }
            return -1;
        }

        public string GetVerbNameFromID(int id) 
        {
            foreach (Verb verb in verbs)
            {
                if (verb.id == id)
                    return verb.Name;
            }
            return null;
        }
}
}
