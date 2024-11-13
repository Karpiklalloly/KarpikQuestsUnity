using System;
using System.Runtime.Serialization;
using Karpik.UIExtension;
using Karpik.UIExtension.Load;
using Newtonsoft.Json;
using UnityEngine;

namespace Karpik.Quests.Unity
{
    [Serializable]
    public class QuestNodeData : IQuestNode
    {
        [JsonIgnore]
        public Vector2 value { get; set; }
        public string Id { get; set; }
        public Id QuestId { get; set; }
        public Vector2 Position { get; set; }
        [JsonIgnore]
        public Vector2 Center { get; set; }
        public Vector2 Size { get; set; }
        public TextureInfo Texture { get; set; }

        public QuestNodeData(Vector2 value, string id, Vector2 position, Vector2 center, Vector2 size, Id questId,
            TextureInfo textureInfo)
        {
            this.value = value;
            Id = id;
            Position = position;
            Center = center;
            Size = size;
            QuestId = questId;
            Texture = textureInfo ?? PlaceHolder.TextureInfo;
        }

        public QuestNodeData(IQuestNode node) :
            this(node.value, node.Id, node.Position, node.Center, node.Size, node.QuestId, node.Texture)
        {

        }

        private QuestNodeData()
        {

        }

        public void SetValueWithoutNotify(Vector2 newValue)
        {

        }

        [OnDeserialized]
        private void OnDeserialize(StreamingContext ctx)
        {
            value = Position;
            Center = Position + new Vector2(Size.x / 2, Size.y / 2);
        }

    }
}