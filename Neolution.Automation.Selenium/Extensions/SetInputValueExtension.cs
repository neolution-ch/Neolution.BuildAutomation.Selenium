namespace Neolution.Automation.Selenium.Extensions
{
    using System;
    using Neolution.Automation.Selenium.Exceptions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    /// <summary>
    /// The SetInputValue extension
    /// </summary>
    public static class SetInputValueExtension
    {
        /// <summary>
        /// Sets the value of a HTML input element.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="text">The text.</param>
        /// <exception cref="ArgumentNullException">driver</exception>
        /// <exception cref="ElementActionException">SendKeys</exception>
        public static void SetInputValue(this IWebDriver driver, By selector, string text)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            var element = driver.SelectElement(selector, WaitCondition.ElementClickable);

            try
            {
                element.Clear();
                element.SendKeys(text);
            }
            catch (StaleElementReferenceException)
            {
                RetrySetInputValueOnStaleElementException(driver, selector, text);
            }
            catch (ElementNotInteractableException)
            {
                RetrySetInputValueOnElementNotInteractableException(driver, selector, element);
            }
        }

        /// <summary>
        /// Retries to set input value after <see cref="StaleElementReferenceException"/>.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="text">The text.</param>
        private static void RetrySetInputValueOnStaleElementException(ISearchContext driver, By selector, string text)
        {
            try
            {
                // Retry by selecting the element again.
                var element = driver.FindElement(selector);
                if (element == null)
                {
                    throw new ElementNotFoundException(selector);
                }

                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception innerEx)
            {
                throw new ElementActionException(selector, $"Retry after {nameof(StaleElementReferenceException)} failed!", innerEx);
            }
        }

        /// <summary>
        /// Retries to set input value after <see cref="ElementNotInteractableException"/>.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="element">The element.</param>
        private static void RetrySetInputValueOnElementNotInteractableException(IWebDriver driver, By selector, IWebElement element)
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
                throw new ElementActionException(selector, $"Retry after {nameof(ElementNotInteractableException)} failed!", ex);
            }
        }
    }
}
