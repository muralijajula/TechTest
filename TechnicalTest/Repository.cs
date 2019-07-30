using System;
using System.Collections.Generic;

namespace Interview
{
    public class Repository<T> : IRepository<T> where T : IStoreable
    {
        private readonly List<T> _entities;
        public Repository()
        {
            _entities = new List<T>();
        }

        public IEnumerable<T> All()
        {
            return _entities;
        }

        public void Delete(IComparable id)
        {
            var existingRecord = FindById(id);
            if (existingRecord != null)
                _entities.Remove(existingRecord);
        }

        public T FindById(IComparable id)
        {
            return _entities.Find(EntityMatch(id));
        }

        public void Save(T item)
        {
             Delete(item.Id);

            _entities.Add(item);
        }

        private Predicate<T> EntityMatch(IComparable id)
        {
            return x => x.Id.Equals(id);
        }
    }
}