using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UITests
{
    public class GoogleMaps
    {
        
        public static readonly string GoogleMapsURL = "https://www.google.com/maps";
        public WebDriverWait WaitForElement { get; set; }
        public DefaultWait<IWebDriver> FluentWait { get; set; }
        private IWebDriver Driver { get; set; }

        private readonly int waitTimeOut = 15;
        private readonly By searchButtonLocator = By.Id("searchbox-searchbutton");
        private readonly By searchFieldLocator = By.Id("searchboxinput");
        private readonly By searchResultCityTitleLocator = By.CssSelector(".x3AX1-LfntMc-header-title-title > span:nth-child(1)");
        private IWebElement SearchButton => this.Driver.FindElement(this.searchButtonLocator);
        private IWebElement SearchField => this.Driver.FindElement(this.searchFieldLocator);
        private IWebElement SearchResultCityTitle => this.Driver.FindElement(this.searchResultCityTitleLocator);
        public GoogleMaps(IWebDriver driver)
        {
            this.Driver = driver;
            this.WaitForElement = new WebDriverWait(this.Driver, new TimeSpan(0, 0, this.waitTimeOut));
        }

        public bool? IsOpen()
        {
            try
            {
                bool isOpen = this.Driver.Url.Equals(GoogleMapsURL) && this.SearchButton.Displayed;

               Console.Write(
                    $"GoogleMaps => {GoogleMapsURL}\r\n" +
                    $"this.Driver.Url.Equals(GoogleMapsURL) => {this.Driver.Url.Equals(GoogleMapsURL)}\r\n" +
                    $"LoginButton.Displayed => {this.SearchButton.Displayed}");

                if (isOpen)
                {
                   Console.Write("\r\nLogin Button Displayed");
                }
                else
                {
                   Console.Write("Login Button Not Displayed");
                }

                return isOpen;
            }
            catch (Exception ex) when (ex is NoSuchElementException)
            {
                Console.Write(
                     $"Exception Message: {ex.Message}\r\n" +
                     $"Exception StackTrace: {ex.StackTrace}");
                return false;
            }
        }

        public bool? CityFound(string cityName)
        {
            try
            {
                this.WaitForElement.Until(ExpectedConditions.ElementIsVisible(this.searchResultCityTitleLocator));
                bool isFound = this.SearchResultCityTitle.Displayed && SearchResultCityTitle.Text.Equals(cityName);

                Console.Write(
                     $"SearchResultCityTitle.Displayed => {SearchResultCityTitle.Displayed}\r\n" +
                     $"tSearchResultCityTitle.Text => {SearchResultCityTitle.Text}\r\n" +
                     $"SearchResultCityTitle.Text.Equals({cityName}) => {SearchResultCityTitle.Text.Equals(cityName)}");

                if (isFound)
                {
                    Console.Write($"\r\n{cityName} City is Found");
                }
                else
                {
                    Console.Write($"{cityName} City is Not Found");
                }

                return isFound;
            }
            catch (Exception ex) when (ex is NoSuchElementException)
            {
                Console.Write(
                     $"Exception Message: {ex.Message}\r\n" +
                     $"Exception StackTrace: {ex.StackTrace}");
                return false;
            }
        }
        public void Open()
        {
            this.Driver.Navigate().GoToUrl(GoogleMapsURL);
            Console.WriteLine("Opening Google Maps");
        }
        public void SearchCity(string cityName)
        {
            this.WaitForElement.Until(ExpectedConditions.ElementToBeClickable(this.searchFieldLocator));
            Console.WriteLine("Enter Dublin in the Search Field");
            this.SearchField.SendKeys(cityName);
            Console.WriteLine("Click the Search Button");
            this.SearchButton.Click();
        }
    }
}