using System.Data;
using System.Data.Common;
using System.Linq;
using DbTool.Lib.Communication.DbCommands.Results;

namespace DbTool.Lib.Communication.DbCommands.DbSchema
{
    public class SchemaExecutor : IDbCommandExecutor
    {
        private readonly DbConnection _dbConnection;

        public SchemaExecutor(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IDbCommandResult Execute(string command)
        {
            try
            {
                _dbConnection.Open();
                var query = new SchemaQuery(command);
                var schema = GetSchemaFrom(query);
                var result = new QueryResult();

                foreach (DataColumn column in schema.Columns)
                {
                    if (query.SelectAll || query.ColumnNames.Contains(column.ColumnName.ToLowerInvariant()))
                    {
                        result.AddColumn(column.ColumnName, column.DataType);
                    }
                }
                foreach (DataRow row in schema.Rows)
                {
                    result.AddRow(row.ItemArray);
                }
                return result;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        private DataTable GetSchemaFrom(SchemaQuery query)
        {
            return query.HasNoCollectionName
                ? _dbConnection.GetSchema()
                : _dbConnection.GetSchema(query.CollectionName);
        }
    }
}