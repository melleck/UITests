using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UITests
{
    public static class WebDriverFactory
    {
        public static IWebDriver Build(BrowserType browserType)
        {
            return browserType switch
            {
                BrowserType.Chrome => GetChromeDriver(),
                BrowserType.HeadlessChrome => GetHeadlessChromeDriver(),
                _ => throw new ArgumentOutOfRangeException("No Such Browser Type Exists"),
            };
        }

        private static IWebDriver GetChromeDriver()
        {
            // Create chrome options to set prefernces
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications"); // to disable notifications

            // options.AddArguments("--disable-gpu"); // to disable gpu

            // Now initialize chrome driver with chrome options which will switch off this browser notification on the chrome browser
            // return new ChromeDriver(ResourcesDirectory, options, TimeSpan.FromSeconds(180));
            return new ChromeDriver(options);
        }

        private static IWebDriver GetHeadlessChromeDriver()
        {
            // Create chrome options to set prefernces
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications"); // to disable notifications
            options.AddArguments("--headless"); // to enable headless chrome

            // options.AddArguments("--no-sandbox"); // to disable sandbox
            // options.AddArguments("--window-size=1920,1080"); // use resolution for headless chrome
            options.AddArguments("--window-size=1920,1080");

            // options.AddArguments("--remote-debugging-port=9222"); // headless chrome enable remote debugging
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.download_restrictions", 0);
            options.AddUserProfilePreference("download.directory_upgrade", true);

            // options.AddUserProfilePreference("download.default_directory", ResourcesDirectory);
            options.AddUserProfilePreference("download.safebrowsing.enabled", false);
            options.AddUserProfilePreference("download.safebrowsing.disable_download_protection", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);
            options.AddUserProfilePreference("profile.default_content_settings.popups", 0);
            options.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);

            // options.AddArguments("--disable-gpu"); // to disable gpu

            // Now initialize chrome driver with chrome options which will switch off this browser notification on the chrome browser
            // return new ChromeDriver(ResourcesDirectory, options, TimeSpan.FromSeconds(180));
            
            return new ChromeDriver(options);
        }
    }
}
