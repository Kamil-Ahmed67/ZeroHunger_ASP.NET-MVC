using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZeroHunger.EF;

namespace ZeroHunger.DTO
{
    public class FoodDistributionDTO
    {
        public FoodDistributionDTO()
        {
            this.EmployeeFoodDistributions = new HashSet<EmployeeFoodDistribution>();
        }

        public int Distribution_Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public System.DateTime Time { get; set; }
        [Required]
        public int CollectRequest_Id { get; set; }
        [Required]
        public string Status { get; set; }

        public virtual CollectRequest CollectRequest { get; set; }
        public virtual ICollection<EmployeeFoodDistribution> EmployeeFoodDistributions { get; set; }
    }
}