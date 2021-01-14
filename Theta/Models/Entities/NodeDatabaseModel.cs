using System;
using SQLite;

namespace Theta.Models.Entities
{
    public class NodeDatabaseModel 
    {
        [PrimaryKey]
        public int? LocalId { get; set; }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Project { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? NodeType { get; set; }

        public int? Status { get; set; }

        public int? Priority { get; set; }

        public int? AssignedEmployeeId { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public static NodeDatabaseModel ToNodeDatabaseModel(in NodeModel nodeModel) => new()
        {
            Name = nodeModel.Name,
            Project = nodeModel.Project,
            Description = nodeModel.Description,
            Id = nodeModel.Id,
            ParentId = nodeModel.ParentId,
            Status = nodeModel.Status,
            Priority = nodeModel.Priority,
            LocalId = nodeModel.LocalId,
            BeginDate = nodeModel.BeginDate,
            EndDate = nodeModel.EndDate,
            NodeType = nodeModel.NodeType,
            AssignedEmployeeId = nodeModel.AssignedEmployeeId,
        };

        public static NodeModel ToNodeModel(in NodeDatabaseModel nodeDatabaseModel) => new()
        {
            Name = nodeDatabaseModel.Name,
            Project = nodeDatabaseModel.Project,
            Description = nodeDatabaseModel.Description,
            Id = nodeDatabaseModel.Id,
            ParentId = nodeDatabaseModel.ParentId,
            Status = nodeDatabaseModel.Status,
            Priority = nodeDatabaseModel.Priority,
            LocalId = nodeDatabaseModel.LocalId,
            BeginDate = nodeDatabaseModel.BeginDate,
            NodeType = nodeDatabaseModel.NodeType,
            EndDate = nodeDatabaseModel.EndDate,
            AssignedEmployeeId = nodeDatabaseModel.AssignedEmployeeId,

        };
    }
}
