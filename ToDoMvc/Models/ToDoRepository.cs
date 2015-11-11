using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDoMvc.Models
{
    public class ToDoRepository : IToDoRepository, IDisposable
    {
        ToDoDatabaseEntities entities = new ToDoDatabaseEntities();

        #region IBooksRepository Members

        public ToDoRepository()
        {
            entities = new ToDoDatabaseEntities();
        }

        public List<ToDo> GetAllToDos()
        {
            return entities.ToDos.ToList();
        }

        public ToDo GetToDoById(int id)
        {
            return entities.ToDos.Find(id);
        }

        public void AddToDo(ToDo todo)
        {
            entities.ToDos.Add(todo);
        }

        public void UpdateToDo(ToDo todo)
        {
            entities.Entry(todo).State = EntityState.Modified;
        }

        public void DeleteToDo(ToDo todo)
        {
            entities.ToDos.Remove(todo);
        }

        public void Save()
        {
            entities.SaveChanges();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                entities = null;
            }
        }

        ~ToDoRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}