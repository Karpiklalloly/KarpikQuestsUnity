using System.Collections.Generic;
using Karpik.Quests.Processors;
using Karpik.UIExtension;
using UnityEngine.UIElements;

namespace Karpik.Quests.Unity
{
    [UxmlElement]
    public partial class DefaultQuestGraph : QuestGraph
    {
        private List<TooltipManipulator> _tooltips = new();
        
        public DefaultQuestGraph()
        {
            this.StretchToParentSize();
            RegisterMenuOpened("New Quest", OnNewQuestClicked);
            AddNodeMenu<QuestNode>("New Quest");
        }

        public sealed override void Load()
        {
            base.Load();
        }

        private void OnNewQuestClicked(IGraphNode node)
        {
            var name = "Quest";
            var description = "This is default quest";
            var quest = new Quest(
                Id.NewId(),
                name,
                description,
                new And(),
                new Orderly());
            Graph.TryAdd(quest);
            
            ((IQuestNode)node).QuestId = quest.Id;
        }

        protected override void OnNodeAdded<T>(T node)
        {
            if (node is not QuestNode questNode) return;

            var quest = Graph.GetQuest(questNode.QuestId);
            
            var tooltipManipulator = new TooltipManipulator(
                () => $"{quest.Name} ({quest.Status})",
                () => quest.Description,
                () => this);
            
            node.AddManipulator(tooltipManipulator);
            _tooltips.Add(tooltipManipulator);
        }
    }
}