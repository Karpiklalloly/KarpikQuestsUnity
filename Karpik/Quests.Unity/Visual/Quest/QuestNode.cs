using Karpik.UIExtension;
using Newtonsoft.Json;
using UnityEngine.UIElements;

namespace Karpik.Quests.Unity
{
    [UxmlElement]
    public partial class QuestNode : GraphNode, IQuestNode
    {
        [UxmlAttribute("QuestId")]
        [JsonProperty("QuestId")]
        public Id QuestId { get; set; }

        public QuestNode()
        {
            
        }
        
        public QuestNode(QuestNodeData data)
        {
            SetId(data.Id);
            QuestId = data.QuestId;
            value = data.Position;
            Size = data.Size;
            Texture = data.Texture;
        }
    }
}
