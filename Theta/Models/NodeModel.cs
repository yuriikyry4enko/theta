using System;
using SQLite;
using Theta.Constants;

namespace Theta.Models
{
    public class NodeModel : ICloneable
    {
        public int? LocalId { get; set; }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Project { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? NodeType { get; set; }

        public int? AssignedEmployeeId { get; set; }

        public int? Status { get; set; }

        public int? Priority { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        [Ignore]
        public string Title
        {
            get
            {
                if (NodeType != null)
                    return $"type:{DictionariesConstants.NodeTypes[NodeType.Value]} title:{Name}";
                else
                    return string.Empty;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
