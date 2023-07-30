using JDA.Core.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace JDA.Core.Utilities
{
    public class ExcelHelper
    {
        public static string Export(DataTable dataTable, Dictionary<string, ColumnMetadataAttribute> columnMetadatas,string filePath)
        {
            ExportCore(dataTable, columnMetadatas, filePath, (package, filePath) =>
            {
                package.SaveAs(new FileInfo(filePath));
            });

            return filePath;
        }
        public static Task<string> ExportAsync(DataTable dataTable, Dictionary<string, ColumnMetadataAttribute> columnMetadatas, string filePath)
        {
            ExportCore(dataTable, columnMetadatas, filePath, async (package, filePath) =>
            {
                await package.SaveAsAsync(new FileInfo(filePath));
            });

            return Task.FromResult(filePath);
        }

        public static void ExportCore(DataTable dataTable, Dictionary<string, ColumnMetadataAttribute> columnMetadatas, string filePath, Action<ExcelPackage, string> action)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
                //设置表头
                var showColumnMetadatas = columnMetadatas.Where(n => !n.Value.Hidden).OrderBy(n => n.Value.Order).ToDictionary(n => n.Key, n => n.Value);
                int headerColumnIndex = 1;
                foreach (var columnMetadata in showColumnMetadatas)
                {
                    var columnMetadataInfo = columnMetadata.Value;
                    worksheet.Cells[1, headerColumnIndex].Value = columnMetadataInfo.Name;
                    headerColumnIndex++;
                }

                //设置内容
                int dataRowIndex = 2;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    int dataColumnIndex = 1;
                    foreach (var columnMetadata in showColumnMetadatas)
                    {
                        worksheet.Cells[dataRowIndex, dataColumnIndex].Value = dataRow[columnMetadata.Key];
                        dataColumnIndex++;
                    }
                    dataRowIndex++;
                }

                string directoryName = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryName))
                    Directory.CreateDirectory(directoryName);

                action?.Invoke(package, filePath);
            }
        }
    }
}
