using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
namespace Karpik.Quests
{
    public interface IDependencyType
    {
        public bool IsOk(Quest from);
    }
}