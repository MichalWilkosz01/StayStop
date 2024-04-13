﻿
namespace StayStop.BLL.Pagination
{
    public class HotelPagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
