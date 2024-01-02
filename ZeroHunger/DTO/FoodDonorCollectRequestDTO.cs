using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZeroHunger.EF;

namespace ZeroHunger.DTO
{
    public class FoodDonorCollectRequestDTO
    {
        public FoodDonorCollectRequestDTO()
        {
            this.EmployeeCollectRequests = new HashSet<EmployeeCollectRequest>();
            this.FoodDistributions = new HashSet<FoodDistribution>();
        }

        public int CollectRequest_Id { get; set; }
        [Required]
        public string Food_Dscription { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public System.DateTime Expired_Time { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public System.DateTime Preffered_Time { get; set; }
        [Required]
        public int DonorId { get; set; }
        public string Status { get; set; }

        public virtual FoodDonor FoodDonor { get; set; }
        public virtual ICollection<EmployeeCollectRequest> EmployeeCollectRequests { get; set; }
        public virtual ICollection<FoodDistribution> FoodDistributions { get; set; }
    }
}
