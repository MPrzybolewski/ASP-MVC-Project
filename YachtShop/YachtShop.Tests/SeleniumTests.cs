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

        [Fact]
        public void LoginView_ShouldShowCorrectString_WhenUserPassWrongEmail()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.Id("Email")).SendKeys("asdfasdfsad");

            driver.FindElement(By.Id("loginButtonForm")).Click();

            var titleString = driver.FindElement(By.Id("wrongEmail")).Text;
            //jaki string
            Assert.Equal("The Email field is not a valid e-mail address.", titleString);
            driver.Close();
        }

        [Fact]
        public void LoginView_ShouldShowCorrectString_WhenUserPassPasswordNotBelongToEmail()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.Id("Email")).SendKeys("seller@seller.pl");

            driver.FindElement(By.Id("Password")).SendKeys("aaaaaa");

            driver.FindElement(By.Id("loginButtonForm")).Click();

            var titleString = driver.FindElement(By.Id("validationString")).Text;
            //jaki string
            Assert.Equal("Invalid login attempt.", titleString);
            driver.Close();
        }


        [Fact]
        public void HomePage_ShouldShowCorrectWelocmeString_WhenUserLogInCorrectly()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.Id("Email")).SendKeys("123@123.pl");

            driver.FindElement(By.Id("Password")).SendKeys("1234");
            driver.FindElement(By.Id("loginButtonForm")).Click();


            var titleString = driver.FindElement(By.Id("WelcomeString")).Text;
            //jaki string
            Assert.Contains("123@123.pl", titleString);
            driver.Close();
        }
        

        [Fact]
        public void HomePage_ShouldShowYachtTab_WhenUserIsNotLogged()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/";
            driver.Navigate().GoToUrl(url);

            var tabText = driver.FindElement(By.Id("yachtTab")).Text;
            Assert.Equal("Yachts", tabText);
            driver.Close();
        }

        [Fact]
        public void HomePage_ShouldNotShowClientTab_WhenUserIsNotLoggedAsASeller()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/";
            driver.Navigate().GoToUrl(url);
            
            Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.Id("sellerTab")));
            driver.Close();
        }

        [Fact]
        public void HomePage_ShouldShowSellerTab_WhenUserIsLoggedAsAdmin()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);
            Login(driver);

            var tabText = driver.FindElement(By.Id("sellerTab")).Text;
            Assert.Equal("Sellers", tabText);
            driver.Close();
        }

        [Fact]
        public void HomePage_ShouldNavigateToSellerView_WhenAdminClickSellerTab()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);
            Login(driver);

            driver.FindElement(By.Id("sellerTab")).Click();

            var title = driver.FindElement(By.Id("sellerPageIndexTitle")).Text;
            Assert.Equal("Seller", title);
            driver.Close();
        }

        [Fact]
        public void SellerIndexPage_ShouldShowCorrectTabs_WhenSellerClickSellerTab()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);
            Login(driver);

            driver.FindElement(By.Id("sellerTab")).Click();

            string[] tabsText = new string[5];
            tabsText[0] = driver.FindElement(By.Id("firstNameCol")).Text;
            tabsText[1] = driver.FindElement(By.Id("secondNameCol")).Text;
            tabsText[2] = driver.FindElement(By.Id("salaryCol")).Text;
            tabsText[3] = driver.FindElement(By.Id("phoneNumberCol")).Text;
            tabsText[4] = driver.FindElement(By.Id("emailCol")).Text;

            string[] expectedText = new string[]
            {
                "First Name",
                "Second Name",
                "Salary",
                "Phone Number",
                "Email"
            };


            Assert.Equal(expectedText, tabsText);
            driver.Close();
        }

        [Fact]
        public void SellerPageIndex_ShouldNavigateToSellerCreatePage_WhenAdminClickCreateButton()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);
            Login(driver);

            driver.FindElement(By.Id("sellerTab")).Click();

            driver.FindElement(By.Id("createButton")).Click();

            string[] tabsText = new string[5];
            tabsText[0] = driver.FindElement(By.Id("firstNameLabel")).Text;
            tabsText[1] = driver.FindElement(By.Id("secondNameLabel")).Text;
            tabsText[2] = driver.FindElement(By.Id("salaryLabel")).Text;
            tabsText[3] = driver.FindElement(By.Id("phoneNumberLabel")).Text;
            tabsText[4] = driver.FindElement(By.Id("emailLabel")).Text;

            string[] expectedText = new string[]
            {
                "First Name",
                "Second Name",
                "Salary",
                "Phone Number",
                "Email"
            };

            Assert.Equal(expectedText, tabsText);
            driver.Close();
        }

        [Fact]
        public void SellerPageCreate_ShouldShowAllForms_WhenAdminIsOnPage()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);
            Login(driver);

            driver.FindElement(By.Id("sellerTab")).Click();

            driver.FindElement(By.Id("createButton")).Click();

            string titleText = driver.FindElement(By.Id("firstNameLabel")).Text;
            Assert.Equal("First Name", titleText);
            driver.Close();
        }
        

       [Fact]
        public void SellerPageCreate_ShouldShowCorrectString_WhenAdminPutWrongSalary()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);
            Login(driver);

            driver.FindElement(By.Id("sellerTab")).Click();

            driver.FindElement(By.Id("createButton")).Click();

            driver.FindElement(By.Id("Salary")).SendKeys("test");
            driver.FindElement(By.Id("createButton")).Click();
            
            var text = driver.FindElement(By.Id("salaryTextDanger")).Text;

            Assert.Contains("not valid for Salary.", text);
            driver.Close();
        }

        [Fact]
        public void SellerPageCreate_ShouldShowCorrectString_WhenAdminPutNullToFirstName()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);
            Login(driver);

            driver.FindElement(By.Id("sellerTab")).Click();

            driver.FindElement(By.Id("createButton")).Click();

            driver.FindElement(By.Id("Salary")).SendKeys("test");
            driver.FindElement(By.Id("createButton")).Click();

            var text = driver.FindElement(By.Id("firstNameTextDanger")).Text;

            Assert.Contains("Enter First Name", text);
            driver.Close();
        }

        [Fact]
        public void SellerPageCreate_ShouldNavigateToIndexPage_WhenAdminPutCorrectData()
        {
            var driver = new ChromeDriver(Directory.GetCurrentDirectory());
            var url = "https://localhost:44373/account/login";
            driver.Navigate().GoToUrl(url);
            Login(driver);

            driver.FindElement(By.Id("sellerTab")).Click();

            driver.FindElement(By.Id("createButton")).Click();

            driver.FindElement(By.Id("FirstName")).SendKeys("test");
            driver.FindElement(By.Id("SecondName")).SendKeys("test");
            driver.FindElement(By.Id("Salary")).SendKeys("2000");
            driver.FindElement(By.Id("PhoneNumber")).SendKeys("123-123-123");
            driver.FindElement(By.Id("Email")).SendKeys("test@test.pl");

            driver.FindElement(By.Id("createButton")).Click();

            var text = driver.FindElement(By.Id("sellerPageIndexTitle")).Text;

            Assert.Contains("Seller", text);
            driver.Close();
        }

        void Login(ChromeDriver driver)
        {
            driver.FindElement(By.Id("Email")).SendKeys("123@123.pl");

            driver.FindElement(By.Id("Password")).SendKeys("1234");
            driver.FindElement(By.Id("loginButtonForm")).Click();
        }
    }
}
