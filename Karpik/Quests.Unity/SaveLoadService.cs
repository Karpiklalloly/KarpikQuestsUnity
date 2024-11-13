using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Karpik.Quests.Unity
{
    public static class SaveLoadService
    {
        public static string Path = Application.dataPath + "Quests.json";
        
        public static void Save(QuestData data)
        {
            var text = JsonConvert.SerializeObject(data, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            });
            File.WriteAllText(Path, text);
        }

        public static QuestData Load()
        {
            if (!File.Exists(Path))
                return new QuestData()
                {
                    Graphs = new List<IGraph>(),
                    QuestGraphsData = new List<QuestGraphData>()
                };
            
            var text = File.ReadAllText(Path);
            try
            {
                return JsonConvert.DeserializeObject<QuestData>(text, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                return new QuestData()
                {
                    Graphs = new List<IGraph>(),
                    QuestGraphsData = new List<QuestGraphData>()
                };
            }
        }
    }
}