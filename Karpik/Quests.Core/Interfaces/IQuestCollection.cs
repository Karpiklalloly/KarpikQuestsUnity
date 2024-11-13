using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System.Collections.Generic;

namespace Karpik.Quests
{
    public interface IQuestCollection : IReadOnlyQuestCollection, IList<Quest>
    {
        
    }
}