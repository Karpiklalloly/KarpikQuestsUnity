using System;
using System.Collections;
using System.Collections.Generic;

namespace Karpik.Quests.Unity
{
    public abstract class CollectionWrapper<TCollection, T> : IList
    where TCollection : IList<T>
    {
        public TCollection Collection => _collection;
        private TCollection _collection;

        public CollectionWrapper(TCollection collection)
        {
            _collection = collection;
        }

        public IEnumerator GetEnumerator() => _collection.GetEnumerator();

        public void CopyTo(Array array, int index) => _collection.CopyTo((T[])array, index);

        public int Count => _collection.Count;
        public bool IsSynchronized => false;
        public object SyncRoot { get; } = new object();
        public bool IsFixedSize => false;
        public bool IsReadOnly => false;
        
        public int Add(object value)
        {
            _collection.Add((T)value);
            return Count - 1;
        }

        public void Clear() => _collection.Clear();

        public bool Contains(object value) => _collection.Contains((T)value);

        public int IndexOf(object value) => _collection.IndexOf((T)value);

        public void Insert(int index, object value) => _collection.Insert(index, (T)value);

        public void Remove(object value) => _collection.Remove((T)value);

        public void RemoveAt(int index) => _collection.RemoveAt(index);
        
        public object this[int index]
        {
            get => _collection[index];
            set => _collection[index] = (T)value;
        }
    }
}