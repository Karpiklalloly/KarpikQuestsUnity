using Karpik.UIExtension;

namespace Karpik.Quests.Unity
{
    public interface IQuestNode : IGraphNode
    {
        public Id QuestId { get; set; }
    }
}