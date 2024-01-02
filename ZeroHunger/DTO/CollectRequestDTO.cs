using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.EF;

namespace ZeroHunger.DTO
{
    public class CollectRequestDTO
    {
        public CollectRequestDTO()
        {
            this.EmployeeCollectRequests = new HashSet<EmployeeCollectRequest>();
            this.FoodDistributions = new HashSet<FoodDistribution>();
        }

        public int CollectRequest_Id { get; set; }
        public string Food_Dscription { get; set; }
        public int Quantity { get; set; }
        public System.DateTime Expired_Time { get; set; }
        public string Address { get; set; }
        public System.DateTime Preffered_Time { get; set; }
        public int DonorId { get; set; }
        public string Status { get; set; }

        public virtual FoodDonor FoodDonor { get; set; }
        
        public virtual ICollection<EmployeeCollectRequest> EmployeeCollectRequests { get; set; }
        public virtual ICollection<FoodDistribution> FoodDistributions { get; set; }
    }
}