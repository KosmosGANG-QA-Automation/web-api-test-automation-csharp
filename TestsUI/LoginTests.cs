using AqaPortfolioProject.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace AqaPortfolioProject.TestsUI
{
    public class LoginTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;

        public LoginTests()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            // Инициализируем поле класса _driver с переданными опциями
            _driver = new ChromeDriver(options);

            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
            _loginPage = new LoginPage(_driver);
        }

        [Theory]
        [Trait("Category", "UI-Negative")]
        // На этом сайте при неверном входе выводится ошибка "Your username is invalid!"
        [InlineData("wronguser", "wrongpassword", "Your username is invalid!")]
        [InlineData("Nikita", "qwerty115", "Your username is invalid!")]
        [InlineData("Artur", "secret333", "Your username is invalid!")]
        public void Test_Invalid_Login_Should_Show_Correct_Error(string username, string password, string expectedError)
        {
            _loginPage.EnterUsername(username)
                      .EnterPassword(password)
                      .ClickSubmit();

            string actualError = _loginPage.GetErrorMessageText();
            Assert.Contains(expectedError, actualError);
        }

        [Fact]
        [Trait("Category", "UI-Positive")]
        public void Test_Valid_Login_Should_Be_Successful()
        {
            _loginPage.EnterUsername("tomsmith")
                      .EnterPassword("SuperSecretPassword!")
                      .ClickSubmit();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/secure"));

            Assert.Contains("/secure", _driver.Url);
        }

        public void Dispose()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}
