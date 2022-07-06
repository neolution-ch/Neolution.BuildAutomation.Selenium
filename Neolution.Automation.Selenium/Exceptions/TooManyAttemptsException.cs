namespace Neolution.Automation.Selenium.Exceptions
{
    using System;
    using OpenQA.Selenium;

    /// <summary>
    /// The maximum attempts to locate an element have exceeded.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class TooManyAttemptsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyAttemptsException"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="maxAttempts">The maximum attempts.</param>
        public TooManyAttemptsException(By selector, int maxAttempts)
            : base($"Could not locate element after {maxAttempts} attempts. Selector: {selector}")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyAttemptsException" /> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="maxAttempts">The maximum attempts.</param>
        /// <param name="innerException">The inner exception.</param>
        public TooManyAttemptsException(By selector, int maxAttempts, Exception innerException)
            : base($"Could not locate element after {maxAttempts} attempts. Selector: {selector}", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyAttemptsException"/> class.
        /// </summary>
        public TooManyAttemptsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyAttemptsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TooManyAttemptsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyAttemptsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public TooManyAttemptsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyAttemptsException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        protected TooManyAttemptsException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
