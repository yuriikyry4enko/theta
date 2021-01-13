using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theta.Models;

namespace Theta.Interfaces
{
    interface INodeDatabase
    {
        Task SaveNode(NodeModel nodeModel);
        Task DeleteNode(NodeModel nodeModel);
        Task<List<NodeModel>> GetNodes();
        Task<List<NodeModel>> GetNodesByParentId(int parentId);
    }
}
