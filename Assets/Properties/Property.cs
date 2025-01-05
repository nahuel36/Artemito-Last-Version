using System.Collections.Generic;
using UnityEngine;

namespace Artemito
{ 
    [System.Serializable]
    public class Property 
    {
        public int id;
        public string name;
        public CustomEnumFlagsField variables;

        public static int GetIDFromName(List<Property> properties, string name)
        {
            foreach (Property property in properties)
            {
                if (property.name == name)
                    return property.id;
            }
            return -1;
        }

        public static string GetNameFromID(List<Property> properties, int id)
        {
            foreach (Property property in properties)
            {
                if (property.id == id)
                    return property.name;
            }
            return null;
        }

        public static int GetLastID(List<Property> properties)
        {
            int lastID = 0;
            foreach (Property property in properties)
            {
                if (property.id > lastID)
                    lastID = property.id;
            }
            return lastID;
        }
    }
}