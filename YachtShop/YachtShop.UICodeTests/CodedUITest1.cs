using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace YachtShop.UICodeTests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        [TestMethod]
        public void CheckCorrectLogInAsAdmin()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            this.UIMap.RunHomePage();
            this.UIMap.ClickLogInTabButton();
            this.UIMap.AddAdminEmail();
            this.UIMap.AddCorrectPassword();
            this.UIMap.ClickLogInButton();
            this.UIMap.ValidateWelcomeStringAfterLogin();
            this.UIMap.LogOut();
        }


        [TestMethod]
        public void Admin_CanAddNewSeller()
        {
            this.UIMap.RunHomePage();
            this.UIMap.ClickLogInTabButton();
            this.UIMap.AddAdminEmail();
            this.UIMap.AddCorrectPassword();
            this.UIMap.ClickLogInButton();
            this.UIMap.NavigateToSellerIndexPage();
            this.UIMap.ClickCreateNewSeller();
            this.UIMap.AddCorrectSeller();
            this.UIMap.ClickCreateNewSellerButton();
            this.UIMap.ValidateSellerFirstName();
            this.UIMap.ValidateSellerSecondName();
            this.UIMap.ValidateSellerSalary();
            this.UIMap.ClickDetailSellerButton();
            this.UIMap.VerifyDetailPage();
            this.UIMap.ClickEditSellerButton();
            this.UIMap.EditSellerFirstName();
            this.UIMap.SaveEditSeller();
            this.UIMap.VerifySellerFirstNameAfterEdit();
            this.UIMap.ClieckSellerDeleteButton();
            this.UIMap.ClickDeleteSellerButton();
            this.UIMap.LogOut();
        }
        [TestMethod]
        public void Admin_CheckRolePage()
        {
            this.UIMap.RunHomePage();
            this.UIMap.ClickLogInTabButton();
            this.UIMap.AddAdminEmail();
            this.UIMap.AddCorrectPassword();
            this.UIMap.ClickLogInButton();
            this.UIMap.ClickRoleTab();
            this.UIMap.ValidateRoleEmail();
            this.UIMap.ValidateRole();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void CreateYacht_CheckValidation()
        {
            this.UIMap.RunHomePage();
            this.UIMap.ClickLogInTabButton();
            this.UIMap.AddAdminEmail();
            this.UIMap.AddCorrectPassword();
            this.UIMap.ClickLogInButton();
            this.UIMap.ClickYachtTab();
            this.UIMap.ClickCreateNewYacht();
            this.UIMap.ClickCreateNewYachtButton();
            this.UIMap.ValidateNullYachtName();
            this.UIMap.ValidateNullYachtPrice();
            this.UIMap.ValidateNullYachtDescription();
            this.UIMap.PutWrongPrice();
            this.UIMap.ClickCreateNewYachtButton();
            this.UIMap.ValidateWrongPrice();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void NotLoggedUser_ShouldNavigateToLoginPage_WhenTypeSellerPage()
        {
            this.UIMap.GoToSellerPage();
            this.UIMap.ValidateLoginPageURL();
        }

        [TestMethod]
        public void NotLoggedUser_ShouldSeeCorrectTabs()
        {
            this.UIMap.RunHomePage();
            this.UIMap.ValidateTabsForNotLoggedUser();
        }

        [TestMethod]
        public void Admin_ShouldSeeAllTabs()
        {
            this.UIMap.RunHomePage();
            this.UIMap.ClickLogInTabButton();
            this.UIMap.AddAdminEmail();
            this.UIMap.AddCorrectPassword();
            this.UIMap.ClickLogInButton();
            this.UIMap.ValidateTabsForAdmin();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void CreateClient_CheckWrongValidation()
        {
            this.UIMap.RunHomePage();
            this.UIMap.ClickLogInTabButton();
            this.UIMap.AddAdminEmail();
            this.UIMap.AddCorrectPassword();
            this.UIMap.ClickLogInButton();
            this.UIMap.ClickClientTab();
            this.UIMap.ClickCreateNewClient();
            this.UIMap.ClickCreateClientButton();
            this.UIMap.ValidateNullClientFirstName();
            this.UIMap.ValidateNullClientSecondName();
            this.UIMap.ValidateNullClientPhoneNumber();
            this.UIMap.ValidateNullClientEmail();
            this.UIMap.TypeWronNumberWithoutDash();
            this.UIMap.ClickCreateClientButton();
            this.UIMap.ValidateIncorrectPhoneNumber();
            this.UIMap.TypeWrongEmail();
            this.UIMap.ClickCreateClientButton();
            this.UIMap.ValidateIncorrectEmail();
            this.UIMap.LogOut();
        }

        [TestMethod]
        public void CreateClient_CheckCorrectValidation()
        {
            this.UIMap.RunHomePage();
            this.UIMap.ClickLogInTabButton();
            this.UIMap.AddAdminEmail();
            this.UIMap.AddCorrectPassword();
            this.UIMap.ClickLogInButton();
            this.UIMap.ClickClientTab();
            this.UIMap.ClickCreateNewClient();
            this.UIMap.AddCorrectClientData();
            this.UIMap.ClickCreateClientButton();
            this.UIMap.ValidateClientIndexPage();
            this.UIMap.DeleteClient();
            this.UIMap.LogOut();
        }


        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
