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

namespace FinancialSecurityCalculator
{
    public partial class Compare : Form
    {
        public Compare(IEnumerable<object> dataSourse, string firstEnterprise, string secondEnterprise)
        {
            InitializeComponent();
            dataGridView1.DataSource = dataSourse;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].HeaderText = "Назва показника";
            dataGridView1.Columns[1].HeaderText = "Для: " + firstEnterprise;
            dataGridView1.Columns[2].HeaderText = "Для: " + secondEnterprise;
            dataGridView1.Columns[0].Width = 260;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
        }
    }
}
