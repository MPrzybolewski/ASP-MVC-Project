using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace YachtShop.Tests
{
    public class SeleniumTests 
    {
        [Fact]
        public void HomeViewPartial_ShouldReturnCorrectString_WhenUserIsNotLoggedIn()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/";
            driver.Navigate().GoToUrl(url);

            var welcomeString = driver.FindElement(By.Id("WelcomeString")).Text;

            Assert.Equal("Welcome in my application!", welcomeString);
            driver.Close();
        }

        [Fact]
        public void HomeView_ShouldRunLoginPage_WhenUserClickLogin()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/";
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.Id("loginButton")).Click();

            var titleString = driver.FindElement(By.Id("loginPageTitle")).Text;

            Assert.Equal("Log in", titleString);
            driver.Close();
        }

        [Fact]
        public void LoginView_ShouldShowCorrectString_WhenUserPassNullEmail()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.Id("loginButtonForm")).Click();

            var titleString = driver.FindElement(By.Id("wrongEmail")).Text;

            Assert.Equal("The Email field is required.", titleString);
            driver.Close();
        }
    }
}
