using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialSecurityCalculator.Entities;
using FinancialSecurityCalculator.Context;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinancialSecurityCalculator
{
    public partial class ChartsCompares : Form
    {
        public ChartsCompares(List<string> listName, List<string> listFirstValue, List<string> listSecondValue)
        {
            InitializeComponent();

            

            for (int i = 0; i < listName.Count; i++)
            {
                chart1.Series.Add(listName[i]);
                chart1.Series[i].ChartType = SeriesChartType.RangeColumn;
                chart1.Series[i].Points.AddXY(listName[i], listFirstValue[i]);
            }
           
        }
    }
}
