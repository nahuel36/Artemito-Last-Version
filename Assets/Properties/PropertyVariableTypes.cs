using Artemito;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Artemito
{ 


[System.Serializable]
public class PropertyVariableTypes 
{
    [System.Serializable]
    public class VariableType
    {
        public string qualifiedName;
        public string showName;
    }

    [SerializeField] private List<VariableType> types;

    public void AddVariableType(Type type)
    {
        if (!ContainsVariableType(type))
        {
            types.Add(new VariableType() { qualifiedName = type.AssemblyQualifiedName, showName = GetVariableInstance(type).Name });
        }
    }
    public List<Type> GetVariablesTypes()
    {
        List<Type> types = new List<Type>();
        foreach (VariableType variable in this.types)
        {
            Type type = Type.GetType(variable.qualifiedName);
            if (type != null)
            {
                types.Add(type);
            }
            else
            {
                Debug.LogWarning($"El variable {variable} no se encontró.");
            }
        }
        return types;
    }

    public bool ContainsVariableType(Type type)
    {
        bool contains = false;

        foreach (VariableType variable in types)
        {
            if (variable.qualifiedName == type.AssemblyQualifiedName)
                contains = true;
        }

        return contains;
    }

    public List<string> GetVariablesNames()
    {
        List<string> names = new List<string>();

        foreach (VariableType nombreTipo in types)
        {
            names.Add(nombreTipo.showName);
        }

        return names;
    }

    public string GetVariableName(Type type)
    {
        foreach (VariableType variable in types)
        {
            if (type.AssemblyQualifiedName == variable.qualifiedName)
                return variable.showName;
        }
        return null;
    }


    public void RemoveVariableType(Type type)
    {
        if (ContainsVariableType(type))
        {
            types.Remove(new VariableType() { qualifiedName = type.AssemblyQualifiedName, showName = GetVariableInstance(type).Name });
        }
    }
    public PropertyVariable GetVariableInstance(Type type)
    {
        if (typeof(PropertyVariable).IsAssignableFrom(type))
        {
            return (PropertyVariable)Activator.CreateInstance(type);
        }
        else
        {
            Debug.LogError($"El variable {type.Name} no es una subclase de types.");
            return null;
        }
    }

    public PropertyVariable GetVariableInstanceFromName(string name)
    {
        string qualifiedName = null;
        foreach (VariableType variable in types)
        {
            if (variable.showName == name)
            {
                qualifiedName = variable.qualifiedName;
            }
        }
        if (qualifiedName == null) return null;

        Type type = Type.GetType(qualifiedName);

        if (typeof(PropertyVariable).IsAssignableFrom(type))
        {
            return (PropertyVariable)Activator.CreateInstance(type);
        }
        else
        {
            Debug.LogError($"El variable {type.Name} no es una subclase de types.");
            return null;
        }
    }
}
}
