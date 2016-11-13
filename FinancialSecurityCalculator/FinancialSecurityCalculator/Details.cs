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
        public Details(List<Services.Services.EnterpriseConclusion> conclusionsList)
        {
            InitializeComponent();
            dataGridView1.DataSource = conclusionsList; //using LINQ is dead end                 
        }
    }
}
