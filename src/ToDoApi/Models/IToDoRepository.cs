using System.Collections.Generic;

namespace ToDoApi.Models
{
    //Defines basic CRUD operations
    public interface IToDoRepository
    {
        void Add(ToDoItem item);//Create
        IEnumerable<ToDoItem> GetAll();//Read All
        ToDoItem Find(string key);//Read
        void Update(ToDoItem item);//Update
        ToDoItem Remove(string key);//Delete
    }
}
