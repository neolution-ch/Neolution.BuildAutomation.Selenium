namespace Neolution.Automation.Selenium
{
    using System.Drawing;

    /// <summary>
    /// Options for the web driver instance.
    /// </summary>
    public sealed class WebDriverOptions
    {
        /// <summary>
        /// The HD resolution width (720p)
        /// </summary>
        private const int HdResolutionWidth = 1280;

        /// <summary>
        /// The HD resolution height (720p)
        /// </summary>
        private const int HdResolutionHeight = 720;

        /// <summary>
        /// Gets or sets the size of the window.
        /// </summary>
        /// <value>
        /// The size of the window.
        /// </value>
        public Size WindowSize { get; set; } = new Size(HdResolutionWidth, HdResolutionHeight);

        /// <summary>
        /// Gets or sets a value indicating whether the web driver is a headless instance.
        /// </summary>
        /// <value>
        ///   <c>true</c> if headless; otherwise, <c>false</c>.
        /// </value>
        public bool Headless { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow insecure requests to localhost.
        /// </summary>
        /// <value>
        ///   <c>true</c> if insecure requests to localhost are allowed; otherwise, <c>false</c>.
        /// </value>
        public bool AllowInsecureLocalhost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore certificate errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if certificate errors should be ignored; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreCertificateErrors { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether whether to accept insecure certificates.
        /// </summary>
        /// <value>
        ///   <c>true</c> if insecure certificates should be accepted; otherwise, <c>false</c>.
        /// </value>
        public bool AcceptInsecureCertificates { get; set; }

        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to disable the Same-Origin-Policy
        /// </summary>
        /// <value>
        ///   <c>true</c> if Same-Origin-Policy should be disabled; otherwise, <c>false</c>.
        /// </value>
        public bool DisableSameOriginPolicy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allows access to local files
        /// </summary>
        /// <value>
        ///   <c>true</c> if access to local files should be allowed; otherwise, <c>false</c>.
        /// </value>
        public bool AllowAccessFromLocalFiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to disable popup blocking.
        /// </summary>
        /// <value>
        ///   <c>true</c> if popup blocking should be disabled; otherwise, <c>false</c>.
        /// </value>
        public bool DisablePopupBlocking { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to disable extensions.
        /// </summary>
        /// <value>
        ///   <c>true</c> if extensions should be disabled; otherwise, <c>false</c>.
        /// </value>
        public bool DisableExtensions { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to disable the browser's internal PDF viewer.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the PDF viewer should be disabled; otherwise, <c>false</c>.
        /// </value>
        public bool DisablePdfViewer { get; set; }

        /// <summary>
        /// Gets or sets the default download directory.
        /// </summary>
        public string DefaultDownloadDirectory { get; set; }
    }
}
