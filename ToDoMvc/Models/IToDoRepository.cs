using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoMvc.Models
{
    public interface IToDoRepository
    {
        List<ToDo> GetAllToDos();
        ToDo GetToDoById(int id);
        void AddToDo(ToDo todo);
        void UpdateToDo(ToDo todo);
        void DeleteToDo(ToDo todo);
        void Save();
    }
}