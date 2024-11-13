using UnityEngine;
using UnityEngine.UIElements;

namespace Karpik.Quests.Unity
{
    public static class LoadHelper
    {
        public static VisualTreeAsset QuestCollectionElement => Resources.Load<VisualTreeAsset>("Quests/QuestCollectionElement");
        
        public static VisualTreeAsset QuestElement => Resources.Load<VisualTreeAsset>("Quests/QuestElement");
        public static VisualTreeAsset ReadQuestElement => Resources.Load<VisualTreeAsset>("Quests/ReadQuestElement");
        
        public static VisualTreeAsset DependencyElement => Resources.Load<VisualTreeAsset>("Quests/DependencyElement");
    }
}