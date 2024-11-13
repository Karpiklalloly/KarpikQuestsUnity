using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using Karpik.Quests.Serialization;

namespace Karpik.Quests
{
    [Serializable]
    public class NeededCount : ICompletionType
    {
        [DoNotSerializeThis]
        [Property]
        [JsonIgnore]
        [CreateProperty]
        public int Count { get => _count; set => _count = value; }

        [SerializeThis("Count")]
        [JsonProperty(PropertyName = "Count")]
        [SerializeField]
        private int _count;
        public NeededCount() : this(0)
        {
        }

        public NeededCount(int count)
        {
            _count = count;
        }

        public Status Check(IEnumerable<IRequirement> quests)
        {
            if (Count == 0)
                return Status.Completed;
            if (!quests.Any())
                return Status.Unlocked;
            var satisfied = quests.Count(Predicates.IsSatisfied);
            var failed = quests.Count(Predicates.IsRuined);
            if (satisfied >= Count)
                return Status.Completed;
            if (failed > 0)
                return Status.Failed;
            if (satisfied > 0)
                return Status.Unlocked;
            return Status.Locked;
        }
    }
}