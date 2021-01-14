using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Theta.Models;
using Theta.Models.Entities;

namespace Theta.Interfaces
{
    interface INodeDatabase
    {
        Task SaveNode(NodeModel nodeModel);
        Task DeleteNode(NodeModel nodeModel);
        Task<NodeModel> GetNodeByExpression(Expression<Func<NodeDatabaseModel, bool>> predExpr);
        Task<List<NodeModel>> GetNodes();
        Task<List<NodeModel>> GetNodesByExpression(Expression<Func<NodeDatabaseModel, bool>> predExpr);
    }
}
