using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System;
using System.Collections.Generic;

namespace Karpik.Quests
{
    public interface IReadOnlyQuestCollection : IEnumerable<Quest>, IEquatable<IReadOnlyQuestCollection>
    {
        public bool Has(in Quest item);
    }
}