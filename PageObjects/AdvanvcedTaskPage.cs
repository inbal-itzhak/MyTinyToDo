using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V128.DOM;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MyTinyTodo.PageObjects
{
    public class AdvanvcedTaskPage : BasePage
    {
        public AdvanvcedTaskPage(IWebDriver driver) : base(driver)
        {
        }

        public void OpenAdvancedTaskPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Click(Driver.FindElement(By.CssSelector("#newtask_adv")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".page-title.mtt-inadd")));
        }

        public void EnterTaskName(string name)
        {
            FillText(Driver.FindElement(By.CssSelector("#taskedit_form [name='task']")), name);
        }

        public void EnterNotes(string notes)
        {
            FillText(Driver.FindElement(By.CssSelector("#taskedit_form [name='note']")), notes);

        }

        public void EnterTag(string tag)
        {
            FillText(Driver.FindElement(By.CssSelector("#taskedit_form [name='tags']")), tag);
        }


        public void SelectPriority(int priority)
        {
            IWebElement Priority = Driver.FindElement(By.CssSelector("[name='prio']"));
            SelectElement selectPriority = new SelectElement(Priority);
            switch (priority)
            {
                case < -1:
                    Console.WriteLine($"priority value must be between -1 t0 2, your value {priority} was changed to -1");
                    priority = -1;
                    break;
                case > 2:
                    Console.WriteLine($"priority value must be between -1 t0 2, your value {priority} was changed to 2");
                    priority = 2;
                    break;
                default:
                    break;
            }
            selectPriority.SelectByValue($"{priority}");
        }

        public void EnterDueDateManually(string date)
        {
            FillText(Driver.FindElement(By.CssSelector("#duedate")), date);
        }

        public void SelectDueDateFromCalanfer(double year, string month, int day)
        {
            try
            {
                
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                Click(Driver.FindElement(By.CssSelector(".ui-datepicker-trigger")));
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".ui-datepicker-calendar")));
                IWebElement months = Driver.FindElement(By.CssSelector(".ui-datepicker-month"));
                SelectElement selectMonth = new SelectElement(months);
                selectMonth.SelectByText(month);
                //selectMonth.SelectByValue(month.ToString());
                IWebElement years = Driver.FindElement(By.CssSelector(".ui-datepicker-year"));
                SelectElement selectYear = new SelectElement(years);
                selectYear.SelectByValue(year.ToString());
                Click(Driver.FindElement(By.CssSelector($"[data-handler='selectDay'] [data-date='{day}']")));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"date {day}\\{month}\\{year} is not a valid date, selected today instead");
                Click(Driver.FindElement(By.CssSelector("[class=' ui-datepicker-days-cell-over  ui-datepicker-today']")));
            }
           

        }

        public void SaveAdvancedTask()
        {
            Click(Driver.FindElement(By.CssSelector("#taskedit_form [type='submit']")));
        }
    }
}
