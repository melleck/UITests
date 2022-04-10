using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace UITests
{
    public class Tests
    {
        public static readonly string GogoleMapsURL = "https://www.google.com/maps";
        private IWebDriver Driver { get; set; }
        
        [SetUp]
        public void Setup()
        {
            this.Driver = WebDriverFactory.Build(BrowserType.Chrome);
            Console.Write("Google Chrome Started Successfully.\r\n");
            this.Driver.Manage().Window.Maximize();
            Console.WriteLine("Google Chrome Maximized");
        }

        [TestCase("Dublin", Description = "Search A City on Google Maps")]
        public void SearhCity(string cityName)
        {
            GoogleMaps googleMaps = new GoogleMaps(this.Driver);
            googleMaps.Open();
            Assert.IsTrue(googleMaps.IsOpen(), "Google Maps Opened Successfully.");
            googleMaps.SearchCity(cityName);
            Assert.IsTrue(googleMaps.CityFound(cityName), "{cityName} City Found on Google Maps?");
        }

        [TearDown]
        public void Teardown()
        {
            this.Driver.Quit();
            Console.WriteLine("\r\nGoogle Chrome Stopped Successfully");
        }
}
}