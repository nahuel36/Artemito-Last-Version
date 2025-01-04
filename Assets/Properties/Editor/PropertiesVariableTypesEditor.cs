using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Artemito { 
public class PropertiesVariableTypesEditor : Editor
{
    public static VisualElement ShowUI(PropertyVariableTypes types, UnityEngine.Object target, SerializedObject serializedObject)
    {
        VisualElement root = new VisualElement();

        if (types.GetVariablesTypes() != null)
        {
            ListView listView = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

            listView.itemsSource = types.GetVariablesTypes();

            listView.overridingAddButtonBehavior = (actual_list_view, actual_button) =>
            {
                GenericMenu menu = new GenericMenu();

                foreach (var actual_type in PropertyVariableTypeRegistry.GetAllTypes())
                {
                    // Agregar opciones al menú
                    menu.AddItem(new GUIContent(actual_type.Name), false, () =>
                    {
                        types.AddVariableType(actual_type);
                        listView.Rebuild();
                        listView.itemsSource = null;
                        listView.itemsSource = types.GetVariablesTypes();
                        EditorUtility.SetDirty(target);
                        serializedObject.ApplyModifiedProperties();
                    });
                }
                // Mostrar el menú en la posición del mouse
                menu.ShowAsContext();
            };

            listView.headerTitle = "Property variables";

            root.Add(listView);

        }
        return root;
    }
}
}