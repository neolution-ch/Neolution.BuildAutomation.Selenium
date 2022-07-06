namespace Neolution.Automation.Selenium.Screenshot
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    /// <summary>
    /// Singleton to manage Selenium screenshots
    /// </summary>
    public sealed class SeleniumScreenshot
    {
        /// <summary>
        /// The lazy instance
        /// </summary>
        private static readonly Lazy<SeleniumScreenshot> Lazy = new Lazy<SeleniumScreenshot>(() => new SeleniumScreenshot());

        /// <summary>
        /// The working directory
        /// </summary>
        private readonly DirectoryInfo workingDirectory;

        /// <summary>
        /// The current identifier
        /// </summary>
        private int lastId = 1;

        /// <summary>
        /// Prevents a default instance of the <see cref="SeleniumScreenshot"/> class from being created.
        /// </summary>
        private SeleniumScreenshot()
        {
            // Make sure the directory we are working in exists.
            this.workingDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "__selenium_extensions", "screenshots"));
            if (!this.workingDirectory.Exists)
            {
                this.workingDirectory.Create();
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static SeleniumScreenshot Instance => Lazy.Value;

        /// <summary>
        /// Uses the web driver to take a screenshot.
        /// </summary>
        /// <param name="driver">The web driver.</param>
        /// <param name="name">The name of the screenshot image file.</param>
        /// <returns>The path to the screenshot image file.</returns>
        public string TakeScreenshot(IWebDriver driver, string name)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            // Replace invalid file name characters and blanks with an underscore
            var cleanName = string.Join("_", name.Split(Path.GetInvalidFileNameChars()));
            cleanName = Regex.Replace(cleanName, @"\s+", "_");

            var filePath = Path.Combine(this.workingDirectory.FullName, $"{this.lastId:0000}_{cleanName}.png");
            this.lastId++;

            var screenshot = ((ChromeDriver)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            return filePath;
        }
    }
}
