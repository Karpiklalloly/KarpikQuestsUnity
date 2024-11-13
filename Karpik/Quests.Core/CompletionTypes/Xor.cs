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
    public class Xor : ICompletionType
    {
        public Status Check(IEnumerable<IRequirement> requirements)
        {
            if (!requirements.Any()) return Status.Completed;
            var satisfied = requirements.Count(Predicates.IsSatisfied);
            var failed = requirements.Count(Predicates.IsRuined);
            
            if (satisfied == 1) return Status.Completed;
            if (satisfied > 1) return Status.Failed;
            if (failed > 0) return Status.Failed;
            
            return Status.Locked;
        }
    }
}