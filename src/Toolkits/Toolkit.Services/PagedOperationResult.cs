namespace Toolkit.Services
{
    public class PagedOperationResult<T> : OperationResult<T>

        where T : class
    {
        public PagedOperationResult()
        {
        }

        public PagedOperationResult(int totalResults, int totalPages, int page, T dataModel, bool sucesss)
            : base(dataModel, sucesss)
        {
            this.Page = page;
            this.TotalResults = totalResults;
            this.TotalPages = totalPages;
        }

        public PagedOperationResult(int totalResults, int totalPages, int page, T dataModel)
            : base(dataModel)
        {
            this.Page = page;
            this.TotalResults = totalResults;
            this.TotalPages = totalPages;
        }

        /// <summary>
        /// Gets or sets the total results.
        /// </summary>
        /// <value>The total results.</value>
        public int TotalResults { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public int Page { get; set; }
    }
}