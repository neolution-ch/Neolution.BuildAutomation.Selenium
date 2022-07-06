namespace Neolution.Automation.Selenium.Extensions
{
    using System;
    using Neolution.Automation.Selenium.Exceptions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    /// <summary>
    /// WebDriver shorthand extensions to interact more resilient with Selenium.
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Clicks the element.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <exception cref="ElementActionException">ContextClick</exception>
        public static void ClickElement(this IWebDriver driver, By selector)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            var element = driver.SelectElement(selector, WaitCondition.ElementClickable);

            try
            {
                try
                {
                    element.Click();
                }
                catch (StaleElementReferenceException)
                {
                    try
                    {
                        // The element reference got stale. Retry by finding and clicking the element again.
                        driver.FindElement(selector).Click();
                    }
                    catch (Exception ex)
                    {
                        throw new ElementActionException(selector, $"{nameof(StaleElementReferenceException)} -> Click", ex);
                    }
                }
                catch (ElementNotInteractableException)
                {
                    try
                    {
                        // Try to move the browser window to the element
                        var actions = new Actions(driver);
                        actions.MoveToElement(element).Perform();

                        // Re-select the element (because we just changed our view scope to it) and then click it again.
                        element = driver.FindElement(selector);
                        element.Click();
                    }
                    catch (Exception)
                    {
                        // Ignore exception, because the click will now be forced with the JS executor
                    }

                    try
                    {
                        element.ClickWithJsExecutor(driver);
                    }
                    catch (Exception ex)
                    {
                        throw new ElementActionException(selector, $"{nameof(ElementNotInteractableException)} -> ClickWithJsExecutor", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ElementActionException(selector, "Click", ex);
            }
        }

        /// <summary>
        /// Right-clicks the element.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <exception cref="ElementActionException">ContextClick</exception>
        public static void RightClickElement(this IWebDriver driver, By selector)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            var element = driver.SelectElement(selector, WaitCondition.ElementClickable);

            try
            {
                try
                {
                    var actions = new Actions(driver);
                    actions.ContextClick(element).Perform();
                }
                catch (StaleElementReferenceException)
                {
                    // Retry by selecting the element again.
                    element = driver.FindElement(selector);

                    var actions = new Actions(driver);
                    actions.ContextClick(element).Perform();
                }
            }
            catch (Exception ex)
            {
                throw new ElementActionException(selector, "ContextClick", ex);
            }
        }

        /// <summary>
        /// Sends keys to element to simulate keyboard typing.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="text">The text of keys to send to the specified element.</param>
        /// <exception cref="ArgumentNullException">driver - null</exception>
        /// <exception cref="ElementActionException">SendKeys</exception>
        public static void SendKeysToElement(this IWebDriver driver, By selector, string text)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            var element = driver.SelectElement(selector, WaitCondition.ElementClickable);

            try
            {
                try
                {
                    element.SendKeys(text);
                }
                catch (StaleElementReferenceException)
                {
                    element = driver.FindElement(selector);
                    if (element == null)
                    {
                        throw new ElementNotFoundException(selector);
                    }

                    element.SendKeys(text);
                }
            }
            catch (Exception ex)
            {
                throw new ElementActionException(selector, "SendKeys", ex);
            }
        }
    }
}
