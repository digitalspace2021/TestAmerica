using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Dtos
{
    public class PagedListDto<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public object Lists { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

        public PagedListDto(List<T> items, int count, int pageNumber = 1, int pageSize = 10, object lists = null)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Lists = lists;

            AddRange(items);
        }

        public static PagedListDto<T> Create(IEnumerable<T> source, int pageNumber, int pageSize, object lists = null)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedListDto<T>(items, count, pageNumber, pageSize, lists);
        }
    }
}
