using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ToDoApi.Models
{
    internal class ToDoRepository : IToDoRepository
    {
        private static readonly ConcurrentDictionary<string, ToDoItem> _toDos = new ConcurrentDictionary<string, ToDoItem>();

        public ToDoRepository()
        {
            Add(new ToDoItem { Name = "ToDoItem1"});
        }

        public void Add(ToDoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _toDos[item.Key] = item;
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            return _toDos.Values;
        }

        public ToDoItem Find(string key)
        {
            ToDoItem item;
            _toDos.TryGetValue(key, out item);
            return item;
        }

        public void Update(ToDoItem item)
        {
            _toDos[item.Key] = item;
        }

        public ToDoItem Remove(string key)
        {
            ToDoItem item;
            _toDos.TryRemove(key, out item);
            return item;
        }
    }
}