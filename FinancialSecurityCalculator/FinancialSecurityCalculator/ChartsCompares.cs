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
        public ChartsCompares(List<string> listName, List<string> listFirstValue, List<string> listSecondValue, List<int> listID, string firstEnterprise, string secondEnterprise)
        {
            InitializeComponent();

            var legend = new Legend();

            chart1.Series.Add("      Назва підприємства:");
            chart1.Series[0].Color = Color.White;
            



            chart1.Series.Add(firstEnterprise);
            chart1.Series.Add(secondEnterprise);

            for (int i = 0; i < listName.Count; i++)
            {
                chart1.ChartAreas[0].AxisX.Interval = 1;
                chart1.Series[firstEnterprise].ChartType = SeriesChartType.RangeColumn;
                chart1.Series[secondEnterprise].ChartType = SeriesChartType.RangeColumn;
                
                chart1.Series[firstEnterprise].Points.AddXY("k" + listID[i], double.Parse(listFirstValue[i]));
                chart1.Series[secondEnterprise].Points.AddXY("k" + listID[i], double.Parse(listSecondValue[i]));
                chart1.Series[firstEnterprise].Points[i].ToolTip = "k" + listID[i] + " = " + listFirstValue[i];
                chart1.Series[secondEnterprise].Points[i].ToolTip = "k" + listID[i] + " = " + listSecondValue[i];

                legend.CustomItems.Add(Color.Aqua, "k" + listID[i] + " - " + listName[i]);
                legend.CustomItems[i].ImageStyle = LegendImageStyle.Marker;
            }
            chart1.Legends.Add(legend);
           
        }
    }
}
