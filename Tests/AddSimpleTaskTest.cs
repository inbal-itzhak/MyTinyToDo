using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTinyTodo.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Allure.NUnit;

namespace MyTinyTodo.Tests
{
    [AllureNUnit]
    public class AddSimpleTaskTest : BaseTest
    {
        
        [Test]
        public void AddSimpleTask()
        {
            string taskName = "task number 1 Inbal";
            string listName = "Inbal Todo";
            TasksPage tp = new TasksPage(driver);
            tp.AddTAsk(taskName, listName);
            Assert.That(tp.GetTaskByName(listName,taskName),Is.EqualTo(taskName), $"there is no task by the name of {taskName}");
        }
    }
}
