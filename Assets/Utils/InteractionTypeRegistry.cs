
using System;
using System.Linq;
using System.Collections.Generic;

namespace Artemito { 
public static class InteractionTypeRegistry
{
    public static List<Type> GetAllTypes()
    {
        // Obtener todas las clases derivadas de `Forma` en el ensamblado actual
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && !type.IsAbstract && typeof(Artemito.Interaction).IsAssignableFrom(type))
            .ToList();
    }
}
}
