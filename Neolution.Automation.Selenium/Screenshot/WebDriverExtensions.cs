namespace Neolution.Automation.Selenium.Screenshot
{
    using System;
    using System.Globalization;
    using OpenQA.Selenium;

    /// <summary>
    /// WebDriver extensions to interact more resilient with Selenium.
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Uses the web driver to take a screenshot.
        /// </summary>
        /// <param name="driver">The web driver.</param>
        /// <returns>The path to the screenshot image file.</returns>
        public static string TakeScreenshot(this IWebDriver driver)
        {
            var uniqueName = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture).Substring(0, 8).ToUpperInvariant();
            return SeleniumScreenshot.Instance.TakeScreenshot(driver, uniqueName);
        }

        /// <summary>
        /// Uses the web driver to take a screenshot.
        /// </summary>
        /// <param name="driver">The web driver.</param>
        /// <param name="name">The name of the screenshot image file.</param>
        /// <returns>The path to the screenshot image file.</returns>
        public static string TakeScreenshot(this IWebDriver driver, string name)
        {
            return SeleniumScreenshot.Instance.TakeScreenshot(driver, name);
        }
    }
}
