using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinancialSecurityCalculator.Services;


namespace FinancialSecurityCalculator
{
    public partial class Details : Form
    {
        private IEnumerable<object> conclusionsList;
  
        public Details(IEnumerable<object> conclusionsList, string Title)
        {
            InitializeComponent();
            this.Text = Title;
            this.conclusionsList = conclusionsList;
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = conclusionsList;
            dataGridView1.Columns[0].HeaderText = "Назва показника";
            dataGridView1.Columns[1].HeaderText = "Значення показника";
            dataGridView1.Columns[2].HeaderText = "Аналітичний висновок";
            dataGridView1.Columns[0].Width = 250;
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Visible = false;
        }

        private static T CastType<T>(object obj, T type)
        {
            return (T) obj;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var anonList = new[] { new { NameOfIndicator = default(string),
                                         CurrentValue = default(double),
                                         Conclusion = default(string),
                                         ClusterID = default(int) } }.ToList();
            anonList.Clear();

            foreach (var item in conclusionsList)
            {
                anonList.Add(CastType(item, new
                {
                    NameOfIndicator = default(string),
                    CurrentValue = default(double),
                    Conclusion = default(string),
                    ClusterID = default(int)
                }));
            }

            var clusterNorma = from item in anonList
                               where item.ClusterID == 1
                               select new
                               {
                                   NameOfIndicator = item.NameOfIndicator,
                                   CurrentValue = item.CurrentValue
                               };

            var clusterPreCrysis = from item in anonList
                                   where item.ClusterID == 2
                                   select new
                                   {
                                       NameOfIndicator = item.NameOfIndicator,
                                       CurrentValue = item.CurrentValue
                                   };

            var clusterCrysis = from item in anonList
                                where item.ClusterID == 3
                                select new
                                {
                                    NameOfIndicator = item.NameOfIndicator,
                                    CurrentValue = item.CurrentValue
                                };

            var clusterNoData = from item in anonList
                                where item.ClusterID == 0
                                select new
                                {
                                    NameOfIndicator = item.NameOfIndicator                                    
                                };

            var clustersForm = new Clusters(clusterNorma.ToList(), clusterPreCrysis.ToList(), clusterCrysis.ToList(), clusterNoData.ToList(), this.Text);
            clustersForm.Show();
        }
    }
}
