using System;
using Karpik.UIExtension;
using UnityEngine.UIElements;

namespace Karpik.Quests.Unity
{
    [UxmlElement]
    public partial class QuestMenu : ModalWindow
    {
        public Quest Quest { get; private set; }
        public bool IsReadOnly => !_editable;

        private ExtendedVisualElement _element;
        private bool _editable;

        public QuestMenu()
        {
            Title = "Quest";
        }

        public void InitQuest(Quest quest, bool editable)
        {
            Quest = quest;
            _editable = editable;
            if (editable)
            {
                var questElement = new QuestElement();
                questElement.Init(quest);
                _element = questElement;
                Add(questElement);
            }
            else
            {
                var questElement = new ReadQuestElement();
                questElement.Init(quest);
                _element = questElement;
                Add(questElement);
            }
        }

        public void AdditionalInit(Action<ExtendedVisualElement> additionalInit)
        {
            additionalInit?.Invoke(_element);
        }
    }
}
