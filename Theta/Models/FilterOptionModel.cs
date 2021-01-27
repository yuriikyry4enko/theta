using System;
using System.Collections.Generic;
using Prism.Mvvm;
using Theta.Constants;

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

        public List<FilterOption> GetFilterItemsWithNames()
        {

            List<FilterOption> values = new List<FilterOption>();


            if (NodeType != null)
            {
                values.Add(new FilterOption
                {
                    Name = "Type",
                    Value = DictionariesConstants.NodeTypes[NodeType.Value],
                });
            }

            if (AssignedEmployeeId != null)
            {
                values.Add(new FilterOption
                {
                    Name = "Assigned to",
                    Value = DictionariesConstants.Employees[AssignedEmployeeId.Value],
                });
            }

            if (Status != null)
            {
                values.Add(new FilterOption
                {
                    Name = nameof(Status),
                    Value = DictionariesConstants.Statuses[Status.Value],
                });
            }

            if (Priority != null)
            {
                values.Add(new FilterOption
                {
                    Name = nameof(Priority),
                    Value = DictionariesConstants.Priorities[Priority.Value],
                });
            }


            return values;
        }
    }

    public class FilterOption : BindableBase
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string FullText
        {
            get
            {
                return $"{Name}: {Value}";
            }
        }
    }
}
