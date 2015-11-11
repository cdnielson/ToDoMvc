using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoMvc.Models;

namespace ToDoMvc.Controllers
{
    public class HomeController : Controller
    {
        private IToDoRepository toDoRepository = null;

        public HomeController()
        :this(new ToDoRepository())
        {

        }

        public HomeController(IToDoRepository toDoRepo)
        {
            this.toDoRepository = toDoRepo;
        }

        public ActionResult Index()
        {
            List<ToDo> todos = toDoRepository.GetAllToDos();
            return View(todos);
        }

        public ActionResult Details(int id)
        {
            ToDo book = toDoRepository.GetToDoById(id);

            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ToDo todo)
        {
            if (ModelState.IsValid)
            {
                toDoRepository.AddToDo(todo);
                toDoRepository.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            ToDo book = toDoRepository.GetToDoById(id);

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Item,Completed")] ToDo todo)
        {
            if (ModelState.IsValid)
            {
                toDoRepository.UpdateToDo(todo);
                toDoRepository.Save();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            ToDo todo = toDoRepository.GetToDoById(id);
            toDoRepository.DeleteToDo(todo);
            return View(todo);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {
            ToDo todo = toDoRepository.GetToDoById(id);
            toDoRepository.DeleteToDo(todo);
            toDoRepository.Save();
            return View("Deleted");
        }

        public ActionResult Deleted()
        {
            return View();
        }
    }
}