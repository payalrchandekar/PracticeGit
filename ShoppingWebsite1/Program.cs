using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace ShoppingWebsite1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/seleniumPractise/#/");
            string[] itemsneeded = { "Brocolli", "Cauliflower", "Beetroot" , "Mango" };
            driver.Manage().Window.Maximize();
            AddToCart(driver, itemsneeded); // calling add to cart method
            ApplyCoupen(driver);//calling applycopen method
            PlaceOrder(driver);//calling placeorder method


        }
        //Method to add items to cart
        public static void AddToCart(IWebDriver driver, string[] itemsneeded)
        {
            //Iterating and adding items you wants to add to cart
            for (int i = 0;i < itemsneeded.Length; i++)
            {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement searchBox = wait.Until(ExpectedConditions.ElementExists(By.XPath("//input[@class='search-keyword']")));
            searchBox.SendKeys(itemsneeded[i]);

             IWebElement searchBtton = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[@class='search-button']")));
             searchBtton.Click();
          
             Thread.Sleep(1000);
             IWebElement addCartButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='product-action']")));
             addCartButton.Click();
             searchBox.Clear();
            }
            
        }
        //Method to apply coupen code
        public static void ApplyCoupen(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            driver.FindElement(By.XPath("//img[@alt='Cart']")).Click();
            driver.FindElement(By.XPath("//button[text()='PROCEED TO CHECKOUT']")).Click();
            driver.FindElement(By.CssSelector("input.promocode")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.CssSelector("button.promoBtn")).Click();

            IWebElement promocodestatus = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("span.promoInfo")));
            string coupenStatus = promocodestatus.Text;
            Console.WriteLine("massege on status is " + coupenStatus);
            Assert.AreEqual(coupenStatus, "Code applied ..!");
            driver.FindElement(By.XPath("//button[text()='Place Order']")).Click();
        }
        //Method to place order

        public static void PlaceOrder(IWebDriver driver)
        {
            IWebElement options = driver.FindElement(By.TagName("select"));
            SelectElement s = new SelectElement(options);
            s.SelectByText("India");

            driver.FindElement(By.XPath("//input[@type='checkbox']")).Click();
            driver.FindElement(By.XPath("//button[contains(text(),'Proceed')]")).Click();
            string ordersuccess = driver.FindElement(By.XPath("//div[@class='wrapperTwo']")).Text;
            Console.WriteLine("massage on sccess page is " + ordersuccess);
            try
            { 
            Assert.AreEqual(ordersuccess, "Thank you, your order has been placed successfully\r\nYou'll be redirected to Home page shortly!!");
            }
            catch
            {
            Console.WriteLine("Both strings doesn not match");
            }

            Console.WriteLine("added changes to clone file");
            Console.WriteLine("added changes to clone file1");
            Console.WriteLine("added changes to clone file2");
            Console.ReadLine();

        }

    }
}
//Thank you, your order has been placed successfully You 'll be redirected to Home page shortly!!