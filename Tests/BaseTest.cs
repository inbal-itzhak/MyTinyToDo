using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;

namespace MyTinyTodo.Tests
{
    public class BaseTest
    {
        public static readonly IConfigurationRoot config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile(path: "appsettings.json")
          .Build();

        public IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = config["baseUrl"];
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
