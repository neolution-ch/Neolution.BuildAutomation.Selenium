﻿namespace Neolution.Automation.Selenium.Exceptions
{
    using System;
    using OpenQA.Selenium;

    /// <summary>
    /// Exception to indicate an action upon an element went wrong.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class ElementActionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementActionException"/> class.
        /// </summary>
        /// <param name="selector">The selector.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="innerException">The inner exception.</param>
        public ElementActionException(By selector, string actionName, Exception innerException)
            : base($"{actionName}: {selector}", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementActionException"/> class.
        /// </summary>
        public ElementActionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementActionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ElementActionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementActionException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ElementActionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementActionException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization information.</param>
        /// <param name="streamingContext">The streaming context.</param>
        protected ElementActionException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
