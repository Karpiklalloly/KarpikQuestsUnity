using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System;
using System.Collections.Generic;

namespace Karpik.Quests
{
    public interface IGraph
    {
        public event Action<Quest> QuestUnlocked; 
        public event Action<Quest> QuestCompleted;
        public event Action<Quest> QuestFailed;
    
        public IEnumerable<Quest> Quests { get; }
        public IEnumerable<Quest> StartQuests { get; }

        public IEnumerable<Quest> QuestsWithStatus(Status status);
    
        public bool TryAdd(Quest quest);

        public bool TryRemove(Id quest);
    
        public bool TryReplace(Quest from, Quest to);

        public void Setup();
        
        public void Clear();
    
        public bool Has(Id questId);
        
        public Quest GetQuest(Id questId);
        public Quest GetQuestDeep(Id questId);
    
        public bool TryAddDependency(Id questId, Id dependencyQuestId, IDependencyType dependencyType);
        public bool TryAddDependency(Id questId, Id dependencyQuestId, DependencyType dependencyType);
    
        public bool TryRemoveDependencies(Id questId);
        public bool TryRemoveDependents(Id questId);
    
        public bool TryRemoveDependency(Id questId, Id dependencyQuestId);
        
        public IEnumerable<QuestConnection> GetDependenciesQuests(Id questId);
        public IEnumerable<QuestConnection> GetDependentsQuests(Id questId);
    
        public bool IsCyclic();

        public void Update(Quest quest, bool inGraph);
    }
}