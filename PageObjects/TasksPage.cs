using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyTinyTodo.PageObjects
{
    public class TasksPage : BasePage
    {
        
        public TasksPage(IWebDriver driver) : base(driver)
        {
          
        }
        
       
        public List<IWebElement> GetAllTasks()
        {
            
            List<IWebElement> tasks = Driver.FindElements(By.CssSelector("#tasklist .task-title")).ToList();
            return tasks;
        }

        public IWebElement? GetTaskByName(string name) 
        {
            List<IWebElement> allTasks =  GetAllTasks();
            if(allTasks.Count > 0)
            {
                foreach (IWebElement task in allTasks)
                {
                    string taskName = Driver.FindElement(By.CssSelector("#tasklist li .task-title")).Text;
                    if (taskName == name)
                    {
                        return task;
                    }
                }
            }
            return null;

        }

        public string GetTaskByName(string listName, string taskName)
        {
            Console.WriteLine("click on 'All Tasks'");
            Click(GetAllTaskLists().First());
            Thread.Sleep(1000);
            Console.WriteLine($"get '{taskName}' task from the list");
            foreach (IWebElement taskInList in GetAllTasks())
            {
               // string name = Driver.FindElement(By.CssSelector("#tasklist li .task-title")).Text;
                Console.WriteLine(taskInList.Text);
                if (taskInList.Text == taskName)
                {
                    return taskInList.Text;
                }

            }
           
            return string.Empty;
        }

        public List<IWebElement> GetAllTaskLists()
        {
            List<IWebElement> allTaskLists = Driver.FindElements(By.CssSelector(".title-block .title")).ToList();
            List<string> tasks = new List<string>();
            return allTaskLists;
        }

        public void SelectList(String listName)
        {
            bool listSelected = false;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            foreach (var task in GetAllTaskLists())
            {
                // IWebElement taskTab = Driver.FindElement(By.CssSelector("#tasklist li .task-title"));
                if (task.Text == listName)
                {
                    Console.WriteLine($"click on {listName}");
                    Click(task);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#newtask_adv")));
                    listSelected = true;
                }
            }
            if(listSelected == false)
            {
                Console.WriteLine($"no list found by the name of {listName}, creating a new list by that name");
                AddNewTaskList(listName);
                foreach (var task in GetAllTaskLists())
                {
                    // IWebElement taskTab = Driver.FindElement(By.CssSelector("#tasklist li .task-title"));
                    if (task.Text == listName)
                    {
                        Console.WriteLine($"click on {listName}");
                        Click(task);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#newtask_adv")));
                        listSelected = true;
                    }
                }
            }
        }



        public IWebElement? FindTaskListByName(string taskName)
        {
            List<IWebElement> TaskList = GetAllTaskLists();
            if(TaskList.Count>1)
            {
                foreach (IWebElement list in TaskList)
                {
                    string taskText = Driver.FindElement(By.CssSelector("#lists .title")).Text;
                    if (list.Text == taskName)
                    {
                        return list;
                    }
                }
            }
            return null;
        }

        public IWebElement? FindTaskByName(string taskName)
        {
            List<IWebElement> allTasks = GetAllTasks();
            if (allTasks.Count > 1)
            {
                foreach (IWebElement task in allTasks)
                {
                    string taskText = Driver.FindElement(By.CssSelector($"#tasklist li .task-title li .title")).Text;
                    if (task.Text == taskName)
                    {
                        return task;
                    }
                }
            }
            return null;
        }

        public void AddNewTaskList(string taskListName)
        {
            Click(Driver.FindElement(By.CssSelector(".mtt-tabs-new-button")));
            FillText(Driver.FindElement(By.CssSelector("#modalTextInput")), taskListName);
            Click(Driver.FindElement(By.CssSelector("#btnModalOk")));
        }

        public void DeleteTaskList(string taskListName)
        {
            Actions actions = new Actions(Driver);
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            actions.ContextClick(FindTaskListByName(taskListName)).Perform();
            Click(Driver.FindElement(By.CssSelector("#listmenucontainer #btnDeleteList")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".modal-box")));
            Click(Driver.FindElement(By.CssSelector("#btnModalOk")));
        }

        public bool IsTaskCompleted(string taskName)
        {
            List<IWebElement> TaskList = GetAllTasks();
            if (TaskList.Count > 0)
            {
                try
                {
                    if (FindTaskListByName(taskName) != null)
                    {
                        IWebElement checkedLine = Driver.FindElement(By.CssSelector(".title - block.title li.title [checked='checked']"));
                        if (checkedLine != null)
                        {
                            return true;
                        }
                    }
                }
                catch(NullReferenceException)
                {
                    return false;
                }
            }

             return false;
        }

        public List<IWebElement> SearchITemsInGeneralList(string searchText) 
        {
            List<IWebElement> TaskList = new List<IWebElement> ();
            try
            {

                Actions actions = new Actions(Driver);
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                Console.WriteLine("click on 'All tasks'");
                TaskList = GetAllTaskLists();
                Click(TaskList[0]);
                Thread.Sleep(1000);
                Console.WriteLine("enter search string in search field");
                Click(Driver.FindElement(By.CssSelector("#search")));
                FillText(Driver.FindElement(By.CssSelector("#search")), searchText);
                List<IWebElement> allTasks = GetAllTasks();
               
                return allTasks;
                
            }
            catch(Exception ex)
            {
                Console.WriteLine($"element not found {ex.Message}");
                return TaskList;
            }
        }

        public List<IWebElement> SearchITemsInSpecificList(string listName, string searchText)
        {
            List<IWebElement> allTasks = new List<IWebElement> ();
            try
               { 
                Actions actions = new Actions(Driver);
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                GetAllTaskLists().FirstOrDefault(taskList => taskList.Text.Contains(listName));
                FillText(Driver.FindElement(By.CssSelector("#search")), searchText);
                actions.SendKeys(Keys.Enter).Perform();
                allTasks = GetAllTasks();

                return allTasks;
            }
                
            catch(ArgumentNullException ex)
            {
                Console.WriteLine($"element not found {ex.Message}");
                return allTasks;
            }
        }

        public void AddTAsk (string taskName, string listName)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".title-block")));
            bool listExists = false;
            foreach (var task in GetAllTaskLists()) 
            {
                if (task.Text == listName)
                {
                    listExists = true;
                    Console.WriteLine($"click on {listName}");
                    Click(task);
                    Console.WriteLine($"enter text: {taskName}");
                    FillText(Driver.FindElement(By.Id("task")), taskName);
                    Click(Driver.FindElement(By.Id("newtask_submit")));
                    break;
                }
               
            }
            if(!listExists)
            {
                    AddNewTaskList(listName);
                    Driver.Navigate().Refresh();
                    Thread.Sleep(5000);
                    var task = FindTaskListByName(listName);
                    if (task != null) 
                    {
                        Console.WriteLine($"click on {listName}");
                        Click(task);
                        Console.WriteLine($"enter text: {taskName}");
                        FillText(Driver.FindElement(By.Id("task")), taskName);
                    Click(Driver.FindElement(By.Id("newtask_submit")));
                    }
                    
            }
            
        }

    }
}
