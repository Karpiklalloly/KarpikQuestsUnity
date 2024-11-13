using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System.Runtime.CompilerServices;

namespace Karpik.Quests
{
    public static class QuestExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLocked(this Quest quest)
        {
            return quest.IsValid() && quest.Status == Status.Locked;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnlocked(this Quest quest)
        {
            return quest.IsValid() && quest.Status == Status.Unlocked;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsCompleted(this Quest quest)
        {
            return quest.IsValid() && quest.Status == Status.Completed;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFailed(this Quest quest)
        {
            return quest.IsValid() && quest.Status == Status.Failed;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFinished(this Quest quest)
        {
            return quest.IsCompleted() || quest.IsFailed();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasParent(this Quest quest)
        {
            return quest.IsValid() && quest.ParentId.IsValid();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValid(this Quest quest)
        {
            return quest is not null && quest.Id.IsValid();
        }
    }
}