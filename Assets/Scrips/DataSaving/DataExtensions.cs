using System;
using System.Collections.Generic;
using Scrips.Cell;

namespace Scrips.DataSaving
{
    public static class DataExtension
    {
        public static string StatesToString(this List<CellModel> cellModels)
        {
            string result = String.Empty;
            
            foreach (CellModel cellModel in cellModels) result += cellModel.State;

            return result;
        }
    }
}