using System;
using System.Collections.Generic;
using UnityEngine;

namespace Karpik.Quests.Unity
{
    [Serializable]
    public struct QuestData
    {
        [SerializeReference]
        public List<IGraph> Graphs;
        
        [SerializeField]
        public List<QuestGraphData> QuestGraphsData;
    }
}