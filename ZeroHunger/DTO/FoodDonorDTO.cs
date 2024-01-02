using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.EF;

namespace ZeroHunger.DTO
{
    public class FoodDonorDTO
    {
        public FoodDonorDTO()
        {
            this.CollectRequests = new HashSet<CollectRequest>();
        }

        public int DonorId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<CollectRequest> CollectRequests { get; set; }
    }
}