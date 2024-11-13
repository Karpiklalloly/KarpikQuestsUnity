using System.Collections.Generic;

namespace Karpik.Quests.Unity
{
    public class Quests
    {
        public static Quests Instance => _instance ??= new Quests();
        private static Quests _instance;

        public IGraph MainGraph
        {
            get
            {
                Load();
                return _graph;
            }
        }
        
        public QuestGraphData QuestGraphData { get; set; }
        private IGraph _graph;
        
        private QuestData _questData = SaveLoadService.Load();

        public void Init(QuestData data)
        {
            _questData.Graphs = data.Graphs;
            _questData.QuestGraphsData = data.QuestGraphsData;
            
            if (_questData.Graphs.Count == 0)
            {
                _questData.Graphs.Add(new Graph());
            }
            _graph = _questData.Graphs[0];

            if (_questData.QuestGraphsData.Count == 0)
            {
                _questData.QuestGraphsData.Add(new QuestGraphData(new List<QuestNodeData>()));
            }
            QuestGraphData = _questData.QuestGraphsData[0];
            
            Save();
        }
        
        public void Save()
        {
            if (_questData.Graphs.Count == 0)
            {
                _questData.Graphs.Add(_graph);
            }
            else
            {
                _questData.Graphs[0] = _graph;
            }

            if (_questData.QuestGraphsData.Count == 0)
            {
                _questData.QuestGraphsData.Add(QuestGraphData);
            }
            else
            {
                _questData.QuestGraphsData[0] = QuestGraphData;
            }
            
            SaveLoadService.Save(_questData);
        }

        public void Load()
        {
            if (_questData.Graphs == null || _questData.QuestGraphsData == null)
            {
                Init(SaveLoadService.Load());
            }
            
            if (_questData.Graphs!.Count == 0)
            {
                Init(SaveLoadService.Load());
            }

            if (_questData.QuestGraphsData!.Count == 0)
            {
                Init(SaveLoadService.Load());
            }
            
            _graph = _questData.Graphs[0];
            QuestGraphData = _questData.QuestGraphsData[0];
        }

        public void Reload()
        {
            Init(SaveLoadService.Load());
        }
    }
}