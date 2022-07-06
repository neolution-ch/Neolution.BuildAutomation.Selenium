namespace Neolution.Automation.Selenium.Extensions
{
    using System;
    using System.Threading;
    using Neolution.Automation.Selenium.Exceptions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

    /// <summary>
    /// WebDriver extensions to interact more resilient with Selenium.
    /// </summary>
    public static class ElementSelectionExtensions
    {
        /// <summary>
        /// Selects the web element with the specified selector. Waits until the element is clickable.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>The selected web element.</returns>
        public static IWebElement SelectElement(this IWebDriver driver, By selector)
        {
            return driver.SelectElement(selector, WaitCondition.ElementClickable);
        }

        /// <summary>
        /// Selects the element.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="waitCondition">The wait conditions.</param>
        /// <returns>The web element. </returns>
        /// <exception cref="ArgumentNullException">driver</exception>
        /// <exception cref="ArgumentOutOfRangeException">waitCondition - null</exception>
        /// <exception cref="ElementNotFoundException">The element could not be found.</exception>
        public static IWebElement SelectElement(this IWebDriver driver, By selector, WaitCondition waitCondition)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            // First try to wait with Selenium until the element meets the specified condition
            EnsureWaitUntilConditionIsFulfilled();

            try
            {
                // Then try to select the element
                var element = driver.AttemptToSelectElement(selector);

                // Very short wait just to make sure element meets the specified condition
                EnsureWaitUntilConditionIsFulfilled(1);

                return element;
            }
            catch (Exception ex)
            {
                throw new ElementNotFoundException(selector, ex);
            }

            void EnsureWaitUntilConditionIsFulfilled(int timeout = 10)
            {
                switch (waitCondition)
                {
                    case WaitCondition.ElementClickable:
                        driver.WaitUntilOrContinue(ExpectedConditions.ElementToBeClickable(selector), timeout);
                        break;
                    case WaitCondition.ElementExists:
                        driver.WaitUntilOrContinue(ExpectedConditions.ElementExists(selector), timeout);
                        break;
                    case WaitCondition.ElementVisible:
                        driver.WaitUntilOrContinue(ExpectedConditions.ElementIsVisible(selector), timeout);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(waitCondition), waitCondition, null);
                }
            }
        }

        /// <summary>
        /// Configures a wait with the specified timeout.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>The web driver wait.</returns>
        private static WebDriverWait Wait(this IWebDriver driver, int timeout = 10)
        {
            var timeSpan = TimeSpan.FromSeconds(timeout);
            return new WebDriverWait(driver, timeSpan);
        }

        /// <summary>
        /// Waits until the specified condition is met.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="timeout">The timeout.</param>
        private static void WaitUntilOrContinue(this IWebDriver driver, Func<IWebDriver, IWebElement> condition, int timeout = 10)
        {
            try
            {
                driver.Wait(timeout).Until(condition);
            }
            catch (WebDriverTimeoutException)
            {
                // Ignore timeout because the caller of this method may attempt to select the element later.
            }
        }

        /// <summary>
        /// Attempts to select element.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="maxAttempts">The maximum attempts.</param>
        /// <returns>The web element.</returns>
        /// <exception cref="TooManyAttemptsException">When the element could not be selected within the maximum specified amount of attempts.</exception>
        private static IWebElement AttemptToSelectElement(this ISearchContext driver, By selector, int maxAttempts = 10)
        {
            const int timeoutBetweenAttempts = 500;

            for (int i = 0; i < maxAttempts; i++)
            {
                try
                {
                    var element = driver.FindElement(selector);
                    if (element != null)
                    {
                        return element;
                    }
                }
                catch (Exception ex)
                {
                    // Defer exceptions until the last attempt.
                    if (i == maxAttempts - 1)
                    {
                        throw new TooManyAttemptsException(selector, maxAttempts, ex);
                    }
                }

                Thread.Sleep(timeoutBetweenAttempts);
            }

            throw new TooManyAttemptsException(selector, maxAttempts);
        }
    }
}
