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
    public class AddTaskAdvancedWindow : BaseTest
    {
        [Test]
        public void AddTaskWithParameters()
        {
            string listName = "Inbal Todo";
            int priority = 5;
            double year = 2025;
            string month = "December";
            int day = 12;
            string taskName = "Advanced Task Inbal";
            string notes = "Advanced Notes Inbal";
            string tag = "Advanced tag Inbal";
            TasksPage tp = new TasksPage(driver);
            AdvanvcedTaskPage atp = new AdvanvcedTaskPage(driver);
            tp.SelectList(listName);
            atp.OpenAdvancedTaskPage();
            atp.SelectPriority(priority);
            atp.SelectDueDateFromCalanfer(year, month, day);
            atp.EnterTaskName(taskName);
            atp.EnterNotes(notes);
            atp.EnterTag(tag);
            atp.SaveAdvancedTask();
            Thread.Sleep(1000);
            Assert.That(tp.GetTaskByName(listName, taskName), Is.EqualTo(taskName), $"there is no task by the name of {taskName}");
        }
    }
}
