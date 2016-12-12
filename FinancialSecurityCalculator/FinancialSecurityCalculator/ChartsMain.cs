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
    public partial class ChartsMain : Form
    {
        private List<Record> data;
        private TabControl tabControl;
        private Button buttonVisualize = new Button() { Text = "Відобразити", Size = new Size(150, 25) };
        private Services.Services services;
        private SeriesCollection series;

        public ChartsMain(List<Record> data, string enterpriseName, TabControl tabControl, Services.Services services)
        {
            buttonVisualize.Click += new EventHandler(VisualizeClick);
            this.data = data.OrderBy(x => x.Year).ToList();
            this.tabControl = tabControl;
            InitializeComponent();
            this.Text = "Підприємство:  " + enterpriseName;
            this.services = services;
            this.Organizer();
        }

        private void Organizer()
        {
            var context = new FSCContext();
            List<EnterpriseLimitIndicator> limits = (from item in context.EnterpriseLimitIndicators // join it to first entry of Context and pass as parameter here
                                                     select item).ToList();
            context.Dispose();
            int temp = 0;
            for (int i = 0; i < limits.Count; i++)
            {
                chart1.Series.Add(limits[i].EnterpriseLimitIndicatorName);
                chart1.Series[i].ChartType = SeriesChartType.Line;
                chart1.Series[i].BorderWidth = 3;

                flowLayoutPanel1.Controls.Add(new CheckBox() { Text = chart1.Series[i].Name, Font = new Font(new Font("Times New Roman", 12), FontStyle.Bold), AutoSize = true });
                foreach (var item in data)
                {
                    if (item.EnterpriseIndicators.FirstOrDefault(x => x.IndicatorName == limits[i].EnterpriseLimitIndicatorName) != null)
                    {
                        double indicatorValue = item.EnterpriseIndicators.FirstOrDefault(x => x.IndicatorName == limits[i].EnterpriseLimitIndicatorName).IndicatorValue;
                        chart1.Series[i].Points.AddXY(item.Year, indicatorValue);
                        string labelValue = indicatorValue.ToString() + " - " + services.DecisionMaking(item.EnterpriseIndicators.FirstOrDefault(x => x.IndicatorName == limits[i].EnterpriseLimitIndicatorName), out temp);
                        chart1.Series[i].Points.Last().ToolTip = labelValue;
                        chart1.Series[i].Points.Last().Label = labelValue;
                        chart1.Series[i].Points.Last().MarkerStyle = MarkerStyle.Diamond;
                        chart1.Series[i].Points.Last().MarkerSize = 13;                        
                    }
                }
            }

            flowLayoutPanel1.Controls.Add(buttonVisualize);

            foreach (var item in chart1.Series.ToList())
            {
                if (item.Points.Count <= 1)
                {
                    chart1.Series.Remove(item);
                }
            }

            var chbList = new List<CheckBox>(20);
            foreach (var item in flowLayoutPanel1.Controls)
            {
                if (item is CheckBox)
                {
                    chbList.Add(item as CheckBox);
                }
            }

            foreach (var box in chbList)
            {
                bool flag = false;
                foreach (var serie in chart1.Series)
                {
                    if (box.Text == serie.Name)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    flowLayoutPanel1.Controls.Remove(box);
                }

            }

            chart1.ApplyPaletteColors();
            for (int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++)
            {
                (flowLayoutPanel1.Controls[i] as CheckBox).Text = tabControl.TabPages[i].Text; //override checkBox text property
                (flowLayoutPanel1.Controls[i] as CheckBox).BackColor = chart1.Series[i].Color;
            }

            for (int i = 0; i < chart1.Series.Count && i < 4; i++)
            {
                (flowLayoutPanel1.Controls[i] as CheckBox).Checked = true;
            }

            this.series = chart1.Series;
            this.CheckBoxChecked();
        }

        private void CheckBoxChecked()
        {
            for (int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++)
            {
                if ((flowLayoutPanel1.Controls[i] as CheckBox).Checked)
                {
                    chart1.Series[i].Enabled = true;
                }
                else
                {
                    chart1.Series[i].Enabled = false;
                }
            }
        }

        void VisualizeClick(object sender, EventArgs e)
        {
            this.CheckBoxChecked();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                for (int i = 0; i < chart1.Series.Count; i++)
                    chart1.Series[i].LabelForeColor = Color.Black;
            else
                for (int i = 0; i < chart1.Series.Count; i++)
                    chart1.Series[i].LabelForeColor = Color.Transparent;
        }
    }
}
