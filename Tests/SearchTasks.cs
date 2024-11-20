using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTinyTodo.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;


namespace MyTinyTodo.Tests
{
    public class SearchTasks : BaseTest
    {
        [Test]
        public void SearchTaskInGeneralList()
        {
            string taskName = "task in General List Inbal";
            string listName = "Inbal General List";
            AddSimpleTaskTest at = new AddSimpleTaskTest();
            TasksPage tp = new TasksPage(driver);
            tp.AddTAsk(taskName, listName);
            Thread.Sleep(1000);
            tp.SearchITemsInGeneralList(taskName);
            Assert.That(tp.GetTaskByName(taskName), Is.Not.Null);
        }
        [Test]
        public void SearchTaskInSpecificList()
        {
            string listName = "Inbal Specific List";
            string taskName = $"task in {listName} List Inbal";
            AddSimpleTaskTest at = new AddSimpleTaskTest();
            TasksPage tp = new TasksPage(driver);
            tp.AddTAsk(taskName, listName);
            Thread.Sleep(1000);
            tp.SearchITemsInSpecificList(listName,taskName);
            Assert.That(tp.GetTaskByName(taskName), Is.Not.Null);
        }
       
    }
}
