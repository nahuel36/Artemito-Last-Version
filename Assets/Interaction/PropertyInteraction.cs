using Artemito;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Linq;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace Artemito { 

[System.Serializable]
public abstract class PropertyInteraction : Interaction
{
    public PropertyData data;
    public int propertyID;
    public UnityEngine.Object propertyContainer;
    public Property property;
#if UNITY_EDITOR
    public override VisualElement InspectorField(UnityEngine.Object target, SerializedObject serializedTarget)
    {
        VisualElement element = base.InspectorField(target, serializedTarget);
        VisualElement field = VisualTreeAssets.Instance.propertyInteraction.Instantiate();

        ObjectField propertyContainerField = (ObjectField)field.Q("container");
        propertyContainerField.objectType = typeof(UnityEngine.Object);
        propertyContainerField.value = propertyContainer;

        propertyContainerField.RegisterValueChangedCallback((evt) =>
        {
            bool founded = false;
            if (evt.newValue != null && evt.newValue is GameObject)
            {
                foreach (var obj in evt.newValue.GetComponents<MonoBehaviour>())
                {
                    if (obj is PropertiesContainer)
                    {
                        founded = true;
                    }
                }
            }
            if (evt.newValue is PropertiesContainer)
            {
                founded = true;
            }
            if (founded || evt.newValue == null)
            {
                propertyContainerField.value = evt.newValue;
                propertyContainer = evt.newValue;
            }
            if (!founded)
            {
                propertyContainerField.value = propertyContainer;
            }
            InteractionFields(field, target, serializedTarget);
            EditorUtility.SetDirty(target);
            serializedTarget.ApplyModifiedProperties();
        });

        element.Add(field);

        InteractionFields(field, target, serializedTarget);



        return element;
    }

    void InteractionFields(VisualElement field, UnityEngine.Object myTarget, SerializedObject serializedObject) 
    {
        VisualElement elem = field.Q("typeInspector");
        elem.Clear();

        PropertiesContainer localPropertyContainer = GetPropertyContainer();

        if (localPropertyContainer != null)
        {    
            elem.Add(localPropertyContainer.PropertyInspectorField(myTarget, serializedObject,data, (data)=>
            {
                this.data = data;
            }));

            if(data != null) 
            { 

                List<string> choices = new List<string>();

                List<Property> propertys = localPropertyContainer.GetAllProperties(data);

                if(propertys == null)
                    {
                        return;
                    }

                foreach (Property property in propertys)
                {
                    choices.Add(property.name);
                }

                field.Q<DropdownField>("property").choices = choices;
                field.Q<DropdownField>("property").value = Property.GetNameFromID(localPropertyContainer.GetAllProperties(data),propertyID);
                field.Q<DropdownField>("property").RegisterValueChangedCallback((evt) =>
                {
                    propertyID = Property.GetIDFromName(localPropertyContainer.GetAllProperties(data),evt.newValue);
                    property = localPropertyContainer.GetProperty(this.data, Property.GetNameFromID(localPropertyContainer.GetAllProperties(data), propertyID));
                    EditorUtility.SetDirty(myTarget);
                    serializedObject.ApplyModifiedProperties();
                });
            }
        }

       
    }
#endif

    public PropertiesContainer GetPropertyContainer()
    {
        if (propertyContainer != null)
        {
            if (propertyContainer is GameObject)
                return propertyContainer.GetComponent<PropertiesContainer>();
            else
                return (PropertiesContainer)propertyContainer;
        }
        return null;
    }
    }
}
