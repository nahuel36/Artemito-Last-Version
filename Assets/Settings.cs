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
}
}
