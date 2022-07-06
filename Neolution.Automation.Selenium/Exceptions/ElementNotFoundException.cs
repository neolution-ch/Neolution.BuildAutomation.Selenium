namespace Neolution.Automation.Selenium.Exceptions
{
    using System;
    using OpenQA.Selenium;

    /// <summary>
    /// The element could not be found.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class ElementNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        public ElementNotFoundException(By selector)
            : base($"Element not found: {selector}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="innerException">The inner exception.</param>
        public ElementNotFoundException(By selector, Exception innerException)
            : base($"Element not found: {selector}", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class.
        /// </summary>
        public ElementNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ElementNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ElementNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementNotFoundException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        protected ElementNotFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
