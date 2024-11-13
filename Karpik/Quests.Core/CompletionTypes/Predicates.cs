using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System.Runtime.CompilerServices;

namespace Karpik.Quests
{
    public static class Predicates
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSatisfied(IRequirement requirement)
        {
            return requirement.IsSatisfied();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRuined(IRequirement requirement)
        {
            return requirement.IsRuined();
        }
    }
}