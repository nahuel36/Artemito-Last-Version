using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using NUnit.Framework;

namespace Artemito
{
[Serializable]
public class CharacterTalk : CharacterInteraction, ICommand
{

        public enum ItemType
        {
            Message,
            Property,
            GameVariable
        }

        [Serializable]
        public class TalkItem
        {
            public ItemType type;
            public string message;
        }

        public override string Name => "Character talk";
        public List<TalkItem> messages;

        async Task ICommand.Execute()
        {
            IMessageTalker talker = character.messageTalker;

            StringBuilder sb = new StringBuilder();

            foreach (TalkItem message in messages)
            {
                sb.Append(message.message);
            }

            talker.Talk(sb.ToString(), true);
            while (talker.Talking)
                await Task.Yield();
        }



        public override VisualElement InspectorField(UnityEngine.Object target, SerializedObject serializedTarget)
        {
            VisualElement root = base.InspectorField(target, serializedTarget);
            
            root.Add(ShowUI(messages, target, serializedTarget));

            return root;
        }

        //hay llamadas recursivas en el layout, debido a usasr reorder animado y dymanic size
        //la unica diferencia que tiene con otros codigos es el text field y que tiene strings

        public static VisualElement ShowUI(List<TalkItem> mesagges_param, UnityEngine.Object myTarget, SerializedObject serializedObject)
        {
            VisualElement root = new VisualElement();

            ListView listView = VisualTreeAssets.Instance.customListView.Instantiate().Q<ListView>();

            listView.itemsSource = mesagges_param;

            listView.headerTitle = "Mesagges";

            listView.makeItem = () =>
            {
                VisualElement ve = new VisualElement();
                return ve;
            };
            
            listView.bindItem = (vElem, index) =>
            {
                vElem.Clear();

                TextField textField = new TextField();

                textField.value = mesagges_param[index].message;

                int indextemp = index;
                
                textField.RegisterValueChangedCallback((evt) =>
                {
                    mesagges_param[indextemp].message = evt.newValue;
                    EditorUtility.SetDirty(myTarget);
                    serializedObject.ApplyModifiedProperties();
                });
                
                vElem.Add(textField);
            };


            listView.overridingAddButtonBehavior = (actual_list, actual_button) =>
            {
                GenericMenu menu = new GenericMenu();
                foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
                { 
                    menu.AddItem(new GUIContent(type.ToString()), false, () =>
                    {
                        TalkItem item = new TalkItem();
                        item.type = type;
                        item.message = "";
                        mesagges_param.Add(item);
                        listView.itemsSource = null;
                        listView.itemsSource = mesagges_param;
                        listView.Rebuild();
                        EditorUtility.SetDirty(myTarget);
                        serializedObject.ApplyModifiedProperties();
                    });
                }
                menu.ShowAsContext();
            };



            root.Add(listView);
            return root;
        }







        public override void Skip()
        {
            throw new System.NotImplementedException();
        }
    }
}
