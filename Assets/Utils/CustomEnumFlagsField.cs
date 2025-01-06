using UnityEngine;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Artemito { 



[System.Serializable]
public class CustomEnumFlagsField
{
    [SerializeReference] public List<PropertyVariable> members;

    public List<string> GetMembersList()
    {
            if (members == null) return new List<string> { "" } ;

        List<string> membersList = new List<string>();

        foreach (var member in members)
        { 
            membersList.Add(member.Name);
        }

        return membersList;
    }

    public System.Enum MembersToEnum(List<string> flags)
    {
        int value = 0;
        if(members == null)
            members = new List<PropertyVariable>();
        for (int i = 0; i < flags.Count; i++)
        {
            foreach (PropertyVariable property in members)
            {
                if (property.Name == flags[i])
                {
                    value += 1 << i;
                }
            }
        
        }
        return (GenericEnum)value;
    }

    public void EnumToMembers(List<string> flags, System.Enum enumValue)
    {
        int newValue = Convert.ToInt32(enumValue);

        if (members == null)
            members = new List<PropertyVariable>();
        int oldValue = Convert.ToInt32(MembersToEnum(flags));
        List<string> toRemove = new List<string>();
        List<string> toAdd = new List<string>();
        for (int i = 0; i < flags.Count; i++)
        {
            if ((oldValue & (1 << i)) != 0)
            {
                if ((newValue & (1 << i)) == 0)
                {
                    toRemove.Add(flags[i]);
                }
            }
            if ((newValue & (1 << i)) != 0)
            {
                if ((oldValue & (1 << i)) == 0)
                {
                    toAdd.Add(flags[i]);
                }
            }
        }
        for (int i = members.Count-1; i >= 0; i--)
        {
            for (int j = 0; j < toRemove.Count; j++)
            {
                if (members[i].Name == toRemove[j])
                    members.RemoveAt(i);
            }
        }
        for (int i = 0; i < toAdd.Count; i++)
        {
            members.Add(Settings.Instance.propertyVariables.GetVariableInstanceFromName(toAdd[i]));
        }
    }

        public void StringToSingleMember(string member_name)
        {
            if (members == null)
                members = new List<PropertyVariable>();

            bool founded = false;

            List<string> toRemove = new List<string>();
            
            for (int j = members.Count - 1; j >= 0; j--)
            {
                if (members[j].Name != member_name)
                    toRemove.Add(members[j].Name);
                else
                    founded = true;
            }
            for (int i = members.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < toRemove.Count; j++)
                {
                    if (members[i].Name == toRemove[j])
                        members.RemoveAt(i);
                }
            }
            if(!founded)
                members.Add(Settings.Instance.propertyVariables.GetVariableInstanceFromName(member_name));
            }

        public string GetSingleMemberString()
        {
            if (members == null)
                members = new List<PropertyVariable>();

            return members.Count > 0 ? members[0].Name : "";
        }
    

        [System.Flags]
    public enum GenericEnum
    {
        value1 = 1 << 0,
        value2 = 1 << 1,
        value3 = 1 << 2,
        value4 = 1 << 3,
        value5 = 1 << 4,
        value6 = 1 << 5,
        value7 = 1 << 6,
        value8 = 1 << 7,
        value9 = 1 << 8,
        value10 = 1 << 9,
        value11 = 1 << 10,
        value12 = 1 << 11,
        value13 = 1 << 12,
        value14 = 1 << 13,
        value15 = 1 << 14,
        value16 = 1 << 15,
        value17 = 1 << 16,
        value18 = 1 << 17,
        value19 = 1 << 18,
        value20 = 1 << 19,
        value21 = 1 << 20,
        value22 = 1 << 21,
        value23 = 1 << 22,
        value24 = 1 << 23,
        value25 = 1 << 24,
        value26 = 1 << 25,
        value27 = 1 << 26,
        value28 = 1 << 27,
        value29 = 1 << 28,
        value30 = 1 << 29,
        value31 = 1 << 30,
        value32 = 1 << 31,
        value33 = 1 << 32,
        value34 = 1 << 33,
        value35 = 1 << 34,
        value36 = 1 << 35,
        value37 = 1 << 36,
        value38 = 1 << 37,
        value39 = 1 << 38,
        value40 = 1 << 39,
        value41 = 1 << 40,
        value42 = 1 << 41,
        value43 = 1 << 42,
        value44 = 1 << 43,
        value45 = 1 << 44,
        value46 = 1 << 45,
        value47 = 1 << 46,
        value48 = 1 << 47,
        value49 = 1 << 48,
        value50 = 1 << 49,
    }
    }
}
