using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class TripListingDTO
    {
        public int UserId { get; set; }
        public int TripId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Date { get; set; }
        public int Spending { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }

    }
}
