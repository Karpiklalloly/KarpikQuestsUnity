using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System.Runtime.CompilerServices;

namespace Karpik.Quests
{
    public static class IdExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValid(this Id id)
        {
            return !id.IsEmpty();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty(this Id id)
        {
            return id.Equals(Id.Empty);
        }
    }
}