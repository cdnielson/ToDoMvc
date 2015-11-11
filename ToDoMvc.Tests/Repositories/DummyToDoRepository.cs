using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ToDoMvc.Models;

namespace ToDoMvc.Tests.Repositories
{
    class DummyToDoRepository : IToDoRepository
    {

        List<ToDo> m_todos = null;

        public DummyToDoRepository(List<ToDo> todos)
        {
            m_todos = todos;
        }
         
        public void AddToDo(ToDo todo)
        {
            m_todos.Add(todo);
        }

        public void DeleteToDo(ToDo todo)
        {
            m_todos.Remove(todo);
        }

        public List<ToDo> GetAllToDos()
        {
            return m_todos;
        }

        public ToDo GetToDoById(int id)
        {
            return m_todos.SingleOrDefault(book => book.Id == id);
        }

        public void Save()
        {
            
        }

        public void UpdateToDo(ToDo todo)
        {
            int id = todo.Id;
            ToDo toDoToUpdate = m_todos.SingleOrDefault(b => b.Id == id);
            DeleteToDo(toDoToUpdate);
            m_todos.Add(todo);
        }
    }
}
