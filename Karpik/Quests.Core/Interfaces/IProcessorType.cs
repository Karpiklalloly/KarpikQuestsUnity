using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
namespace Karpik.Quests
{
    public interface IProcessorType
    {
        public void Setup(QuestCollection quests);
        public void Update(QuestCollection quests);
    }
}