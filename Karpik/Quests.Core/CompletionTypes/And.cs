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
    public class And : ICompletionType
    {
        public Status Check(IEnumerable<IRequirement> requirements)
        {
            if (!requirements.Any()) return Status.Completed;
            
            if (requirements.Any(Predicates.IsRuined)) return Status.Failed;
            if (requirements.All(Predicates.IsSatisfied)) return Status.Completed;
            if (requirements.Any(Predicates.IsSatisfied)) return Status.Unlocked;

            return Status.Locked;
        }
    }
}
