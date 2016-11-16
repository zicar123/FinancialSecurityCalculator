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
        public Compare(IEnumerable<object> dataSourse)
        {
            InitializeComponent();
            dataGridView1.DataSource = dataSourse;
            dataGridView1.ReadOnly = true;
        }
    }
}
