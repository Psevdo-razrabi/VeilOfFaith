using System.Collections.Generic;
using Helpers;

namespace SaveSystem
{
    public interface IRepository<T> where T : class 
    {
        DataContext DataContext { get; }
        IEnumerable<T> GetAll();
        void SetToCollection(IEnumerable<T> collection);
        T GetElementInList(GuidId id);
        void Create(T item);
        void Delete(T item);
        void Update(T item);
        T GetData();
    }
}