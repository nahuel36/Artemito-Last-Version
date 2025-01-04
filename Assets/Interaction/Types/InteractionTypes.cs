using Artemito;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Artemito
{ 
[System.Serializable]
public class InteractionTypes
{
    [System.Serializable]
    public class InteractionType
    {
        public string qualifiedName;
        public string showName;
    }

    [SerializeField] private List<InteractionType> interactions = new List<InteractionType>();
    
    public void AddInteractionType(Type type)
    {
        if (!ContainsInteractionType(type))
        {
            interactions.Add(new InteractionType() { qualifiedName = type.AssemblyQualifiedName, showName = GetInteractionInstance(type).Name });
        }
    }
    public List<Type> GetInteractionTypes()
    {
        List<Type> types = new List<Type>();
        foreach (InteractionType interaction in interactions)
        {
            Type type = Type.GetType(interaction.qualifiedName);
            if (type != null)
            {
                types.Add(type);
            }
            else
            {
                Debug.LogWarning($"El interaction {interaction} no se encontró.");
            }
        }
        return types;
    }

    public bool ContainsInteractionType(Type type)
    {
        bool contains = false;

        foreach (InteractionType interaction in interactions)
        {
            if (interaction.qualifiedName == type.AssemblyQualifiedName)
                contains = true;
        }

        return contains;
    }

    public List<string> GetInteractionsNames()
    {
        List<string> names = new List<string>();

        foreach (InteractionType nombreTipo in interactions)
        {
            names.Add(nombreTipo.showName);
        }

        return names;
    }

    public string GetInteractionName(Type type)
    {
        foreach (InteractionType interaction in interactions)
        {
            if (type.AssemblyQualifiedName == interaction.qualifiedName)
                return interaction.showName;
        }
        return null;
    }


    public void RemoveInteractionType(Type type)
    {
        if (ContainsInteractionType(type))
        {
            interactions.Remove(new InteractionType() { qualifiedName = type.AssemblyQualifiedName, showName = GetInteractionInstance(type).Name });
        }
    }
    public Interaction GetInteractionInstance(Type type)
    {
        if (typeof(Interaction).IsAssignableFrom(type))
        {
            return (Interaction)Activator.CreateInstance(type);
        }
        else
        {
            Debug.LogError($"El interaction {type.Name} no es una subclase de Forma.");
            return null;
        }
    }

    public Interaction GetInteractionInstanceFromName(string name)
    {
        string qualifiedName = null;
        foreach (InteractionType interaction in interactions)
        {
            if (interaction.showName == name)
            {
                qualifiedName = interaction.qualifiedName;
            }
        }
        if (qualifiedName == null) return null;

        Type type = Type.GetType(qualifiedName);

        if (typeof(Interaction).IsAssignableFrom(type))
        {
            return (Interaction)Activator.CreateInstance(type);
        }
        else
        {
            Debug.LogError($"El interaction {type.Name} no es una subclase de Forma.");
            return null;
        }
    }
}
}
