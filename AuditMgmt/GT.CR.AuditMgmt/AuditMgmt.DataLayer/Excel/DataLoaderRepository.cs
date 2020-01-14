using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace AuditMgmt.DataLayer.Excel
{
    public class DataLoaderRepository : IDataLoaderRepository
    {
        SqlConnection connection;
        public DataLoaderRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
        }
        public void SaveData(DataTable dataTable, string tableName, List<string> headers, string SPtoExecute = null)
        {
            int colIndex = 0;
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection);
            //loop through headers
            headers.ForEach(i => { sqlBulkCopy.ColumnMappings.Add(colIndex, colIndex); colIndex++; });
            sqlBulkCopy.DestinationTableName = tableName;
            connection.Open();
            sqlBulkCopy.WriteToServer(dataTable);
            if (!string.IsNullOrEmpty(SPtoExecute))
                connection.Query("Exec " + SPtoExecute);
            connection.Close();
        }

        /// <summary>
        /// Delete old data of the table
        /// </summary>
        /// <param name="tableName"></param>
        public void DeleteTableValues(string tableName)
        {
            using (var command = new SqlCommand())
            {
                command.CommandText = "DELETE FROM "+ tableName;
                command.Connection = connection;
                connection.Open();
                int numberDeleted = command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}
