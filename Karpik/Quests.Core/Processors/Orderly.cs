using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System;
using System.Linq;

namespace Karpik.Quests.Processors
{
    [Serializable]
    public class Orderly : IProcessorType
    {
        public void Setup(QuestCollection quests)
        {
            if (quests.Count == 0) return;

            foreach (var subQuest in quests)
            {
                subQuest.Setup();
            }

            quests.First().TryUnlock();
        }

        public void Update(QuestCollection quests)
        {
            var lastCompleted = quests.LastOrDefault(x => x.IsCompleted());

            if (lastCompleted is null) return;

            int index = quests.IndexOf(lastCompleted);
            
            for (int i = 0; i < index; i++)
            {
                var quest = quests[i];
                if (!quest.IsCompleted() && !quest.IsFailed()) return;
            }

            index++;
            do
            {
                if (quests.Count == index)
                {
                    return;
                }
                
                var quest = quests[index];
                if (quest.IsLocked())
                {
                    quest.TryUnlock();
                    break;
                }

                index++;
            } while (true);
        }
    }
}
