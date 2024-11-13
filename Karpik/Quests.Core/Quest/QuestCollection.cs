using Newtonsoft.Json;
using UnityEngine;
using Karpik.UIExtension;
using Unity.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karpik.Quests.Serialization;

namespace Karpik.Quests
{
    [Serializable]
    public class QuestCollection : IQuestCollection
    {
        [SerializeThis("Data")]
        [JsonProperty(PropertyName = "Data")]
        [SerializeField]
        private List<Quest> _data = new();
        public QuestCollection()
        {
        }

        public QuestCollection(IEnumerable<Quest> quests)
        {
            foreach (var quest in quests)
            {
                _data.Add(quest);
            }
        }

#region list
        [DoNotSerializeThis]
        [JsonIgnore]
        public int Count => _data.Count;

        [DoNotSerializeThis]
        [JsonIgnore]
        public bool IsReadOnly => false;

        public Quest this[int index] { get => _data[index]; set => _data[index] = value; }

        public void Add(Quest item)
        {
            if (Has(in item))
                return;
            _data.Add(item);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(Quest item)
        {
            return Has(in item);
        }

        public void CopyTo(Quest[] array, int arrayIndex)
        {
            _data.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Quest> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public bool Remove(Quest item)
        {
            if (!Has(in item))
                return false;
            var index = IndexOf(item);
            if (index < 0)
                return false;
            _data.RemoveAt(index);
            return true;
        }

        public int IndexOf(Quest item)
        {
            return _data.FindIndex(x => x.Equals(item));
        }

        public void Insert(int index, Quest item)
        {
            _data.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _data.RemoveAt(index);
        }

        public bool Has(in Quest item)
        {
            return _data.Contains(item);
        }

#endregion
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($" Count:{Count}\n");
            foreach (Quest item in _data)
            {
                builder.Append(item.ToString() + '\n');
            }

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is QuestCollection collection))
                return false;
            return Equals(collection);
        }

        public override int GetHashCode()
        {
            return _data.GetHashCode();
        }

        public bool Equals(IReadOnlyQuestCollection other)
        {
            if (other is null)
                return false;
            for (int i = 0; i < other.Count(); i++)
            {
                if (!this[i].Equals(other.ElementAt(i)))
                    return false;
            }

            return true;
        }
    }
}