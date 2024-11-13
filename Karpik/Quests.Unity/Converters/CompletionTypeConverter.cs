using System;
using System.Collections.Generic;
using System.Linq;

namespace Karpik.Quests.Converters
{
    public static class CompletionTypeConverter
    {
        private static readonly Dictionary<string, Type> TypeToCompletion = new()
        {
            { nameof(Or), typeof(Or) },
            { nameof(And), typeof(And) }
        };
        
        private static readonly Dictionary<Type, string> CompletionToType = new()
        {
            { typeof(Or), nameof(Or) },
            { typeof(And), nameof(And) }
        };

        public static List<string> Choices => TypeToCompletion.Keys.ToList();

        
        public static string ToName(ref ICompletionType type)
        {
            return CompletionToType[type.GetType()];
        }

        
        public static ICompletionType FromName(ref string name)
        {
            var index = name.LastIndexOf('.') + 1;
            name = name[index..];
            return (ICompletionType)Activator.CreateInstance(TypeToCompletion[name]);
        }
    }
}