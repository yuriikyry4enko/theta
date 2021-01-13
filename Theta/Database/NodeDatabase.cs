using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Theta.Interfaces;
using Theta.Models;
using Theta.Models.Entities;

namespace Theta.Database
{
    public class NodeDatabase : BaseDatabase, INodeDatabase
    {
        public async Task SaveNode(NodeModel nodeModel)
        {
            try
            {
                var databaseConnection = await GetDatabaseConnection<NodeDatabaseModel>().ConfigureAwait(false);
                var nodeDatabaseModel = NodeDatabaseModel.ToNodeDatabaseModel(nodeModel);

                await AttemptAndRetry(() => databaseConnection.InsertAsync(nodeDatabaseModel)).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task DeleteNode(NodeModel nodeModel)
        {
            try
            {
                var databaseConnection = await GetDatabaseConnection<NodeDatabaseModel>().ConfigureAwait(false);
                var nodeDatabaseModel = NodeDatabaseModel.ToNodeDatabaseModel(nodeModel);

                await AttemptAndRetry(() => databaseConnection.DeleteAsync(nodeDatabaseModel)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task<List<NodeModel>> GetNodes()
        {
            try
            {
                List<NodeModel> allNodes = new List<NodeModel>();

                var nodeDatabaseConnection = await GetDatabaseConnection<NodeDatabaseModel>().ConfigureAwait(false);
                var allNodesDatabaseModels = await AttemptAndRetry(() => nodeDatabaseConnection.Table<NodeDatabaseModel>().ToListAsync()).ConfigureAwait(false);

                foreach(var nodeDatabaseModel in allNodesDatabaseModels)
                {
                    allNodes.Add(NodeDatabaseModel.ToNodeModel(nodeDatabaseModel));
                }

                return allNodes;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }


            return null;
        }

        public async Task<List<NodeModel>> GetNodesByParentId(int parentId)
        {
            try
            {
                List<NodeModel> allNodes = new List<NodeModel>();

                var nodeDatabaseConnection = await GetDatabaseConnection<NodeDatabaseModel>().ConfigureAwait(false);
                var allNodesDatabaseModels = await AttemptAndRetry(() => nodeDatabaseConnection.Table<NodeDatabaseModel>().Where(x => x.ParentId != null && x.ParentId == parentId).ToListAsync()).ConfigureAwait(false);

                foreach (var nodeDatabaseModel in allNodesDatabaseModels)
                {
                    allNodes.Add(NodeDatabaseModel.ToNodeModel(nodeDatabaseModel));
                }

                return allNodes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }


            return null;
        }
    }
}
