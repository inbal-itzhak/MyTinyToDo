using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;

namespace MyTinyTodo.PageObjects
{
    public class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            Driver = driver;
           
        }

        public IWebDriver Driver { get; set; }

       public void FillText(IWebElement element, String text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public void Click(IWebElement el)
        {
            Thread.Sleep(1000);
            el.Click();
        }

    }
}
