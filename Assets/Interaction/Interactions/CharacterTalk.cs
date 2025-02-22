using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Artemito
{
[Serializable]
public class CharacterTalk : CharacterInteraction, ICommand
{

        [Serializable]
        public class TalkItem
        {
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
        

            listView.itemsAdded += new Action<IEnumerable<int>>((IEnumerable<int> k) =>
            {
                mesagges_param[mesagges_param.Count - 1] = new TalkItem();
                EditorUtility.SetDirty(myTarget);
                serializedObject.ApplyModifiedProperties();
            });



            root.Add(listView);
            return root;
        }







        public override void Skip()
        {
            throw new System.NotImplementedException();
        }
    }
}
