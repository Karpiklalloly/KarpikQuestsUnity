using System;
using Karpik.UIExtension;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Karpik.Quests.Unity
{
    [UxmlElement]
    public partial class ReadQuestElement : ExtendedVisualElement
    {
        public Label Id => _root.DeepQ<Label>("Id");
        public Label Name => _root.DeepQ<Label>("Name");
        public Label Description => _root.DeepQ<Label>("Description");
        public QuestCollectionElement SubQuests => _root.Q<QuestCollectionElement>("SubQuests");
        
        private readonly VisualElement _root = LoadHelper.ReadQuestElement.CloneTree();
        
        public ReadQuestElement()
        {
            hierarchy.Add(_root);
            name = nameof(ReadQuestElement);
            
            Init();
        }
        
        public void Init(Quest quest)
        {
            dataSource = quest;
            SubQuests.Init((IQuestCollection)quest.SubQuests, quest);
            SubQuests.IsReadOnly = true;
        }
        
        private void Init()
        {
            InitId();
            InitName();
            InitDescription();
            //InitOrderlyTasks();
            InitCompletionType();
            InitStatus();

            //Bundles.Grid.CountPerLine = 4;
        }
        
        private void InitId()
        {
            Id.RegisterBinding(nameof(Quest.Id),
                nameof(Id.text),
                null,
                (ref Id s) => s.Value,
                BindingMode.ToTarget);
        }

        private void InitName()
        {
            Name.RegisterBinding(
                nameof(Quest.Name),
                nameof(Name.text),
                null,
                (ref string s) => s,
                BindingMode.ToTarget);
        }

        private void InitDescription()
        {
            Description.RegisterBinding(
                nameof(Quest.Description),
                nameof(Description.text), 
                null,
                (ref string value) => value,
                BindingMode.ToTarget);
        }
        
        // private void InitOrderlyTasks()
        // {
        //     var bundles = Bundles;
        //
        //     bundles.RegisterBinding<StyleColor, IProcessorType>(
        //         nameof(Quest.Processor),
        //         PropertyPath.FromName(nameof(Bundles.style)).Add(nameof(Bundles.style.backgroundColor)).ToString(),
        //         null,
        //         (ref IProcessorType processor) =>
        //         {
        //             if (processor is Orderly)
        //             {
        //                 return new StyleColor(new Color(1, 0, 0, 0.2f));
        //             }
        //             else
        //             {
        //                 return new StyleColor(new Color(0, 1, 0, 0.2f));
        //             }
        //         });
        // }

        private void InitCompletionType()
        {
            Id.RegisterBinding<StyleColor, ICompletionType>(
                nameof(Quest.CompletionType),
                PropertyPath.FromName(nameof(Id.style)).Add(nameof(Id.style.backgroundColor)).ToString(),
                null,
                ((ref ICompletionType completionType) =>
                {
                    return completionType switch
                    {
                        And => new StyleColor(new Color(1, 0, 0, 0.2f)),
                        Or => new StyleColor(new Color(0, 1, 0, 0.2f)),
                        _ => new StyleColor(new Color(1, 1, 0, 0.2f))
                    };
                }));
        }

        private void InitStatus()
        {
            Name.RegisterBinding<StyleColor, Status>(
                nameof(Quest.Status),
                PropertyPath.FromName(nameof(Name.style)).Add(nameof(Name.style.backgroundColor)).ToString(),
                null,
                ((ref Status status) =>
                {
                    return status switch
                    {
                        Status.Locked => new StyleColor(new Color(0, 0, 1, 0.2f)),
                        Status.Unlocked => new StyleColor(new Color(1, 1, 0, 0.2f)),
                        Status.Completed => new StyleColor(new Color(0, 1, 0, 0.2f)),
                        Status.Failed => new StyleColor(new Color(1, 0, 0, 0.2f)),
                        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
                    };
                }));
        }
    }
}
