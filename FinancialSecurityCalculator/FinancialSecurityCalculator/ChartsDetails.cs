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
    public partial class ChartsDetails : Form
    {
        private Dictionary<string, double> data;

        public ChartsDetails(Dictionary<string, double> data, string Title)
        {
            this.data = data;
            InitializeComponent();
            this.Text = Title;
            this.Organizer();
        }

        private void Organizer()
        {
            //chart1.ChartAreas[0].AxisY.Interval = 0.1;
            for (int i = 0; i < data.Count; i++)
            {
                chart1.Series.Add(data.Keys.ToList()[i]);
                chart1.Series[i].ChartType = SeriesChartType.RangeColumn;
                chart1.Series[i].Points.AddXY("Діаграма значень показників", data.Values.ToList()[i]);
                chart1.Series[i].Points[0].ToolTip = data.Values.ToList()[i].ToString();
            }
        }
    }
}
