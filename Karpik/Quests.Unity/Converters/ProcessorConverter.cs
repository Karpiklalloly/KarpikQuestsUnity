using System;
using System.Collections.Generic;
using Karpik.Quests.Processors;

namespace Karpik.Quests.Converters
{
    public static class ProcessorConverter
    {
        private static readonly Dictionary<Type, bool> ProcessorToOrderly = new()
        {
            { typeof(Orderly), true },
            { typeof(Disorderly), false }
        };

        private static readonly Dictionary<bool, Type> OrderlyToProcessor = new()
        {
            { true, typeof(Orderly) },
            { false, typeof(Disorderly) }
        };

        
        public static bool ToName(ref IProcessorType processor)
        {
            return ProcessorToOrderly[processor.GetType()];
        }
        
        
        public static IProcessorType FromName(ref bool value)
        {
            return (IProcessorType)Activator.CreateInstance(OrderlyToProcessor[value]);
        }
    }
}