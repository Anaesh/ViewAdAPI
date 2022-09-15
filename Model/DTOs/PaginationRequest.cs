using System;
using System.Text.Json.Serialization;

namespace ViewAdAPI.Model.DTOs
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;

        public int Skip()
        {
            return (PageNumber - 1) * PageSize;
        }

        public int GetPageNumber()
        {
            return (PageNumber == 0 ? 1 : PageNumber);
        }
        public int GetPageSize()
        {
            return (PageSize == 0 ? 25 : PageSize);
        }
    }
}

