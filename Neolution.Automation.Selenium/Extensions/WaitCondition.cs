namespace Neolution.Automation.Selenium.Extensions
{
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// Wait conditions for selenium. Roughly translates to <see cref="ExpectedConditions"/>
    /// </summary>
    public enum WaitCondition
    {
        /// <summary>
        ///  Wait until the element is clickable
        /// </summary>
        ElementClickable = 0,

        /// <summary>
        /// Wait until the element exists in the DOM
        /// </summary>
        ElementExists = 1,

        /// <summary>
        ///  Wait until the element is visible
        /// </summary>
        ElementVisible = 2,
    }
}
