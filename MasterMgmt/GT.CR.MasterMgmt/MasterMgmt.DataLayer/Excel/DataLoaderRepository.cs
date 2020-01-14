using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace MasterMgmt.DataLayer.Excel
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
            var a=dataTable.Rows.Count;
            if (!string.IsNullOrEmpty(SPtoExecute))
                connection.Query("Exec " + SPtoExecute);
            connection.Close();
        }
    }
}
