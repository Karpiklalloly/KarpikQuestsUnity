using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Karpik.Quests
{
    [Serializable]
    public class Or : ICompletionType
    {
        public Status Check(IEnumerable<IRequirement> quests)
        {
            if (!quests.Any()) return Status.Completed;
            
            var satisfied = quests.Count(Predicates.IsSatisfied);
            var failed = quests.Count(Predicates.IsRuined);
            
            if (satisfied > 0) return Status.Completed;
            if (failed > 0 ) return Status.Failed;
            
            return Status.Locked;
        }
    }
}
