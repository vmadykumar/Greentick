using MasterMgmt.DataLayer.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Linq;
using MasterMgmt.CommonLayer.Models.DTO;

namespace MasterMgmt.BusinessLayer.ExcelFactory
{
    public class LoadData
    {
        readonly IDataLoaderRepository dataLoaderRepository;
        public LoadData(IDataLoaderRepository _dataLoaderRepository)
        {
            dataLoaderRepository = _dataLoaderRepository;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public void toTable(string FilePath, List<SheetInfo> sheetMappings)
        {
            IExcelDataReader dr = ExcelReaderFactory.CreateReader(File.Open(FilePath, FileMode.Open), new ExcelReaderConfiguration() { FallbackEncoding = Encoding.GetEncoding(1252) });
            var dataSet = dr.AsDataSet(new ExcelDataSetConfiguration() { ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true } });
            foreach (var sheet in sheetMappings)
            {
                if (dataSet.Tables.Count > sheet.SheetNumber)
                    dataLoaderRepository.SaveData(dataSet.Tables[sheet.SheetNumber], sheet.TableName, new string[dataSet.Tables[sheet.SheetNumber].Columns.Count].ToList(),sheet.Procedure);
            }
        }
    }
}
