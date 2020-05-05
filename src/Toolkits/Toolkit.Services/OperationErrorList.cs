using System.Collections.Generic;

namespace Toolkit.Services
{
    public class OperationErrorList : List<OperationError>
    {
        /// <summary>
        /// Adds the specified error code.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorDescription">The error description.</param>
        public void Add(string errorCode, string errorDescription)
        {
            this.Add(new OperationError
            {
                Code = errorCode,
                Description = errorDescription
            });
        }

        /// <summary>
        /// Adds the specified error code.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorDescription">The error description.</param>
        /// <param name="args">The arguments.</param>
        public void Add(string errorCode, string errorDescription, params object[] args)
        {
            this.Add(new OperationError
            {
                Code = errorCode,
                Description = string.Format(errorDescription, args)
            });
        }

        /// <summary>
        /// Adds the specified error code.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorDescription">The error description.</param>
        /// <param name="path">The path.</param>
        /// <param name="args">The arguments.</param>
        public void AddEx(string errorCode, string errorDescription, string path, params object[] args)
        {
            this.Add(new OperationError
            {
                Code = errorCode,
                Description = errorDescription,
                Path = path
            });
        }
    }
}