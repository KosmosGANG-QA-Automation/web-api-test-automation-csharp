using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AqaPortfolioProject.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            // Объявляем умный таймер на 10 секунд
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Локаторы для сайта
        private By UsernameInput => By.Id("username");
        private By PasswordInput => By.Id("password");
        private By SubmitButton => By.CssSelector("button[type='submit']");
        private By FlashMessage => By.Id("flash");

        public LoginPage EnterUsername(string username)
        {
            // Ждем, пока поле логина станет видимым на экране, и только потом вводим текст
            var element = _wait.Until(ExpectedConditions.ElementIsVisible(UsernameInput));
            element.Clear();
            element.SendKeys(username);
            return this;
        }

        public LoginPage EnterPassword(string password)
        {
            // Ждем, пока поле пароля станет видимым на экране
            var element = _wait.Until(ExpectedConditions.ElementIsVisible(PasswordInput));
            element.Clear();
            element.SendKeys(password);
            return this;
        }

        public void ClickSubmit()
        {
            // Ждем, пока кнопка станет доступной для клика
            _wait.Until(ExpectedConditions.ElementToBeClickable(SubmitButton)).Click();
        }

        public string GetErrorMessageText()
        {
            // Ждем появления плашки с ошибкой
            var element = _wait.Until(ExpectedConditions.ElementIsVisible(FlashMessage));
            return element.Text;
        }
    }
}
