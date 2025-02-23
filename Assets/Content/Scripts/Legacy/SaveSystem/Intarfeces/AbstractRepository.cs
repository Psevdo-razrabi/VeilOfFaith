using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace SaveSystem
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        public DataContext DataContext { get; private set; }
        protected List<T> CollectionList = new();

        public AbstractRepository(DataContext context)
        {
            Preconditions.CheckNotNull(context);
            DataContext = context;
        }

        public IEnumerable<T> GetAll() => CollectionList;

        public void SetToCollection(IEnumerable<T> collection) => DataContext.SetDataToList(collection as List<T>);

        public void Create(T item) => CollectionList.Add(item);

        public void Update(T item)
        {
            var index = CollectionList.IndexOf(item);

            if (index == -1)
            {
                Debug.LogWarning($"{index} is not found in {nameof(CollectionList)}");
                return;
            }

            CollectionList[index] = item;
        }

        public abstract T GetElementInList(GuidId id);
        public abstract T GetData();
        public abstract void Delete(T item);
    }
}