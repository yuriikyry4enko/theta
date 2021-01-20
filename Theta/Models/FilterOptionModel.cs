using System;
namespace Theta.Models
{
    public class FilterOptionModel
    {
        public int? NodeType { get; set; }

        public int? AssignedEmployeeId { get; set; }

        public int? Status { get; set; }

        public int? Priority { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
