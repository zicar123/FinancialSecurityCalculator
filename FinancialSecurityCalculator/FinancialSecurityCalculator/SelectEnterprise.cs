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
    public partial class SelectEnterprise : Form
    {
        public SelectEnterprise(List<Enterprise> data)
        {
            InitializeComponent();
            dataGridView1.DataSource = data;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "ID підприємства";
            dataGridView1.Columns[2].HeaderText = "Назва Підприємства";
            dataGridView1.Columns[3].HeaderText = "Область";
            dataGridView1.Columns[4].HeaderText = "Галузь";
        }
        private Enterprise enterpriseDataTemp;

        public Dictionary<string, object> EnterpriseData
        {
            get
            {
                return new Dictionary<string, object>
                {
                    { "EnterpriseID", enterpriseDataTemp.EnterpriseId },
                    { "EnterpriseName", enterpriseDataTemp.EnterpriseName },
                    { "Region", enterpriseDataTemp.Region },
                    { "Branch", enterpriseDataTemp.Branch }
                };
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enterpriseDataTemp = (Enterprise)dataGridView1?.CurrentRow.DataBoundItem;
            this.DialogResult = DialogResult.OK;
        }
    }
}
