using API.Data.Models;

namespace API.DTOs
{
    public class AllAktorsDTO
    {
        public IEnumerable<Aktor> Items { set; get; }
        public int TotalItems { set; get; }
        public int PageNumber { set; get; }
        public int PageSize { set; get; }
    }
}