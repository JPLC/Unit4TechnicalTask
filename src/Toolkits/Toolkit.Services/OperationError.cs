namespace Toolkit.Services
{
    public class OperationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationError"/> class.
        /// </summary>
        public OperationError()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationError"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="code">The code.</param>
        /// <param name="path">The path.</param>
        public OperationError(string description, string code = null, string path = null)
        {
            Code = code;
            Description = description;
            Path = path;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>string</value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets for expressing a human readable message related to the current error which
        /// may be displayed to the user of the api.
        /// </summary>
        /// <value>string</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets for expressing a JSON Pointer (RFC6901) to a field in related resource
        /// (contained in the 'about' link relation) that this error is relevant for.
        /// </summary>
        /// <value>string</value>
        public string Path { get; set; }
    }
}