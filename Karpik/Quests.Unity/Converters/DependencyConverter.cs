using System;
using System.Collections.Generic;
using System.Linq;
using Karpik.Quests.DependencyTypes;

namespace Karpik.Quests.Converters
{
    public static class DependencyConverter
    {
        private static readonly Dictionary<string, Type> DependencyToType = new()
        {
            { nameof(Unlocked), typeof(Unlocked) },
            { nameof(Completion), typeof(Completion) },
            { nameof(Fail), typeof(Fail) }
        };
        
        private static readonly Dictionary<Type, string> TypeToDependency = new()
        {
            { typeof(Unlocked), nameof(Unlocked) },
            { typeof(Completion), nameof(Completion) },
            { typeof(Fail), nameof(Fail) }
        };
        
        public static List<string> Choices => DependencyToType.Keys.ToList();

        public static string ToName(ref IDependencyType status)
        {
            return TypeToDependency[status.GetType()];
        }

        public static IDependencyType FromName(ref string name, IGraph graph, Id quest, Id dependency)
        {
            var d = (IDependencyType)Activator.CreateInstance(DependencyToType[name]);
            graph.TryReplaceDependency(quest, dependency, d);
            return d;
        }
    }
}