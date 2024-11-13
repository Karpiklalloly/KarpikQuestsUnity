using System;
using Karpik.UIExtension;
using Unity.Properties;
using UnityEngine.UIElements;

namespace Karpik.Quests.Unity
{
    [UxmlElement]
    public partial class QuestCollectionElement : ExtendedVisualElement
    {
        public ListView Quests => _root.Q<ListView>("Quests");

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                Quests.showAddRemoveFooter = !value;
                _isReadOnly = value;
            }
        }
        
        private bool _isReadOnly = false;
        
        private VisualElement _root = LoadHelper.QuestCollectionElement.CloneTree();
        private QuestCollectionWrapper _collection;
        private Quest _owner;
        
        public QuestCollectionElement()
        {
            hierarchy.Add(_root);
            name = nameof(QuestCollectionElement);
            
            Init();
        }

        public void Init(IQuestCollection collection, Quest owner = null)
        {
            _owner = owner;
            _collection = new QuestCollectionWrapper(collection);
            Quests.itemsSource = _collection;
        }
        
        public void Init()
        {
            var list = Quests;
            
            list.makeItem = () => new Button();

            list.bindItem = (element, index) =>
            {
                if (list.itemsSource is not QuestCollectionWrapper collectionWrapper)
                    throw new ArgumentException($"Expected QuestCollectionWrapper, got {list.itemsSource.GetType().Name}");
                var collection = collectionWrapper.Collection;
                var button = element as Button;
                button!.dataSource = collection[index];
                button.RegisterBinding<string, string>(
                    PropertyPath.FromName(nameof(Quest.Name)).ToString(),
                    nameof(button.text));
                button.clicked += () =>
                {
                    VisualElement element;
                    if (IsReadOnly)
                    {
                        var quest = new ReadQuestElement();
                        quest.Init(collection[index]);
                        element = quest;
                    }
                    else
                    {
                        var quest = new QuestElement();
                        quest.Init(collection[index]);
                        element = quest;
                    }
                    
                    Modal.Start<ModalWindow>("Quest " + button.text)
                        .Add(element)
                        .Show();
                };
            };
            
            list.showAddRemoveFooter = true;
            list.onAdd += view =>
            {
                if (_owner is not null)
                {
                    _owner.Add(new Quest());
                }
                else
                {
                    _collection.Add(new Quest());
                }
            };
            list.onRemove += view =>
            {
                var quest = _collection.Collection[view.selectedIndex];
                
                if (_owner is not null)
                {
                    _owner.Remove(quest);
                }
                else
                {
                    _collection.Remove(quest);
                }
            };
        }
    }
}