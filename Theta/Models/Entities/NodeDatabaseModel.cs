using System;
using SQLite;

namespace Theta.Models.Entities
{
    class NodeDatabaseModel 
    {
        [PrimaryKey]
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

        };
    }
}
