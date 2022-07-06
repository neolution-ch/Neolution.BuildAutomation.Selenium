namespace Neolution.Automation.Selenium
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Globalization;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;

    /// <summary>
    /// The factory to create Selenium web driver instances.
    /// </summary>
    public static class WebDriverFactory
    {
        /// <summary>
        /// Creates the Firefox instance.
        /// </summary>
        /// <returns>The Firefox instance.</returns>
        public static FirefoxDriver CreateFirefox()
        {
            return CreateFirefox(new WebDriverOptions());
        }

        /// <summary>
        /// Creates the Firefox instance.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The chrome instance.</returns>
        /// <exception cref="ArgumentNullException">options</exception>
        [SuppressMessage("Minor Code Smell", "S4040: Strings should be normalized to uppercase", Justification = "The chrome parameter is lower-case.")]
        public static FirefoxDriver CreateFirefox(WebDriverOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            ConfigureDefaultWebDriverOptions(options);

            var firefoxOptions = new FirefoxOptions
            {
                AcceptInsecureCertificates = options.AcceptInsecureCertificates,
            };

            var webDriver = new FirefoxDriver(firefoxOptions);
            webDriver.Manage().Window.Size = options.WindowSize;

            return webDriver;
        }

        /// <summary>
        /// Creates the chrome instance.
        /// </summary>
        /// <returns>The chrome instance.</returns>
        public static ChromeDriver CreateChrome()
        {
            return CreateChrome(new WebDriverOptions());
        }

        /// <summary>
        /// Creates the chrome instance.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The chrome instance.</returns>
        /// <exception cref="ArgumentNullException">options</exception>
        [SuppressMessage("Minor Code Smell", "S4040: Strings should be normalized to uppercase", Justification = "The chrome parameter is lower-case.")]
        public static ChromeDriver CreateChrome(WebDriverOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            ConfigureDefaultWebDriverOptions(options);

            var chromeOptions = new ChromeOptions
            {
                AcceptInsecureCertificates = options.AcceptInsecureCertificates,
            };

            if (options.Headless)
            {
                chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--no-sandbox");
                chromeOptions.AddArgument("--disable-gpu"); // Needed: https://developers.google.com/web/updates/2017/04/headless-chrome#cli
            }

            if (options.AllowInsecureLocalhost)
            {
                chromeOptions.AddArgument("--allow-insecure-localhost");
            }

            if (options.IgnoreCertificateErrors)
            {
                chromeOptions.AddArgument("--ignore-certificate-errors");
            }

            if (!string.IsNullOrWhiteSpace(options.LanguageCode))
            {
                chromeOptions.AddArgument($"--lang={options.LanguageCode.ToLowerInvariant()}");
            }

            if (options.DisableSameOriginPolicy)
            {
                // https://stackoverflow.com/questions/3102819/disable-same-origin-policy-in-chrome
                chromeOptions.AddArgument("--disable-web-security");
            }

            if (options.AllowAccessFromLocalFiles)
            {
                // https://www.webmo.net/link/help/AccessingLocalFiles.html
                chromeOptions.AddArgument("--allow-file-access-from-files");
            }

            if (options.DisablePopupBlocking)
            {
                chromeOptions.AddUserProfilePreference("disable-popup-blocking", options.DisablePopupBlocking.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
            }

            if (options.DisableExtensions)
            {
                chromeOptions.AddUserProfilePreference("disable-extensions", options.DisableExtensions.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
            }

            if (options.DisablePdfViewer)
            {
                chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", options.DisablePdfViewer.ToString(CultureInfo.InvariantCulture).ToLowerInvariant());
            }

            if (!string.IsNullOrWhiteSpace(options.DefaultDownloadDirectory))
            {
                chromeOptions.AddUserProfilePreference("download.default_directory", options.DefaultDownloadDirectory);
            }

            var webDriver = new ChromeDriver(chromeOptions);
            webDriver.Manage().Window.Size = options.WindowSize;

            return webDriver;
        }

        /// <summary>
        /// Ensure some defaults are configured for the web driver options.
        /// </summary>
        /// <param name="options">The web driver options.</param>
        private static void ConfigureDefaultWebDriverOptions(WebDriverOptions options)
        {
            if (options.WindowSize.IsEmpty)
            {
                // Use a default window size if it was not set by user
                ConfigureDefaultWindowSize(options);
            }
        }

        /// <summary>
        /// Configures the default window size
        /// </summary>
        /// <param name="options">The web driver options.</param>
        private static void ConfigureDefaultWindowSize(WebDriverOptions options)
        {
            const int fullHdWidth = 1980;
            const int fullHdHeight = 1080;
            options.WindowSize = new Size(fullHdWidth, fullHdHeight);
        }
    }
}
