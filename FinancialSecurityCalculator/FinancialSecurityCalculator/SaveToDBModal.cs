using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace FinancialSecurityCalculator
{
    public partial class SaveToDBModal : Form
    {
        public SaveToDBModal(List<string> regions, List<string> branches)
        {
            InitializeComponent();
            comboBox1.Items.AddRange(regions.ToArray());
            comboBox2.Items.AddRange(branches.ToArray());
        }

        public void ClearFields()
        {
            textBox1.Text = string.Empty;
            comboBox1.Text = null;
            comboBox2.Text = null;
        }

        public Dictionary<string, object> EnterpriseData
        {
            get
            {
                return new Dictionary<string, object>
                {
                    { "EnterpriseName", textBox1.Text },
                    { "Region", comboBox1.SelectedItem },
                    { "Branch", comboBox2.SelectedItem },
                    { "Year", numericUpDown1.Value }
                };
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
