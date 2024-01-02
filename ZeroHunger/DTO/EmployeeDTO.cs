using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZeroHunger.EF;

namespace ZeroHunger.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO()
        {
            this.EmployeeCollectRequests = new HashSet<EmployeeCollectRequest>();
            this.EmployeeFoodDistributions = new HashSet<EmployeeFoodDistribution>();
        }

        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }

        public virtual ICollection<EmployeeCollectRequest> EmployeeCollectRequests { get; set; }
        public virtual ICollection<EmployeeFoodDistribution> EmployeeFoodDistributions { get; set; }
    }
}