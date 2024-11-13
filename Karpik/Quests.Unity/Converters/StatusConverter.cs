using System.Collections.Generic;
using System.Linq;

namespace Karpik.Quests.Converters
{
    public static class StatusConverter
    {
        private static readonly Dictionary<string, Status> StatusToType = new()
        {
            { nameof(Status.Locked), Status.Locked },
            { nameof(Status.Unlocked), Status.Unlocked },
            { nameof(Status.Failed), Status.Failed },
            { nameof(Status.Completed), Status.Completed }
        };
        
        private static readonly Dictionary<Status, string> TypeToStatus = new()
        {
            { Status.Locked, nameof(Status.Locked) },
            { Status.Unlocked, nameof(Status.Unlocked) },
            { Status.Failed, nameof(Status.Failed) },
            { Status.Completed, nameof(Status.Completed) }
        };
        
        public static List<string> Choices => StatusToType.Keys.ToList();

        public static string ToName(ref Status status)
        {
            return TypeToStatus[status];
        }

        public static Status FromName(ref string name)
        {
            return StatusToType[name];
        }
    }
}