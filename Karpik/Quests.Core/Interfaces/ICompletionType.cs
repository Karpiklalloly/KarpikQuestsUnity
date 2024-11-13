using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System.Collections.Generic;

namespace Karpik.Quests
{
    public interface ICompletionType
    {
        public Status Check(IEnumerable<IRequirement> requirements);
    }
}