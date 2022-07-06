namespace Neolution.Automation.Selenium.Extensions
{
    using System;
    using Neolution.Automation.Selenium.Exceptions;
    using OpenQA.Selenium;

    /// <summary>
    /// Extensions for Selenium web elements.
    /// </summary>
    public static class WebElementExtensions
    {
        /// <summary>
        /// Clicks the element with the JavaScript executor.
        /// </summary>
        /// <param name="element">The web element.</param>
        /// <param name="driver">The driver.</param>
        /// <exception cref="ArgumentNullException">driver</exception>
        /// <exception cref="ElementActionException">ExecuteScript => arguments[0].click();</exception>
        public static void ClickWithJsExecutor(this IWebElement element, IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            var executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
    }
}
