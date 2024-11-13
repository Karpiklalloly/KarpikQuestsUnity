using Karpik.Quests.Converters;
using Karpik.UIExtension;
using UnityEngine.UIElements;

namespace Karpik.Quests.Unity
{
    [UxmlElement]
    public partial class DependencyElement : ExtendedVisualElement
    {
        public override VisualElement contentContainer => _root;
        public Label From => _root.Q<Label>("From");
        public Label To => _root.Q<Label>("To");
        public DropdownField Type => _root.Q<DropdownField>("Type");
        

        private VisualElement _root = LoadHelper.DependencyElement.CloneTree();
        private QuestConnection _connection;
        private IGraph _graph;

        public DependencyElement()
        {
            hierarchy.Add(_root);
            Type.choices = DependencyConverter.Choices;
            
            Init();
        }

        public void Init(QuestConnection connection, IGraph graph)
        {
            _connection = connection;
            dataSource = connection;
            _graph = graph;
        }

        private void Init()
        {
            From.RegisterBinding(
                nameof(QuestConnection.DependencyQuest),
                nameof(Label.text),
                null,
                (ref Quest value) => $"Id: {value.Id.Value}\n" +
                                      $"Name: {value.Name}\n" +
                                      $"Description: {value.Description}");
            
            To.RegisterBinding(
                nameof(QuestConnection.DependentQuest),
                nameof(Label.text),
                null,
                (ref Quest value) => $"Id: {value.Id.Value}\n" +
                                      $"Name: {value.Name}\n" +
                                      $"Description: {value.Description}");
            
            Type.RegisterBinding<string, IDependencyType>(
                nameof(QuestConnection.Dependency),
                nameof(DropdownField.value),
                (ref string value) =>
                    DependencyConverter.FromName(
                        ref value,
                        _graph,
                        _connection.DependentQuest.Id,
                        _connection.DependencyQuest.Id),
                DependencyConverter.ToName);
        }
    }
}