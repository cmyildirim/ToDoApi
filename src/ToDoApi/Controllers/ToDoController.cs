using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;


namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        public ToDoController(IToDoRepository todoItems)
        {
            ToDoItems = todoItems;
        }

        public IToDoRepository ToDoItems { get; set; }

        //Create
        [HttpPost]
        public IActionResult Create([FromBody] ToDoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            ToDoItems.Add(item);
            return CreatedAtRoute("GetTodo", new{ id = item.Key}, item);
        }

        //Read
        //GET /api/todo/{id}
        [HttpGet("GetById/{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = ToDoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //Read All
        //GET /api/todo
        [HttpGet]
        public IEnumerable<ToDoItem> GetAll()
        {
            return ToDoItems.GetAll();
        }

        //Update - PUT Full update
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] ToDoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            ToDoItems.Update(item);
            
            return new NoContentResult();
        }

        //Update - PATCH Partial update
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] ToDoItem item, string id)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            item.Key = todo.Key;

            ToDoItems.Update(item);
            return new NoContentResult();
        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = ToDoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            ToDoItems.Remove(id);
            return new NoContentResult();
        }
    }
}
