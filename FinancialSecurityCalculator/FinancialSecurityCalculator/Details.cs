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
        public Details(object conclusionsList)
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = conclusionsList;
            dataGridView1.Columns[0].HeaderText = "Назва показника";
            dataGridView1.Columns[1].HeaderText = "Поточне значення";
            dataGridView1.Columns[2].HeaderText = "Аналітичний висновок";
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[0].Width = 250; 
        }
    }
}
