﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;

namespace FinancialSecurityCalculator.SLExcelUtility
{
    public class SLExcelStatus
    {
        public string Message { get; set; }
        public bool Success
        {
            get { return string.IsNullOrWhiteSpace(Message); }
        }
    }

    public class SLExcelData
    {
        public SLExcelStatus Status { get; set; }
        public Columns ColumnConfigurations { get; set; }
        public List<string> Headers { get; set; }
        public List<List<string>> DataRows { get; set; }
        public string SheetName { get; set; }

        public SLExcelData()
        {
            Status = new SLExcelStatus();
            Headers = new List<string>();
            DataRows = new List<List<string>>();
        }
    }
}
