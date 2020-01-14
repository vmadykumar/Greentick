using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AuditMgmt.DataLayer.Excel
{
   public interface IDataLoaderRepository
    {
        void SaveData(DataTable dataTable, string tableName,List<string> headers, string SPtoExecute = null);

        void DeleteTableValues(string tableName);
    }
}
