using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Theta.Interfaces;
using Theta.Models;
using Theta.Models.Entities;

namespace Theta.Database
{
    public class NodeDatabase : BaseDatabase, INodeDatabase
    {
        public async Task<List<NodeModel>> GetNodesByFilterOptions(List<string> filterOptions)
        {
            try
            {
                List<NodeModel> allNodes = new List<NodeModel>();
                StringBuilder filterNodeDatabase = null;

                var databaseConnection = await GetDatabaseConnection<NodeDatabaseModel>().ConfigureAwait(false);

                if(filterOptions != null)
                {
                    filterNodeDatabase = new StringBuilder();

                    for (int i = 0; i < filterOptions.Count; i++)
                    {
                        filterNodeDatabase.Append($"{filterOptions[i]}");

                        if(i != filterOptions.Count - 1)
                        {
                            filterNodeDatabase.Append(" AND ");
                        }
                    }
                }

                var result = await AttemptAndRetry(() =>
                    databaseConnection
                    .QueryAsync<NodeDatabaseModel>(filterOptions == null ?
                        $"SELECT * FROM NodeDatabaseModel" :
                        $"SELECT * FROM NodeDatabaseModel WHERE {filterNodeDatabase.ToString()}"))
                    .ConfigureAwait(false);

                foreach (var nodeDatabaseModel in result)
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


        public async Task SaveNode(NodeModel nodeModel)
        {
            try
            {
                var databaseConnection = await GetDatabaseConnection<NodeDatabaseModel>().ConfigureAwait(false);
                var nodeDatabaseModel = NodeDatabaseModel.ToNodeDatabaseModel(nodeModel);

                if(nodeModel.LocalId == null)
                    await AttemptAndRetry(() => databaseConnection.InsertAsync(nodeDatabaseModel)).ConfigureAwait(false);
                else
                    await AttemptAndRetry(() => databaseConnection.UpdateAsync(nodeDatabaseModel)).ConfigureAwait(false);
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

                await AttemptAndRetry(() => databaseConnection
                    .QueryAsync<NodeDatabaseModel>($"DELETE FROM NodeDatabaseModel WHERE {nameof(nodeDatabaseModel.ParentId)} = {nodeDatabaseModel.LocalId}"))
                    .ConfigureAwait(false);

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

        public async Task<List<NodeModel>> GetNodesByExpression(Expression<Func<NodeDatabaseModel, bool>> predExpr)
        {
            try
            {
                List<NodeModel> allNodes = new List<NodeModel>();

                var nodeDatabaseConnection = await GetDatabaseConnection<NodeDatabaseModel>().ConfigureAwait(false);
                var allNodesDatabaseModels = await AttemptAndRetry(() => nodeDatabaseConnection.Table<NodeDatabaseModel>().Where(predExpr).ToListAsync()).ConfigureAwait(false);

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

        public async Task<NodeModel> GetNodeByExpression(Expression<Func<NodeDatabaseModel, bool>> predExpr)
        {
            try
            {
                var nodeDatabaseConnection = await GetDatabaseConnection<NodeDatabaseModel>().ConfigureAwait(false);
                var nodeDatabaseModels = await AttemptAndRetry(() => nodeDatabaseConnection.GetAsync<NodeDatabaseModel>(predExpr)).ConfigureAwait(false);

                return NodeDatabaseModel.ToNodeModel(nodeDatabaseModels);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }
    }
}
