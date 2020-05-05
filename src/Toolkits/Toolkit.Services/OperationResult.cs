using System.Linq;

namespace Toolkit.Services
{
    public class OperationResult
    {
        private bool? success = null;

        public OperationResult()
        {
        }

        public OperationResult(bool success)
        {
            this.success = success;
        }

        public object Entity { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource was not found. If this property is
        /// not setted then if the property "Success" property is always FALSE.
        /// </summary>
        /// <value><c>true</c> if [not found]; otherwise, <c>false</c>.</value>
        public bool NotFound { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OperationResult"/> is success. If
        /// this property is not setted then if the property NotFound is TRUE or Errors collection
        /// has any error then this property is always FALSE.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success
        {
            get
            {
                if (success.HasValue)
                {
                    return success.Value;
                }

                return !Errors.Any() && !NotFound;
            }

            set
            {
                success = value;
            }
        }

        /// <summary>
        /// Gets errors collections. If this collection have one or more errors and the Success
        /// property is not setted then property Success is always FALSE.
        /// </summary>
        /// <value>The errors.</value>
        public OperationErrorList Errors { get; } = new OperationErrorList();

        public static OperationResult<T> Create<T>(T entity, bool success = false)
            where T : class
        {
            return new OperationResult<T>(entity, success);
        }
    }
}