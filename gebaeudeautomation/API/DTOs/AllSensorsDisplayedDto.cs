using System.ComponentModel.DataAnnotations;
using API.Data.Models;

namespace API.DTOs
{
    public class AllDisplayedSensorsDto
    {
        public IEnumerable<Sensor> Items { set; get; }
        public int TotalItems { set; get; }
        public int PageNumber { set; get; }
        public int PageSize { set; get; }
    }
}