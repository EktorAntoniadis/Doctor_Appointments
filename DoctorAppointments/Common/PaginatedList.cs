using DoctorAppointments.Models;
using System.Numerics;

namespace DoctorAppointments.Common
{
    public class PaginatedList<T> where T : class
    {
        public List<T> Records { get; set; }

        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> records, int totalCount, int pageIndex, int pageSize)
        {
            Records = records;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)TotalCount / PageSize);
        }
    }
}