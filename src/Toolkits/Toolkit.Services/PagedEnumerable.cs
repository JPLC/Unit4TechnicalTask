using System;
using System.Collections;
using System.Collections.Generic;

namespace Toolkit.Services
{
    /// <inheritdoc />
    /// <summary>PagedEnumerable class.</summary>
    /// <typeparam name="T">The type.</typeparam>
    public class PagedEnumerable<T> : IEnumerable<T>
    {
        private const int PagedSizeDefaultValue = 50;

        private readonly IEnumerable<T> source;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedEnumerable{T}"/> class.
        /// </summary>
        public PagedEnumerable()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedEnumerable{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortExpression">The sort expression.</param>
        public PagedEnumerable(IEnumerable<T> source, Func<int> totalRecords, int currentPage, int pageSize, string sortExpression = "")
        {
            if (currentPage <= 0)
            {
                currentPage = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = PagedSizeDefaultValue;
            }

            this.source = source;
            TotalRecords = totalRecords();
            TotalPages = TotalRecords > 0 ? (int)Math.Ceiling((decimal)TotalRecords / pageSize) : 0;
            CurrentPage = TotalPages >= currentPage ? currentPage : TotalPages;
            PageSize = pageSize;
            SortExpression = sortExpression;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedEnumerable{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortExpression">The sort expression.</param>
        public PagedEnumerable(IEnumerable<T> source, int totalRecords, int currentPage, int pageSize, string sortExpression = "")
        {
            if (pageSize <= 0)
            {
                pageSize = PagedSizeDefaultValue;
            }

            this.source = source;
            TotalRecords = totalRecords;
            TotalPages = TotalRecords > 0 ? (int)Math.Ceiling((decimal)TotalRecords / pageSize) : 0;
            CurrentPage = TotalPages >= currentPage ? currentPage : TotalPages;
            PageSize = pageSize;
            SortExpression = sortExpression;
        }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total records.
        /// </summary>
        /// <value>The total records.</value>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        /// <value>The sort expression.</value>
        /// <remarks>
        /// The expression (Ex: "id,-name"). Use minus (-) before field name to specify descending order.
        /// </remarks>
        public string SortExpression { get; set; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return source.GetEnumerator();
        }
    }
}
