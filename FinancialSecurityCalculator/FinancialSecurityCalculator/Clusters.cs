using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialSecurityCalculator
{
    public partial class Clusters : Form
    {
        public Clusters(IEnumerable<object> cluster1, IEnumerable<object> cluster2, IEnumerable<object> cluster3, IEnumerable<object> cluster4, string Title)
        {
            InitializeComponent();

            this.Text = Title;

            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            dataGridView3.ReadOnly = true;
            dataGridView4.ReadOnly = true;

            dataGridView1.DataSource = cluster1;
            dataGridView2.DataSource = cluster2;
            dataGridView3.DataSource = cluster3;
            dataGridView4.DataSource = cluster4;

            dataGridView1.Columns[0].Width = 240;
            dataGridView2.Columns[0].Width = 240;
            dataGridView3.Columns[0].Width = 240;
            dataGridView4.Columns[0].Width = 240;

            dataGridView1.Columns[0].HeaderText = "Назва показника";
            dataGridView2.Columns[0].HeaderText = "Назва показника";
            dataGridView3.Columns[0].HeaderText = "Назва показника";
            dataGridView4.Columns[0].HeaderText = "Назва показника";


            dataGridView1.Columns[1].HeaderText = "Значення показника";
            dataGridView2.Columns[1].HeaderText = "Значення показника";
            dataGridView3.Columns[1].HeaderText = "Значення показника";
        }
    }
}
 