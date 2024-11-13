namespace Karpik.Quests.Unity
{
    public class QuestCollectionWrapper : CollectionWrapper<IQuestCollection, Quest>
    {
        public QuestCollectionWrapper(IQuestCollection collection) : base(collection)
        {
        }
    }
}