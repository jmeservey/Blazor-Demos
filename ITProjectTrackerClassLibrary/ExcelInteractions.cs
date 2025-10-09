using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITProjectTrackerClassLibrary
{
    public class ExcelInteractions
    {
        private static string PopulateItemsSpreadsheetPath = @"Z:\MIS\Project Tracker\Resources\Populated Items.xlsx";

        public static (List<string> projectTypeList, List<string> ePlantList, List<string> departmentList, List<string> projectStageList, List<string> percentCompleteList) GetComboLists()
        {
            List<string> projectTypeList = new List<string>();
            List<string> ePlantList = new List<string>();
            List<string> departmentList = new List<string>();
            List<string> projectStageList = new List<string>();
            List<string> percentCompleteList = new List<string>();
            string tempItem;
            List<string> tempList = new List<string>();
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(PopulateItemsSpreadsheetPath);
            Worksheet worksheet = workbook.Worksheets[0];
            DataTable worksheetDataTable = worksheet.ExportDataTable();
            List<string> headerList = worksheetDataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();

            foreach (string header in headerList)
            {
                tempList.Clear();

                foreach (DataRow row in worksheetDataTable.AsEnumerable())
                {
                    tempItem = row[header].ToString();

                    if (tempItem.Length > 0)
                    {
                        tempList.Add(tempItem);
                    }
                    else
                    {
                        break;
                    }
                }

                if (header == "Project Type")
                {
                    projectTypeList.AddRange(tempList);
                }
                else if (header == "EPlant")
                {
                    ePlantList.AddRange(tempList);
                }
                else if (header == "Department")
                {
                    departmentList.AddRange(tempList);
                }
                else if (header == "Project Stage")
                {
                    projectStageList.AddRange(tempList);
                }
                else if (header == "Percent Complete")
                {
                    percentCompleteList.AddRange(tempList);
                }
            }

            return (projectTypeList, ePlantList, departmentList, projectStageList, percentCompleteList);
        }

        public static string GetPopulatedItemsPath()
        {
            return PopulateItemsSpreadsheetPath;
        }
    }
}
