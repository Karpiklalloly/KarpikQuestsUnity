using Karpik.Quests.Converters;
using Karpik.UIExtension;
using UnityEngine.UIElements;

namespace Karpik.Quests.Unity
{
    [UxmlElement]
    public partial class QuestElement : ExtendedVisualElement
    {
        public override VisualElement contentContainer => _root;

        public Label Id => _root.Q<Label>("Id");
        public TextField Name => _root.Q<TextField>("Name");
        public TextField Description => _root.Q<TextField>("Description");
        public Toggle OrderlyCompletion => _root.Q<Toggle>("OrderlyCompletion");
        public DropdownField CompletionType => _root.Q<DropdownField>("CompletionType");
        public EnumField Status => _root.Q<EnumField>("Status");
        public QuestCollectionElement SubQuests => _root.Q<QuestCollectionElement>("SubQuests");
        
        private readonly VisualElement _root = LoadHelper.QuestElement.CloneTree();

        public QuestElement()
        {
            hierarchy.Add(_root);
            name = nameof(QuestElement);
            
            Init();
        }
        
        public void Init(Quest quest)
        {
            dataSource = quest;
            SubQuests.Init((IQuestCollection)quest.SubQuests, quest);
            AdditionalInit(quest);
        }
        
        public void Init()
        {
            InitId();
            InitName();
            InitDescription();
            InitOrderlyTasks();
            InitCompletionType();
            InitStatus();
            InitBundles();
        }

        private void InitId()
        {
            Id.RegisterBinding(nameof(Quest.Id),
                nameof(Id.text),
                (ref string s) => new Id(s),
                (ref Id s) => s.Value);
        }

        private void InitName()
        {
            Name.RegisterBinding(
                nameof(Quest.Name),
                nameof(Name.value),
                (ref string s) => s,
                (ref string s) => s);
        }

        private void InitDescription()
        {
            Description.RegisterBinding(
                nameof(Quest.Description),
                nameof(Description.value), 
                (ref string value) => value,
                (ref string value) => value);
        }

        private void InitOrderlyTasks()
        {
            OrderlyCompletion.RegisterBinding<bool, IProcessorType>(
                nameof(Quest.Processor),
                nameof(OrderlyCompletion.value), 
                ProcessorConverter.FromName,
                ProcessorConverter.ToName);
        }

        private void InitCompletionType()
        {
            CompletionType.RegisterBinding<string, ICompletionType>(
                nameof(Quest.CompletionType),
                nameof(CompletionType.value),
                CompletionTypeConverter.FromName,
                CompletionTypeConverter.ToName);
            
            CompletionType.choices = CompletionTypeConverter.Choices;
        }

        private void InitStatus()
        {
            Status.RegisterBinding<Status, Status>(
                nameof(Quest.Status), 
                nameof(Status.value));
        }

        private void InitBundles()
        {
            
        }

        protected virtual void AdditionalInit(Quest quest)
        {
            
        }
    }
}
