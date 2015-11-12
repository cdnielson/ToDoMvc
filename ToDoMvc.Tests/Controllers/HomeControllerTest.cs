using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoMvc;
using ToDoMvc.Controllers;
using ToDoMvc.Models;
using ToDoMvc.Tests.Repositories;

namespace ToDoMvc.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        //instantiate test items
        ToDo todo1 = null;
        ToDo todo2 = null;
        ToDo todo3 = null;
        ToDo todo4 = null;
        ToDo todo5 = null;
        List<ToDo> todos = null;
        DummyToDoRepository toDoRepository = null;
        HomeController controller = null;

        public HomeControllerTest()
        {
            //create test items
            todo1 = new ToDo {Id = 1, Item = "Item 1", Completed = true};
            todo2 = new ToDo {Id = 2, Item = "Item 2", Completed = false};
            todo3 = new ToDo {Id = 3, Item = "Item 3", Completed = true};
            todo4 = new ToDo {Id = 4, Item = "Item 4", Completed = false};
            todo5 = new ToDo {Id = 5, Item = "Item 5", Completed = true};

            //put test items into a List
            todos = new List<ToDo>
            {
                todo1,
                todo2,
                todo3,
                todo4,
                todo5
            };

            //dump list of todos into a dummy repository
            toDoRepository = new DummyToDoRepository(todos);

            //instatiate controller with dummy repository
            controller = new HomeController(toDoRepository);
        }

        //Test that the list returned by Index matches the list of To Dos
        [TestMethod]
        public void Index()
        {
            ViewResult result = controller.Index() as ViewResult;
            var model = (List<ToDo>) result.ViewData.Model;
            CollectionAssert.Contains(model, todo1);
            CollectionAssert.Contains(model, todo2);
            CollectionAssert.Contains(model, todo3);
            CollectionAssert.Contains(model, todo4);
            CollectionAssert.Contains(model, todo5);
        }

        //Test the Details view matches the To Do that was sent
        [TestMethod]
        public void Details()
        {
            ViewResult result = controller.Details(1) as ViewResult;
            Assert.AreEqual(result.Model, todo1);
        }

        //Test when a new item is added it appears in the list
        [TestMethod]
        public void Create()
        {
            ToDo newToDo = new ToDo {Id = 6, Item = "Item 6", Completed = false};
            controller.Create(newToDo);
            List<ToDo> todos = toDoRepository.GetAllToDos();
            CollectionAssert.Contains(todos, newToDo);
        }

        //Test that when an item is updated the collection contains it
        [TestMethod]
        public void Edit()
        {
            int testid = 1;
            ToDo originalToDo = toDoRepository.GetToDoById(testid);
            ToDo editedToDo = new ToDo {Id = testid, Item = "Item One", Completed = false};
            controller.Edit(editedToDo);
            List<ToDo> todos = toDoRepository.GetAllToDos();
            CollectionAssert.Contains(todos, editedToDo);
            CollectionAssert.DoesNotContain(todos, originalToDo);
        }

        //Test that the item deleted gets deleted
        [TestMethod]
        public void Delete()
        {
            int idToDelete = 2;
            ToDo deletedToDo = toDoRepository.GetToDoById(idToDelete);
            controller.Delete(idToDelete);
            List<ToDo> todos = toDoRepository.GetAllToDos();
            CollectionAssert.DoesNotContain(todos, deletedToDo);
        }
    }
}
