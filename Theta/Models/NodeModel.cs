using System;
namespace Theta.Models
{
    public class NodeModel
    {
        public int? LocalId { get; set; }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Project { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string NodeType { get; set; }

        public string Status { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Priority { get; set; }
    }
}
