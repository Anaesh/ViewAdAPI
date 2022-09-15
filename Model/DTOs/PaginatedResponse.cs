using System;
namespace ViewAdAPI.Model.DTOs
{
    public class PaginatedResponse<T> where T : class
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPageNumber { get; set; }
        public int TotalPages { get; set; }
        public int? PreviousPage { get; set; }
        public int? NextPage { get; set; }
        public T Data { get; set; }

        public PaginatedResponse(int totalCount, T data, int currentPage, int pageSize)
        {
            TotalCount = totalCount;
            Data = data;
            CurrentPageNumber = currentPage;
            PageSize = pageSize;

            TotalPages = (int)Math.Ceiling((double)TotalCount / (double)PageSize);

            if (CurrentPageNumber > 1)
                PreviousPage = (CurrentPageNumber > TotalPages) ? TotalPages : CurrentPageNumber-1;
            if (CurrentPageNumber < TotalPages)
                NextPage = CurrentPageNumber+1;
            
        }
    }
}

